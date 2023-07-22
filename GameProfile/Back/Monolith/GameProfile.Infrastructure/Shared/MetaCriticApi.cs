using HtmlAgilityPack;

namespace GameProfile.Infrastructure.Shared
{
    public class MetaCriticApi
    {
        public async Task<double> GetGameReview(string gameUrl)
        {

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = await web.LoadFromWebAsync(gameUrl);
          

            HtmlNode userRatingNode = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[1]/div[1]/div/div/div/div/div/div/div/div/div[1]/div[1]/div[3]/div/div/div[2]/div[1]/div[2]/div[1]/div/a/div");
            string userRating = userRatingNode?.InnerText;

            if (userRating == null || userRating.Equals("tbd", StringComparison.OrdinalIgnoreCase))
            {
                return -1;
            }

            return double.Parse(userRating);
        }
    }
}
