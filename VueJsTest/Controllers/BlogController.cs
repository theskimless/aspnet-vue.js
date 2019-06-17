using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueJsTest.Models;

namespace VueJsTest.Controllers
{
    public class BlogController : Controller
    {
        AppDbContext _dbContext;
        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(string title, string rubricsId, string text)
        {
            var post = new Post { Title = title, Data = text, Date = DateTime.Now };
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();

            foreach(var rubric in rubricsId.Split(","))
            {
                _dbContext.PostRubrics.Add(new PostRubric { PostId = post.Id, RubricId = int.Parse(rubric) });
            }
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateRubric(string name)
        {
            _dbContext.Rubrics.Add(new Rubric { Name = name });
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        //RETURN DATA
        public string SendJson()
        {
            var indexView = new IndexView { Rubrics = _dbContext.Rubrics.ToList(), Posts = _dbContext.Posts.ToList(), PostRubrics = _dbContext.PostRubrics.ToList() };
            return JsonConvert.SerializeObject(indexView);
        }
    }

    public class IndexView
    {
        public List<Rubric> Rubrics { get; set; }
        public List<Post> Posts { get; set; }
        public List<PostRubric> PostRubrics { get; set; }
    }
}
