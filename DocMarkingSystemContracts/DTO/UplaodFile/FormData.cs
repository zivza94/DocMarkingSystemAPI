using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace DocMarkingSystemContracts.DTO.UplaodFile
{
    public class FormData
    {
        public int ID { get; set; }
        public IFormFile FileToUpload { get; set; }
    }
}
