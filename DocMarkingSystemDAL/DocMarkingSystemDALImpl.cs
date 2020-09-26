using DocMarkingSystemContracts.Interfaces;
using System;
using DIContract;
using DALContracts;
using DocMarkingSystemContracts.DTO.Documents;
using DocMarkingSystemContracts.DTO.Markers;
using DocMarkingSystemContracts.DTO.Sharing;
using DocMarkingSystemContracts.DTO.Users;
using System.Data;

namespace DocMarkingSystemDAL
{
    [Register(Policy.Transient, typeof(IDocMarkingSystemDAL))]
    public class DocMarkingSystemDALImpl : IDocMarkingSystemDAL
    {
        private IInfraDAL _dal;
        public DocMarkingSystemDALImpl(IInfraDAL dal)
        {
            _dal = dal;
        }
        public IDBConnection Connect(string strConnection)
        {
            return _dal.Connect(strConnection);
        }
        // documents
        public void CreateDocument(IDBConnection conn, Document document)
        {
            IDBParameter docID = _dal.CreateParameter("P_DOCUMENT_ID", document.DocID);
            IDBParameter userID = _dal.CreateParameter("P_USER_ID", document.UserID);
            IDBParameter imageURL = _dal.CreateParameter("P_IMAGE_URL", document.ImageURL);
            IDBParameter documentName = _dal.CreateParameter("P_DOCUMENT_NAME", document.DocumentName);
            _dal.ExecuteSPQuery(conn, "CreateDocument", docID, userID, imageURL, documentName);
        }
        public DataSet GetDocument(IDBConnection conn, string docID)
        {
            IDBParameter docIDParam = _dal.CreateParameter("P_DOC_ID", docID);
            IDBParameter outParam = _dal.GetOutParameter();
            return _dal.ExecuteSPQuery(conn, "GETDOCUMENT", docIDParam, outParam);
        }
        public DataSet GetDocuments(IDBConnection conn, string userID)
        {
            IDBParameter userIDParam = _dal.CreateParameter("P_USER_ID", userID);
            IDBParameter outParam = _dal.GetOutParameter();
            return _dal.ExecuteSPQuery(conn, "GETDOCUMENTS", userIDParam, outParam);
        }
        public void RemoveDocument(IDBConnection conn, string docID)
        {
            IDBParameter docIDParam = _dal.CreateParameter("P_DOC_ID", docID);
            _dal.ExecuteSPQuery(conn, "REMOVEDOCUMENT", docIDParam);
        }
        // markers
        public void CreateMarker(IDBConnection conn, Marker marker)
        {
            IDBParameter markerID = _dal.CreateParameter("P_MARKER_ID", marker.MarkerID);
            IDBParameter docID = _dal.CreateParameter("P_DOC_ID", marker.DocID);
            IDBParameter userID = _dal.CreateParameter("P_USER_ID", marker.UserID);
            IDBParameter backColor = _dal.CreateParameter("P_BACK_COLOR", marker.BackColor);
            IDBParameter foreColor = _dal.CreateParameter("P_FORE_COLOR", marker.ForeColor);
            IDBParameter markerType = _dal.CreateParameter("P_MARKR_TYPE", marker.MarkerType);
            IDBParameter markerx = _dal.CreateParameter("P_MARKER_X", marker.MarkerLocation.PointX);
            IDBParameter markery = _dal.CreateParameter("P_MARKER_Y", marker.MarkerLocation.PointY);
            IDBParameter markerxRadius = _dal.CreateParameter("P_MARKER_X_RADIUS", marker.MarkerLocation.RadiusX);
            IDBParameter markeryRadius = _dal.CreateParameter("P_MARKER_Y_RADIUS", marker.MarkerLocation.RadiusY);
            _dal.ExecuteSPQuery(conn, "CREATEMARKER", docID, markerID, markerType, markerx, markery,
                markerxRadius,
                markeryRadius, foreColor, backColor, userID);
        }
        public DataSet GetMarkers(IDBConnection conn, string docID)
        {
            IDBParameter docIDParam = _dal.CreateParameter("P_DOC_ID", docID);
            IDBParameter outParam = _dal.GetOutParameter();
            return _dal.ExecuteSPQuery(conn, "GETMARKERS", docIDParam, outParam);
        }
        public void RemoveMarker(IDBConnection conn, string markerID)
        {
            IDBParameter markerIDParam = _dal.CreateParameter("P_MARKER_ID", markerID);
            _dal.ExecuteSPQuery(conn, "REMOVEMARKER", markerIDParam);
        }
        //share
        public void CreateShare(IDBConnection conn, Share share)
        {
            IDBParameter docID = _dal.CreateParameter("P_DOC_ID", share.DocID);
            IDBParameter userID = _dal.CreateParameter("P_USER_ID", share.UserID);
            _dal.ExecuteSPQuery(conn, "CREATESHARE", docID, userID);
        }
        public DataSet GetSharedDocuments(IDBConnection conn, string userID)
        {
            IDBParameter userIDParam = _dal.CreateParameter("P_USER_ID", userID);
            IDBParameter outParam = _dal.GetOutParameter();
            return _dal.ExecuteSPQuery(conn, "GETSHAREDDOCUMENTS", userIDParam, outParam);
        }
        public DataSet GetSharedUsers(IDBConnection conn, string docID)
        {
            IDBParameter docIDParam = _dal.CreateParameter("P_DOC_ID", docID);
            IDBParameter outParam = _dal.GetOutParameter();
            return _dal.ExecuteSPQuery(conn, "GETSHAREDUSERS", docIDParam, outParam);
        }
        public void removeShare(IDBConnection conn, Share share)
        {
            IDBParameter docID = _dal.CreateParameter("P_DOC_ID", share.DocID);
            IDBParameter userID = _dal.CreateParameter("P_USER_ID", share.UserID);
            _dal.ExecuteSPQuery(conn, "REMOVESHARE", docID, userID);
        }
        //users
        public void CreateUser(IDBConnection conn, User user)
        {
            IDBParameter userName = _dal.CreateParameter("P_USERNAME", user.UserName);
            IDBParameter userID = _dal.CreateParameter("P_USERID", user.UserID);
            _dal.ExecuteSPQuery(conn, "CreateUser", userID, userName);
        }
        public DataSet Login(IDBConnection conn, string userID)
        {
            IDBParameter userIDParam = _dal.CreateParameter("UserID", userID);
            IDBParameter outParam = _dal.GetOutParameter();
            return _dal.ExecuteSPQuery(conn, "Login", userIDParam, outParam);
        }
        public void RemoveUser(IDBConnection conn, string userID)
        {
            IDBParameter userIDParam = _dal.CreateParameter("UserID", userID);
            _dal.ExecuteSPQuery(conn, "RemoveUser", userIDParam);
        }

