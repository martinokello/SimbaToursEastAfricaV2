using System;
using Microsoft.AspNetCore.Http;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class EmailDao { 
        public string EmailBody { get; set; }
        public string EmailSubject { get; set; }
        public string EmailTo { get; set; }
        public string EmailFrom { get; set; }
        public IFormFile Attachment { get; set; }
    }
}
