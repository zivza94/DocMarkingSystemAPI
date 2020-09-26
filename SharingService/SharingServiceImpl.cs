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
        IDocMarkingSystemDAL _dal;
        IDBConnection _conn;
        private ISharingWebSocket _webSocket;
        public SharingServiceImpl(IDocMarkingSystemDAL dal,ISharingWebSocket webSocket)
        {
            _dal = dal;
            _webSocket = webSocket;
        }
        public void Connect(string connStr)
        {
            _conn = _dal.Connect(connStr);
        }
        public async Task<Response> CreateShare(CreateShareRequest request)
        {
            Response retval;
            if (_dal.IsShareExists(_conn,request.Share))
            {
                retval = new CreateShareResponseShareExists(request);
            }else if (!_dal.IsDocumentExists(_conn,request.Share.DocID) || !_dal.IsUserExists(_conn,request.Share.UserID))
            {
                retval = new CreateShareResponseInvalidID(request);
            }
            else if (!_dal.IsUserOwner(_conn,request.UserID,request.Share.DocID))
            {
                retval = new CreateShareResponseNotAuthorized(request);
            }
            else
            {
                try
                {
                    _dal.CreateShare(_conn,request.Share);
                    retval = new CreateShareResponseOK(request);
                    await _webSocket.Notify("New share: "+ request.Share.ToString());
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
            if (_dal.IsUserExists(_conn,request.UserID))
            {
                try
                {
                    List<string> documentsIDs = new List<string>();
                    DataSet ds = _dal.GetSharedDocuments(_conn, request.UserID);
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
            if ( _dal.IsDocumentExists(_conn,request.DocID))
            {
                try
                {
                    List<string> usersIDs = new List<string>();
                    DataSet ds = _dal.GetSharedUsers(_conn, request.DocID);
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
            if (! _dal.IsShareExists(_conn,request.Share))
            {
                retval = new RemoveShareResponseInvalidID(request);
            }else if (request.UserID != request.Share.UserID && !_dal.IsUserOwner(_conn,request.UserID, request.Share.DocID) )
            {
                retval = new RemoveShareResponseNotAuthorized(request);
            }
            else
            {
                try
                {
                    _dal.removeShare(_conn,request.Share);
                    retval = new RemoveShareResponseOK(request);
                    await _webSocket.Notify("remove share: " + request.Share.ToString());
                }
                catch
                {
                    retval = new AppResponseError("Error in server in remove share");
                }
            }
            return retval;
        }
    }
}
