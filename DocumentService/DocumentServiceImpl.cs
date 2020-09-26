using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DALContracts;
using DIContract;
using DocMarkingSystemContracts.DTO;
using DocMarkingSystemContracts.DTO.Documents;
using DocMarkingSystemContracts.DTO.Sharing;
using DocMarkingSystemContracts.Interfaces;
using Newtonsoft.Json;

namespace DocumentService
{
    [Register(Policy.Transient, typeof(IDocumentService))]
    public class DocumentServiceImpl : IDocumentService
    {
        IDocMarkingSystemDAL _dal;
        IDBConnection _conn;
        private IDocumentWebSocket _webSocket;
        public DocumentServiceImpl(IDocMarkingSystemDAL dal, IDocumentWebSocket webSocket)
        {
            _dal = dal;
            _webSocket = webSocket;
        }
        public void Connect(string connStr)
        {
            _conn = _dal.Connect(connStr);
        }

        public async Task<Response> GetDocument(GetDocumentRequest request)
        {
            Response retval = new GetDocumentResponseInvalidDocID(request);
            if ( _dal.IsDocumentExists(_conn,request.DocId))
            {
                try
                {
                    DataSet ds = _dal.GetDocument(_conn, request.DocId);
                    var data = ds.Tables[0].Rows[0];
                    Document doc = new Document()
                    {
                        DocID = (string) data["document_id"],
                        DocumentName = (string) data["document_name"],
                        ImageURL = (string) data["image_url"],
                        UserID = (string) data["user_id"]
                    };
                    retval = new GetDocumentResponseOK(doc);
                    
                }
                catch
                {
                    retval = new AppResponseError("Error in get document");
                }
            }

            return retval;
        }

        public async Task<Response> CreateDocument(CreateDocumentRequest request)
        {
            Response retval = new CreateDocumentResponseInvalidData(request);
            
            if(_dal.IsUserExists(_conn,request.UserID))
            {
                var id = Guid.NewGuid().ToString();
                Document document = new Document(){DocID = id,DocumentName = request.DocumentName,
                    ImageURL = request.ImageURL,UserID = request.UserID};
                try
                {
                    _dal.CreateDocument(_conn, document);
                    retval = new CreateDocumentResponseOK(request);
                    await _webSocket.Notify("new Document" + id);
                }
                catch(Exception ex)
                {
                    retval = new AppResponseError("Error in create document");
                }
            }
            return retval;
        }

        public async Task<Response> GetDocuments(GetDocumentsRequest request)
        {
            Response retval = new GetDocumentsResponseInvalidUserID(request);
            if (_dal.IsUserExists(_conn,request.UserID))
            {
                List<Document> documents = new List<Document>();
                
                try
                {
                    DataSet ds = _dal.GetDocuments(_conn, request.UserID);
                    var rows = ds.Tables[0].Rows;
                    foreach (DataRow row in rows)
                    {
                        Document doc = new Document()
                        {
                            DocID = (string) row["document_id"], DocumentName = (string) row["document_name"],
                            ImageURL = (string) row["image_url"], UserID = (string) row["user_id"]
                        };
                        documents.Add(doc);
                    }

                    var sharedDocsResponse = await GetSharedDocuments(request.UserID);
                    if (sharedDocsResponse is GetDocumentsResponseOK)
                    {

                        documents.AddRange((sharedDocsResponse as GetDocumentsResponseOK).Documents);
                    }else if (sharedDocsResponse is AppResponseError)
                    {
                        return sharedDocsResponse;
                    }
                    
                    retval = new GetDocumentsResponseOK(documents);
                }
                catch(Exception ex)
                {
                    retval = new AppResponseError("Error in get documents");
                }
            }

            return retval;


        }

        public async Task<Response> RemoveDocument(RemoveDocumentRequest request)
        {
            Response retval = new RemoveDocumentResponseInvalidDocID(request);
            if (_dal.IsDocumentExists(_conn,request.DocID))
            {
                try
                {
                    _dal.RemoveDocument(_conn,request.DocID);
                    retval = new RemoveDocumentResponseOK(request);
                    await _webSocket.Notify("remove document: " + request.DocID);
                }
                catch (Exception ex)
                {
                    retval = new AppResponseError("Error in create document");
                }
            }

            return retval;
        }

        private async Task<Response> GetSharedDocuments(string userId)
        {
            Response retval;
            GetSharedDocumentsRequest shareReq = new GetSharedDocumentsRequest(){UserID = userId};
            var jsonRequest = JsonConvert.SerializeObject(shareReq);
            var response = await SendRequest(jsonRequest);
            if (response.IsSuccessStatusCode)
            {
                List<Document> documents = new List<Document>();
                var content = await response.Content.ReadAsStringAsync();
                var codeResponse = JsonConvert.DeserializeObject<GetSharedDocumentsResponseOK>(content);
                foreach (string docId in codeResponse.Documents)
                {
                    GetDocumentRequest request = new GetDocumentRequest(){DocId = docId};
                    Response docResponse= await GetDocument(request);
                    if (docResponse is GetDocumentResponseOK)
                    {
                        Document doc = (docResponse as GetDocumentResponseOK).Document;
                        documents.Add(doc);
                    }else if (docResponse is AppResponseError)
                    {
                        return docResponse;
                    }
                }
                retval = new GetDocumentsResponseOK(documents);
            }
            else
            {
                retval = new AppResponseError(response.StatusCode.ToString());
            }

            return retval;
        }
        private async Task<HttpResponseMessage> SendRequest(string jsonData)
        {
            var url = "https://localhost:5001/api/Sharing/getSharedDocuments";
            using var client = new HttpClient();
            var data = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var retval = await client.PostAsync(url, data);
            return retval;



        }

        /*private async Task<Response> RemoveSharedDocument(string docID)
        {
            Response retval = null;
            DataSet ds = _dal.ExecuteQuery(_conn, "SELECT * from shared_documents where doc_ID = '" + docID + "'");
            if (ds.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    RemoveShareRequest request = new RemoveShareRequest()
                    {
                        Share = new Share()
                        {
                            DocID = (string) row["DOC_ID"],
                            UserID = (string) row["USER_ID"]
                        }
                    };
                    var jsonRequest = JsonConvert.SerializeObject(request);
                    var response = await RemoveShare(jsonRequest);
                    if (!response.IsSuccessStatusCode)
                    {
                        retval = new AppResponseError("Error while delete shared document");
                    }
                }
            } 
            return retval;

        }*/
        /*private async Task<HttpResponseMessage> RemoveShare(string jsonData)
        {
            var url = "https://localhost:5001/api/Sharing/RemoveShare";
            using var client = new HttpClient();
            var data = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var retval = await client.PostAsync(url, data);
            return retval;
        }*/
    }
}
