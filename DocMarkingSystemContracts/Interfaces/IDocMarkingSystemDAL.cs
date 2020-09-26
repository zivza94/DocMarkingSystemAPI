using DALContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DocMarkingSystemContracts.DTO.Documents;
using DocMarkingSystemContracts.DTO.Markers;
using DocMarkingSystemContracts.DTO.Sharing;
using DocMarkingSystemContracts.DTO.Users;

namespace DocMarkingSystemContracts.Interfaces
{
    public interface IDocMarkingSystemDAL
    {
        //for all
        IDBConnection Connect(string strConnection);

        //user
        void CreateUser(IDBConnection conn,User user);
        DataSet Login(IDBConnection conn, string userID);
        void RemoveUser(IDBConnection conn, string userID);


        //document
        void CreateDocument(IDBConnection conn, Document document);
        DataSet GetDocument(IDBConnection conn, string docID);
        DataSet GetDocuments(IDBConnection conn, string userID);

        void RemoveDocument(IDBConnection conn, string docID);
        //marker
        void CreateMarker(IDBConnection conn, Marker marker);
        DataSet GetMarkers(IDBConnection conn, string docID);
        void RemoveMarker(IDBConnection conn, string markerID);
        //share
        void CreateShare(IDBConnection conn, Share share);
        DataSet GetSharedDocuments(IDBConnection conn, string userID);
        DataSet GetSharedUsers(IDBConnection conn, string docID);
        void removeShare(IDBConnection conn, Share share);

        //validators
        bool IsUserExists(IDBConnection conn, string userID);
        bool IsDocumentExists(IDBConnection conn, string docID);
        bool IsMarkerExists(IDBConnection conn, string markerID);
        bool IsUserOwner(IDBConnection conn, string userID, string docID);
        bool IsShareExists(IDBConnection conn, Share share);

    }
}
