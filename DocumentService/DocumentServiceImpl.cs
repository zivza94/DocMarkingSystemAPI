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
        IInfraDAL _dal;
        IDBConnection _conn;
        public DocumentServiceImpl(IInfraDAL dal)
        {
            _dal = dal;
        }
        public void Connect(string connStr)
        {
            _conn = _dal.Connect(connStr);
        }

        public async Task<Response> GetDocument(GetDocumentRequest request)
        {
            Response retval = new GetDocumentResponseInvalidDocID(request);
            if (await IsDocumentExists(request.DocId))
            {
                IDBParameter docID = _dal.CreateParameter("P_DOC_ID", request.DocId);
                IDBParameter outParam = _dal.GetOutParameter();
                try
                {
                    DataSet ds = _dal.ExecuteSPQuery(_conn, "GETDOCUMENT", docID, outParam);
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
            
            if(await IsUserExists(request.UserID))
            {
                var id = Guid.NewGuid();
                IDBParameter docID = _dal.CreateParameter("P_DOCUMENT_ID", id.ToString());
                IDBParameter userID = _dal.CreateParameter("P_USER_ID", request.UserID);
                IDBParameter imageURL = _dal.CreateParameter("P_IMAGE_URL", request.ImageURL);
                IDBParameter documentName = _dal.CreateParameter("P_DOCUMENT_NAME", request.DocumentName);
                try
                {
                    DataSet ds = _dal.ExecuteSPQuery(_conn, "CreateDocument", docID, userID, imageURL, documentName);
                    retval = new CreateDocumentResponseOK(request);
                }
                catch
                {
                    retval = new AppResponseError("Error in create document");
                }
            }
            return retval;
        }

        public async Task<Response> GetDocuments(GetDocumentsRequest request)
        {
            Response retval = new GetDocumentsResponseInvalidUserID(request);
            if (await IsUserExists(request.UserID))
            {
                //validate user exist
                List<Document> documents = new List<Document>();
                IDBParameter userID = _dal.CreateParameter("P_USER_ID", request.UserID);
                IDBParameter outParam = _dal.GetOutParameter();
                try
                {
                    DataSet ds = _dal.ExecuteSPQuery(_conn, "GETDOCUMENTS", userID, outParam);
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
            if (await IsDocumentExists(request.DocID))
            {
                try
                {
                    IDBParameter docID = _dal.CreateParameter("P_DOC_ID", request.DocID);
                    _dal.ExecuteSPQuery(_conn, "REMOVEDOCUMENT", docID);
                    retval = new RemoveDocumentResponseOK(request);
                }
                catch (Exception ex)
                {
                    retval = new AppResponseError("Error in create document");
                }
            }

            return retval;
        }

        private async Task<bool> IsDocumentExists(string docID)
        {
            var retval = true;
            DataSet ds = _dal.ExecuteQuery(_conn, "select * from documents where document_id = '" + docID + "'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                retval = false;
            }

            return retval;
        }

        private async Task<bool> IsUserExists(string userID)
        {
            var retval = true;
            DataSet ds = _dal.ExecuteQuery(_conn, "SELECT * FROM USERS WHERE USERID = '" + userID+ "'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                retval = false;
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

        private async Task<Response> RemoveSharedDocument(string docID)
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

        }
        private async Task<HttpResponseMessage> RemoveShare(string jsonData)
        {
            var url = "https://localhost:5001/api/Sharing/RemoveShare";
            using var client = new HttpClient();
            var data = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var retval = await client.PostAsync(url, data);
            return retval;
        }
    }
}
