using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.UplaodFile
{
    public class UploadFileRequest
    {
        public  IFormCollection FormData { get; set; }
    }
}