        //validators
        public bool IsDocumentExists(IDBConnection conn, string docID)
        {
            var retval = true;
            DataSet ds = _dal.ExecuteQuery(conn, "select * from documents where document_id = '" + docID + "'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                retval = false;
            }

            return retval;
        }
        public bool IsMarkerExists(IDBConnection conn, string markerID)
        {
            var retval = true;
            DataSet ds = _dal.ExecuteQuery(conn, "select * from documents_markers where marker_id = '" + markerID + "'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                retval = false;
            }

            return retval;
        }
        public bool IsShareExists(IDBConnection conn, Share share)
        {
            var retval = true;
            DataSet ds = _dal.ExecuteQuery(conn, "SELECT * FROM SHARED_DOCUMENTS WHERE USER_ID = '" + share.UserID +
                                                 "' AND DOC_ID = '" + share.DocID + "'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                retval = false;
            }

            return retval;
        }
        public bool IsUserExists(IDBConnection conn, string userID)
        {
            var retval = true;
            DataSet ds = _dal.ExecuteQuery(conn, "SELECT * FROM USERS WHERE USERID = '" + userID + "'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                retval = false;
            }

            return retval;
        }
        public bool IsUserOwner(IDBConnection conn, string userID, string docID)
        {
            var retval = true;
            DataSet ds = _dal.ExecuteQuery(conn, "select * from documents where document_id = '" + docID + "' and user_id = '" +
                                                  userID + "'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                retval = false;
            }

            return retval;
            
        }

        

        

        
    }
}
