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
        IDocMarkingSystemDAL _dal;
        IDBConnection _conn;
        private IMarkerWebSocket _webSocket;
        public MarkerServiceImpl(IDocMarkingSystemDAL dal, IMarkerWebSocket webSocket)
        {
            _dal = dal;
            _webSocket = webSocket;
        }

        public void Connect(string connStr)
        {
            _conn = _dal.Connect(connStr);
        }

        public async Task<Response> CreateMarker(CreateMarkerRequest request)
        {
            Response retval = new CreateMarkerResponseInvalidDocID(request);
            if (!_dal.IsDocumentExists(_conn,request.DocID))
            {
                retval = new CreateMarkerResponseInvalidDocID(request);
            }
            else if (!_dal.IsUserExists(_conn ,request.UserID))
            {
                retval = new CreateMarkerResponseInvalidUserID(request);
            }else if (!ValidateMarkerType(request.MarkerType))
            {
                retval = new CreateMarkerResponseInvalidMarkerType(request);
            }
            else
            {
                var id = Guid.NewGuid().ToString();
                Marker marker = new Marker()
                {
                    BackColor = request.BackColor,DocID =  request.DocID,ForeColor = request.ForeColor,MarkerID = id
                    ,MarkerLocation = request.MarkerLocation,
                    MarkerType =  request.MarkerType,UserID = request.UserID
                };
                try
                {
                    _dal.CreateMarker(_conn,marker);
                    retval = new CreateMarkerResponseOK(request);
                    await _webSocket.SendNewMarker(marker);
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
            if (_dal.IsDocumentExists(_conn,request.DocID))
            {
                try
                {
                    List<Marker> markers = new List<Marker>();
                    DataSet ds = _dal.GetMarkers(_conn, request.DocID);
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
            if (_dal.IsMarkerExists(_conn,request.MarkerID))
            {
                
                try
                {
                    _dal.RemoveMarker(_conn,request.MarkerID);
                    retval = new RemoveMarkerResponseOK(request);
                    await _webSocket.SendRemoveMarker(request.MarkerID);
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
        /*private async Task<bool> IsDocumentExists(string docID)
        {
            var retval = true;
            DataSet ds = _dal.ExecuteQuery(_conn, "select * from documents where document_id = '" + docID + "'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                retval = false;
            }

            return retval;
        }*/

        /*private async Task<bool> IsMarkerExists(string markerID)
        {
            var retval = true;
            DataSet ds = _dal.ExecuteQuery(_conn, "select * from documents_markers where marker_id = '" + markerID + "'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                retval = false;
            }

            return retval;
        }*/
        /*private async Task<bool> IsUserExists(string userID)
        {
            var retval = true;
            DataSet ds = _dal.ExecuteQuery(_conn, "SELECT * FROM USERS WHERE USERID = '" + userID + "'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                retval = false;
            }

            return retval;
        }*/

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
