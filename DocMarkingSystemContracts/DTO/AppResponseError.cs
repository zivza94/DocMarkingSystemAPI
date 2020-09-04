using System;
using System.Collections.Generic;
using System.Text;
using DIContract;

namespace DocMarkingSystemContracts.DTO
{
    public class AppResponseError :Response
    {
        public AppResponseError(string msg)
        {
            Message = msg;
        }

        public string Message { get; }
    }
}
