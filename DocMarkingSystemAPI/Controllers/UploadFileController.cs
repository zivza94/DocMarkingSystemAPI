using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using DocMarkingSystemContracts.DTO.UplaodFile;
using Microsoft.Extensions.Configuration;

namespace DocMarkingSystemAPI.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private string _url;
        public UploadFileController(IConfiguration conf)
        {
            _url = conf.GetValue<string>("Kestrel:EndPoints:Https:Url");
        }
        [HttpGet]
        public async Task<Response> TestGet()
        {
            return new Response();
        }
        [HttpPost]
        public async Task<Response> UploadFile()
        {
            Response retval;
            try
            {
                //var file = request.FormData.Files[0];
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').Replace(" ","_");
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    //dbPath = Path.Combine(_url, dbPath);
                    using (var stream = new FileStream(fullPath,FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    retval = new UploadFileResponseOK(){DbPath = dbPath};
                }
                else
                {
                    retval = new UploadFileResponseNoData();
                }
            }
            catch (Exception ex)
            {
                retval = new AppResponseError("Error in upload file," + ex.Message);
            }

            return retval;

        }
    }
}