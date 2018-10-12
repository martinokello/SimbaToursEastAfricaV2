using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.Web;
using System.Xml.Linq;
using Twitter;
using SimbaToursEastAfrica.Caching;
using Microsoft.Extensions.Options;

namespace MartinLayooInc.SocialMedia
{
    public class TwitterProfileFeed<T> where T : WidgetGroupItemList, new()
    {       //Holds url of Widget setup config
        private String Setup_Url;
        //Holds xslt for widget display
        private String Html_Url;

        //Holds xslt for error display on service not available.
        private String ErrorHtml_Url;

        private String Profile_Url;


        private string TwitterStatusBaseUrl = "https://api.twitter.com/1.1/statuses/user_timeline.json?";
        private string TwitterHomeBaseurl = "https://api.twitter.com/1.1/statuses/home_timeline.json?";
        //parameters
        //"include_entities=true&include_rts=true&screen_name=jordanharold&count=2"
        
        public IOptions<ApplicationConstants.twitterProfileFiguration> TwitterProfileFiguration { get; set; }
        public T GetFeeds()
        {
            var result = new T();

            var widgetControl = new TwitterFeedsManipulator<WidgetGroupItemList>(new GroupObject()
            {
                GroupActionUrl = TwitterProfileFiguration.Value.GroupActionUrl,
                GroupHeaderText = TwitterProfileFiguration.Value.GroupHeaderText,
                CacheKey = TwitterProfileFiguration.Value.cachKey,
                CacheTimeInSeconds = TwitterProfileFiguration.Value.cacheTimeSecs.ToString(),
                PageSize = TwitterProfileFiguration.Value.PageSize,
                GroupActionText = TwitterProfileFiguration.Value.GroupActionText
            });

            var oauthAthentication = new OauthAuthentication
            {
                ConsumerKey = TwitterProfileFiguration.Value.OauthConsumerKey,
                ConsumerSecret = TwitterProfileFiguration.Value.OauthConsumerSecret,
                TokenKey = TwitterProfileFiguration.Value.OauthToken,
                TokenSecret = TwitterProfileFiguration.Value.OauthTokenSecret
            };

            result = default(T);

            string profileTweetUrl = TwitterStatusBaseUrl +
                                       string.Format(
                                           "include_entities={0}&include_rts={1}&screen_name={2}&count={3}",
                                           true, true, TwitterProfileFiguration.Value.TwitterProfile, TwitterProfileFiguration.Value.PageSize);

            result = (T)widgetControl.GetProfileTwitterFeeds(profileTweetUrl, oauthAthentication);
            return result;
        }
    }
}