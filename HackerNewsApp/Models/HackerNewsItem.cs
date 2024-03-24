using System.Text.Json.Serialization;

namespace HackerNewsApp.Models
{
    public class HackerNewsItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        public string Url { get; set; }

        public string Score { get; set; }
    }
}
