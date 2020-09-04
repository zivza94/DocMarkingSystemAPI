using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO.Sharing;

namespace DocMarkingSystemContracts.Interfaces
{
    public interface ISharingService
    {
        public void Connect(string connStr);
        public Task<Response> CreateShare(CreateShareRequest request);
        public Task<Response> GetSharedDocuments(GetSharedDocumentsRequest request);
        public Task<Response> GetSharedUsers(GetSharedUsersRequest request);
        public Task<Response> RemoveShare(RemoveShareRequest request);

    }
}
