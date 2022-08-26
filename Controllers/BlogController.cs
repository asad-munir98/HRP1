using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRP1.Models;
using System.Security.Cryptography.Xml;
using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace HRP1.Controllers
{
    public class BlogController : Controller
    {
        // GET: BlogController
        public ActionResult Index()
        {
            const string posts = "https://jsonplaceholder.typicode.com/posts";
            const string comments = "https://jsonplaceholder.typicode.com/comments";

            BlogViewModel blogs = new BlogViewModel();

            const string urlParameters = "?api_key=123";

           // ViewBag.url = URL;
            //var db = null;
            List<BlogViewModel> allBlogs = new List<BlogViewModel>();

            HttpClient clientPost = new HttpClient();
            clientPost.BaseAddress = new Uri(posts);

            // Add an Accept header for JSON format.
            clientPost.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage responsePost = clientPost.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (responsePost.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = responsePost.Content.ReadAsAsync<IEnumerable<BlogViewModel>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                foreach (var d in dataObjects.Take(10))
                {
                     blogs = new BlogViewModel();

                    blogs.id = d.id;
                    blogs.Title = d.Title;
                    blogs.Body = d.Body;
                    blogs.postIdForComments = blogs.id;

                    allBlogs.Add(blogs);

                }
            }
            HttpClient clientComments = new HttpClient();
            clientComments.BaseAddress = new Uri(comments);
            HttpResponseMessage responseComment = clientComments.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (responseComment.IsSuccessStatusCode)
            {
                // Parse the response body.
                var commentsData = responseComment.Content.ReadAsAsync<IEnumerable<BlogViewModel>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                foreach (var d in commentsData.Take(10))
                {
                    blogs.postId = d.postId;
                    blogs.Name = d.Name;
                    blogs.commentbody = d.Body;
                    blogs.email = d.email;
                    blogs.commentID = d.id;

                    allBlogs.Add(blogs);

                }
            }

            return View(allBlogs);
        }

    }
}
