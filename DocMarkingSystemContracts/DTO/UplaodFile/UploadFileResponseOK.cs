using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Text;

namespace DocMarkingSystemContracts.DTO.UplaodFile
{
    public class UploadFileResponseOK :UploadFileResponse
    {
        public string DbPath { get; set; }
    }
}
