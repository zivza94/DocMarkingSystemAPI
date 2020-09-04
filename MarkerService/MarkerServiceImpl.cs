using DALContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO;
using DocMarkingSystemContracts.DTO.Markers;
using DocMarkingSystemContracts.Interfaces;

namespace MarkerService
{
    [Register(Policy.Transient,typeof(IMarkerService))]
    public class MarkerServiceImpl:IMarkerService
    {
        IInfraDAL _dal;
        IDBConnection _conn;
        public MarkerServiceImpl(IInfraDAL dal)
        {
            _dal = dal;
        }

        public void Connect(string connStr)
        {
            _conn = _dal.Connect(connStr);
        }

        public async Task<Response> CreateMarker(CreateMarkerRequest request)
        {
            Response retval = new CreateMarkerResponseInvalidDocID(request);
            if (!await IsDocumentExists(request.DocID))
            {
                retval = new CreateMarkerResponseInvalidDocID(request);
            }
            else if (!await IsUserExists(request.UserID))
            {
                retval = new CreateMarkerResponseInvalidUserID(request);
            }else if (!ValidateMarkerType(request.MarkerType))
            {
                retval = new CreateMarkerResponseInvalidMarkerType(request);
            }
            else
            {
                IDBParameter markerID = _dal.CreateParameter("P_MARKER_ID", Guid.NewGuid().ToString());
                IDBParameter docID = _dal.CreateParameter("P_DOC_ID", request.DocID);
                IDBParameter userID = _dal.CreateParameter("P_USER_ID", request.UserID);
                IDBParameter backColor = _dal.CreateParameter("P_BACK_COLOR", request.BackColor);
                IDBParameter foreColor = _dal.CreateParameter("P_FORE_COLOR", request.ForeColor);
                IDBParameter markerType = _dal.CreateParameter("P_MARKR_TYPE",request.MarkerType);
                IDBParameter markerx = _dal.CreateParameter("P_MARKER_X", request.MarkerLocation.PointX);
                IDBParameter markery = _dal.CreateParameter("P_MARKER_Y", request.MarkerLocation.PointY);
                IDBParameter markerxRadius = _dal.CreateParameter("P_MARKER_X_RADIUS", request.MarkerLocation.RadiusX);
                IDBParameter markeryRadius = _dal.CreateParameter("P_MARKER_Y_RADIUS", request.MarkerLocation.RadiusY);
                try
                {
                    _dal.ExecuteSPQuery(_conn, "CREATEMARKER", docID, markerID, markerType, markerx, markery,
                        markerxRadius,
                        markeryRadius, foreColor, backColor, userID);
                    retval = new CreateMarkerResponseOK(request);
                }
                catch(Exception ex)
                {
                    retval = new AppResponseError("Error in create marker");
                }
            }

            return retval;
        }

        public async Task<Response> GetMarkers(GetMarkersRequest request)
        {
            //TODO test
            Response retval = new GetMarkersResponseInvalidDocID(request);
            if (await IsDocumentExists(request.DocID))
            {
                try
                {
                    List<Marker> markers = new List<Marker>();
                    IDBParameter docID = _dal.CreateParameter("P_DOC_ID", request.DocID);
                    IDBParameter outParam = _dal.GetOutParameter();
                    DataSet ds = _dal.ExecuteSPQuery(_conn, "GETMARKERS", docID,outParam);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        markers.Add(CreateMarkerFromRow(row));
                    }
                    retval = new GetMarkersResponseOk(markers);
                }
                catch (Exception ex)
                {
                    retval = new AppResponseError("Error in get markers");
                }
            }

            return retval;
        }

        public async Task<Response> RemoveMarker(RemoveMarkerRequest request)
        {
            Response retval = new RemoveMarkerResponseInvalidID(request);
            if (await IsMarkerExists(request.MarkerID))
            {
                IDBParameter markerID = _dal.CreateParameter("P_MARKER_ID", request.MarkerID);
                try
                {
                    _dal.ExecuteSPQuery(_conn, "REMOVEMARKER", markerID);
                    retval = new RemoveMarkerResponseOK(request);
                }
                catch(Exception ex)
                {
                    retval = new AppResponseError("Error in remove marker");
                }
            }
            return retval;
        }

        private Marker CreateMarkerFromRow(DataRow row)
        {
            Marker marker = new Marker()
            {
                BackColor = (string)row["BACK_COLOR"],DocID = (string)row["DOC_ID"],ForeColor = (string)row["FORE_COLOR"],
                MarkerID = (string)row["MARKER_ID"],MarkerLocation = new Location()
                {
                    PointX = Double.Parse(row["MARKER_X"].ToString()) , PointY = Double.Parse(row["MARKER_Y"].ToString()),
                    RadiusX = Double.Parse(row["MARKER_X_RADIUS"].ToString()), RadiusY = Double.Parse(row["MARKER_Y_RADIUS"].ToString())
                },UserID = (string)row["USER_ID"],MarkerType = (string)row["MARKER_TYPE"]
            };
            return marker;
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

        private async Task<bool> IsMarkerExists(string markerID)
        {
            var retval = true;
            DataSet ds = _dal.ExecuteQuery(_conn, "select * from documents_markers where marker_id = '" + markerID + "'");
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

        private bool ValidateMarkerType(string markerType)
        {
            var retval = false;
            if (markerType == "Rectangle" || markerType == "Ellipse")
            {
                retval = true;
            }

            return retval;
        }
    }
}
