using HackerNewsApp.Models;
using HackerNewsApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HackerNewsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHackerNewsService _hackerNewsService;

        public HomeController(IHackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        public async Task<IActionResult> Index()
        {
            var bestStories = await _hackerNewsService.GetBestStoriesAsync();
            return View(bestStories);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> StoryDetails(int storyId)
        {
            var storyResponse = await _hackerNewsService.GetStoryDetailsAsync(storyId);
            if (storyResponse != null)
            {
                return View(storyResponse);
            }
            else
            {
                // Handle error: Story not found
                return NotFound();
            }
        }
    }
}
