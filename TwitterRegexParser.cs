using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace WebParser
{
    class TwitterRegexParser
    {
        public static object Parse(string filename)
        {
            string txt = File.ReadAllText(filename);

            var content = new Regex("<div class=\"js-tweet-text-container\">[\\s\\S]*?<p class=\"TweetTextSize.+?>([\\s\\S]*?)</p>", RegexOptions.Multiline).Matches(txt);
            var handle = new Regex("<b>(.+?)</b>[\\W]*?</span>[\\W]*?</a>[\\W]*?<small class=\"time").Matches(txt);
            var timestamp = new Regex("<span class=\"_timestamp js-short-timestamp.+?>(.+?)</span>").Matches(txt);
            var reply = new Regex("title=\"Reply\"[\\s\\S]*?<span class=\"ProfileTweet-actionCountForPresentation\".+?>(\\d*?)</span>").Matches(txt);
            var retweets = new Regex("title=\"Retweet\"[\\s\\S]*?<span class=\"ProfileTweet-actionCountForPresentation\".+?>(\\d*?)</span>").Matches(txt);
            var favourites = new Regex("title=\"Like\"[\\s\\S]*?<span class=\"ProfileTweet-actionCountForPresentation\".+?>(\\d*?)</span>").Matches(txt);

            List<object> result = new List<object>();

            for (int i = 0; i < timestamp.Count; i++)
            {
                result.Add(new
                {
                    Handle = handle[i].Groups[1].Value,
                    Content = content[i].Groups[1].Value,
                    Timestmap = timestamp[i].Groups[1].Value,
                    Replies = reply[i].Groups[1].Value,
                    Retweets = retweets[i].Groups[1].Value,
                    Favourites = favourites[i].Groups[1].Value
                });
            }

            return result.ToArray();
        }
    }
}
