using BookSearchBot.Models;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BookSearchBot.API
{
    public class GoodReadsApiWrapper : IApiWrapper
    {
        private HttpClient client;

        public string SearchType { get; set; }

        public GoodReadsApiWrapper()
        {
            client = new HttpClient();
        }

        public async Task<GoodReadsResponse> GetSearchResponse(string input)
        {
            var baseUrl = "https://www.goodreads.com/search/index.xml";
            var key = "?key=EnwsQAOl0VnrGqslXgy5zA";

            var response = await client.GetAsync($"{baseUrl}{key}&q={input}");

            var content = await response.Content.ReadAsStringAsync();

            var serializer = new XmlSerializer(typeof(GoodReadsResponse));

            using (var stream = new StringReader(content))
            {
                return (GoodReadsResponse)serializer.Deserialize(stream);
            }
        }
    }
}
