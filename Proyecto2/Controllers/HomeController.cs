using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proyecto2.Models;

namespace Proyecto2.Controllers
{
    public class HomeController : Controller
    {
        public static List<MovieModel> movies = null;

        private readonly ILogger<HomeController> _logger;

        private static async Task<List<MovieModel>> GetMovies(String name)
        {
            List<MovieModel> _movies = new List<MovieModel>();

            HttpClient client = new HttpClient();
            HttpRequestMessage request = null;
            if (String.IsNullOrEmpty(name))
            {
                request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api.themoviedb.org/3/movie/popular?language=es-US&page=1"),
                    Headers =
                    {
                        {"accept", "application/json" },
                        {"Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJiZTBiMjBlMjMwZjQ1OGQxOTY2ODIwYmUzZTA5MWZiMyIsIm5iZiI6MTc1NjcyNjAzNS4yNjgsInN1YiI6IjY4YjU4MzEzZmNkNDdjMmVkOTNmMDVhNSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.rvbbEySKWdWjY0G5wNhSSgRHOcbzHiwl8aBBFhOGZLk" },
                    },
                };
            } else
            {
                request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://api.themoviedb.org/3/search/movie?query={name}&language=es-US&page=1"),
                    Headers =
                    {
                        {"accept", "application/json" },
                        {"Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJiZTBiMjBlMjMwZjQ1OGQxOTY2ODIwYmUzZTA5MWZiMyIsIm5iZiI6MTc1NjcyNjAzNS4yNjgsInN1YiI6IjY4YjU4MzEzZmNkNDdjMmVkOTNmMDVhNSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.rvbbEySKWdWjY0G5wNhSSgRHOcbzHiwl8aBBFhOGZLk" },
                    },
                };
            }

            PageResultModel _page = new PageResultModel();

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                _page = JsonConvert.DeserializeObject<PageResultModel>(body);
            }

            _movies = _page.results;

            return _movies;
        }

        private static async Task<MovieModel> GetDetails(int id)
        {
            MovieModel _movie = new MovieModel();

            HttpClient client = new HttpClient();
            HttpRequestMessage request = null;
            request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.themoviedb.org/3/movie/{id}?language=es-US"),
                Headers =
                    {
                        {"accept", "application/json" },
                        {"Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJiZTBiMjBlMjMwZjQ1OGQxOTY2ODIwYmUzZTA5MWZiMyIsIm5iZiI6MTc1NjcyNjAzNS4yNjgsInN1YiI6IjY4YjU4MzEzZmNkNDdjMmVkOTNmMDVhNSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.rvbbEySKWdWjY0G5wNhSSgRHOcbzHiwl8aBBFhOGZLk" },
                    },
            };

            MovieModel _movieDetail = new MovieModel();

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                _movieDetail = JsonConvert.DeserializeObject<MovieModel>(body);
            }

            _movie = _movieDetail;

            return _movie;
        }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            Task<List<MovieModel>> _movies = GetMovies(null);
            movies = _movies.Result;
        }

        public IActionResult Index(String name)
        {
            if (String.IsNullOrEmpty(name))
            {
                Task<List<MovieModel>> _movies = GetMovies(null);
                movies = _movies.Result;
            } else
            {
                Task<List<MovieModel>> _movies = GetMovies(name);
                movies = _movies.Result;
            }

            return View(movies);
        }

        public IActionResult Details(int id)
        {
            foreach (MovieModel m in movies)
            {
                if (m.id == id) return View(m);
            }

            var _movieDetail = GetDetails(id);
            var _movie = _movieDetail.Result;

            return View(_movie);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
