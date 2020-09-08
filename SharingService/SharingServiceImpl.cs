using DocMarkingSystemContracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DALContracts;
using DIContract;
using DocMarkingSystemContracts.DTO;
using DocMarkingSystemContracts.DTO.Sharing;

namespace SharingService
{
    [Register(Policy.Transient,typeof(ISharingService))]
    public class SharingServiceImpl:ISharingService
    {
        IInfraDAL _dal;
        IDBConnection _conn;
        public SharingServiceImpl(IInfraDAL dal)
        {
            _dal = dal;
        }
        public void Connect(string connStr)
        {
            _conn = _dal.Connect(connStr);
        }
        public async Task<Response> CreateShare(CreateShareRequest request)
        {
            Response retval;
            if (await IsShareExists(request.Share))
            {
                retval = new CreateShareResponseShareExists(request);
            }else if (await IsDocumentExists(request.Share.DocID) && await IsUserExists(request.Share.UserID))
            {
                retval = new CreateShareResponseInvalidID(request);
            }
            else if (!await IsUserOwner(request.UserID,request.Share.DocID))
            {
                retval = new CreateShareResponseNotAuthorized(request);
            }
            else
            {
                try
                {
                    IDBParameter docID = _dal.CreateParameter("P_DOC_ID", request.Share.DocID);
                    IDBParameter userID = _dal.CreateParameter("P_USER_ID", request.Share.UserID);
                    _dal.ExecuteSPQuery(_conn, "CREATESHARE", docID, userID);
                    retval = new CreateShareResponseOK(request);
                }
                catch (Exception ex)
                {
                    retval = new AppResponseError("Error in create share");
                }
            }

            return retval;
        }

        public async Task<Response> GetSharedDocuments(GetSharedDocumentsRequest request)
        {
            Response retval = new GetSharedDocumentsResponseInvalidUserID(request);
            if (await IsUserExists(request.UserID))
            {
                try
                {
                    List<string> documentsIDs = new List<string>();
                    IDBParameter userID = _dal.CreateParameter("P_USER_ID", request.UserID);
                    IDBParameter outParam = _dal.GetOutParameter();
                    DataSet ds =_dal.ExecuteSPQuery(_conn, "GETSHAREDDOCUMENTS", userID,outParam);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        documentsIDs.Add((string)row["DOC_ID"]);
                    }
                    retval = new GetSharedDocumentsResponseOK(documentsIDs);
                }
                catch (Exception ex)
                {
                    retval = new AppResponseError("Error in get shared documents");
                }
            }

            return retval;
        }

        public async Task<Response> GetSharedUsers(GetSharedUsersRequest request)
        {
            Response retval = new GetSharedUsersResponseInvalidDocID(request);
            if (await IsDocumentExists(request.DocID))
            {
                try
                {
                    List<string> usersIDs = new List<string>();
                    IDBParameter docID = _dal.CreateParameter("P_DOC_ID", request.DocID);
                    IDBParameter outParam = _dal.GetOutParameter();
                    DataSet ds = _dal.ExecuteSPQuery(_conn, "GETSHAREDUSERS", docID,outParam);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        usersIDs.Add((string)row["USER_ID"]);
                    }
                    retval = new GetSharedUsersResponseOK(usersIDs);
                }
                catch (Exception ex)
                {
                    retval = new AppResponseError("Error in get shared users");
                }
            }

            return retval;
        }

        public async Task<Response> RemoveShare(RemoveShareRequest request)
        {
            Response retval = new RemoveShareResponseInvalidID(request);
            if (!await IsShareExists(request.Share))
            {
                retval = new RemoveShareResponseInvalidID(request);
            }else if (request.UserID != request.Share.UserID && !await IsUserOwner(request.UserID, request.Share.DocID) )
            {
                retval = new RemoveShareResponseNotAuthorized(request);
            }
            else
            {
                try
                {
                    IDBParameter docID = _dal.CreateParameter("P_DOC_ID", request.Share.DocID);
                    IDBParameter userID = _dal.CreateParameter("P_USER_ID", request.Share.UserID);
                    _dal.ExecuteSPQuery(_conn, "REMOVESHARE", docID, userID);
                    retval = new RemoveShareResponseOK(request);
                }
                catch
                {
                    retval = new AppResponseError("Error in server in remove share");
                }
            }
            return retval;
        }

        private async Task<bool> IsUserOwner(string userID, string docID)
        {
            var retval = true;
            DataSet ds = _dal.ExecuteQuery(_conn, "select * from documents where document_id = '" + docID + "' and user_id = '" +
                                                  userID + "'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                retval = false;
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
            DataSet ds = _dal.ExecuteQuery(_conn, "SELECT * FROM USERS WHERE USERID = '" + userID + "'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                retval = false;
            }

            return retval;
        }
        private async Task<bool> IsShareExists(Share share)
        {
            var retval = true;
            DataSet ds = _dal.ExecuteQuery(_conn, "SELECT * FROM SHARED_DOCUMENTS WHERE USER_ID = '" + share.UserID+
                                                  "' AND DOC_ID = '" + share.DocID +"'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                retval = false;
            }

            return retval;
        }

    }
}
