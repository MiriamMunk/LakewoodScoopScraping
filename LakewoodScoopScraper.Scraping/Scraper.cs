using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LakewoodScoopScraper.Scraping
{
    public static class Scraper
    {
        public static List<Post> Scrape()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip |
                System.Net.DecompressionMethods.Deflate
            };
            using var client = new HttpClient(handler);
            var url = "https://www.thelakewoodscoop.com/";
            var html = client.GetStringAsync(url).Result;
            return ParseLakewoodScoopHtml(html);
        }

        private static List<Post> ParseLakewoodScoopHtml(string html)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);
            var resultDivs = document.QuerySelectorAll(".post");
            var posts = new List<Post>();
            foreach (var div in resultDivs)
            {
                var post = new Post();

                var title = div.QuerySelector("h2");
                var link = title.QuerySelector("a");
                if (title != null)
                {
                    post.Title = title.TextContent;
                    post.Link = link.Attributes["href"].Value;
                }

                var image = div.QuerySelector(".aligncenter");
                if (image == null)
                {
                    continue;
                }
                post.ImageUrl = image.Attributes["src"].Value;

                var text = div.QuerySelector("p");
                if (text == null)
                {
                    continue;
                }
                post.Text = text.TextContent;

                var comments = div.QuerySelector(".backtotop");
                if (comments != null)
                {
                    post.Comments = comments.TextContent;
                }

                posts.Add(post);
            }

            return posts;
        }
    }
}
