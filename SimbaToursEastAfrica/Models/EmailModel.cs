using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimbaToursEastAfrica.Infrastructure.TestMultipart.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Models
{
   // [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "body")]
    public class EmailModel
    {
        [FromForm]
        public string emailBody { get; set; }
        [FromForm]
        public string emailSubject { get; set; }
        [FromForm]
        public string emailTo { get; set; }
        [FromForm]
        public string emailFrom { get; set; }
        [FromForm]
        public IFormFile attachment { get; set; }
    }
}
