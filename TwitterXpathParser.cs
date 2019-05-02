using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebParser
{
    class TwitterXpathParser
    {
        public static object Parse(string filename)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(filename);
            var root = doc.DocumentNode;

            var tweets = root.SelectNodes("//div[starts-with(@class, 'tweet ')]");

            List<object> result = new List<object>();

            foreach (var node in tweets)
            {
                result.Add(new
                {
                    Timestamp = node.SelectSingleNode(".//span[starts-with(@class, '_timestamp js-short-timestamp')]").InnerText,
                    Handle = node.SelectSingleNode(".//span[starts-with(@class, 'username ')]/b").InnerText,
                    Content = node.SelectSingleNode(".//div[@class='js-tweet-text-container']/p").InnerText,
                    Favourites = node.SelectSingleNode(".//div[contains(@class, 'ProfileTweet-action--favorite')]/button/span/span").InnerText,
                    Retweets = node.SelectSingleNode(".//div[contains(@class, 'ProfileTweet-action--retweet')]/button/span/span").InnerText,
                    Replies = node.SelectSingleNode(".//div[contains(@class, 'ProfileTweet-action--reply')]/button/span/span").InnerText,
                });
            }

            return result.ToArray();
        }
    }
}
