using HackerNewsApp.Models;

namespace HackerNewsApp.Services
{
    public interface IHackerNewsService
    {
        Task<IEnumerable<HackerNewsItem>> GetBestStoriesAsync();
        Task<HackerNewsItem> GetStoryDetailsAsync(int storyId);

    }
}
