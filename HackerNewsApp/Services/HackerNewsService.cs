using HackerNewsApp.Models;
using System.Text.Json;

namespace HackerNewsApp.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly HttpClient _httpClient;

        public HackerNewsService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<HackerNewsItem>> GetBestStoriesAsync()
        {
            var response = await _httpClient.GetAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var bestStoryIds = JsonSerializer.Deserialize<IEnumerable<int>>(content);

            var bestStories = new List<HackerNewsItem>();
            foreach (var id in bestStoryIds.Take(10)) // Example: getting top 10 best stories
            {
                var storyResponse = await _httpClient.GetAsync($"https://hacker-news.firebaseio.com/v0/item/{id}.json");
                storyResponse.EnsureSuccessStatusCode();
                var storyContent = await storyResponse.Content.ReadAsStringAsync();
                var story = JsonSerializer.Deserialize<HackerNewsItem>(storyContent);
                if (story != null)
                {
                    bestStories.Add(story);
                }
            }

            return bestStories;
        }
        public async Task<HackerNewsItem> GetStoryDetailsAsync(int storyId)
        {
            var response = await _httpClient.GetAsync($"https://hacker-news.firebaseio.com/v0/item/{storyId}.json");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var story = JsonSerializer.Deserialize<HackerNewsItem>(content);

            return story;
        }
    }
}
