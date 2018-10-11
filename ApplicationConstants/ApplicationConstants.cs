using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationConstants
{
    public class ApplicationConstants
    {
        public string BaseUrl { get; set; }
        public string BusinessEmail { get; set; }
        public string CancelUrl { get; set; }
        public string NotifyUrl { get; set; }
        public  string SuccessUrl { get; set; }
    }

    public class twitterProfileFiguration
    {
        public string GroupHeaderText { get; set; }
        public string GroupActionText { get; set; }
        public string GroupActionUrl { get; set; }
        public string TwitterProfile { get; set; }
        public string PageSize { get; set; }
        public string OauthToken { get; set; }
        public string OauthTokenSecret { get; set; }
        public string OauthConsumerKey { get; set; }
        public string OauthConsumerSecret { get; set; }
        public int cacheTimeSecs { get; set; }
        public string cachKey { get; set; }
    }
}
