using PersonalBlog.Models;
using PersonalBlog.Models.EntityFrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using System.Web.UI.WebControls.WebParts;

namespace MVC_Empty.Views
{
    public class HomeController : Controller
    {
        // GET: Home
        NorthwindEntities1 db = new NorthwindEntities1();
        public ActionResult Index()
        {
            var model = db.ToDoLists.ToList();
            return View(model);
        }

        [Route("Home/GetId")]
    
        [HttpGet]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]

        public ActionResult New(ToDoList todo)
        {
            if (todo.TaskNo == 0) //for insert
            {
                db.ToDoLists.Add(todo);
            }
            else
            {
                var updateData = db.ToDoLists.Find(todo.TaskNo);
                if (updateData == null)
                {
                    return HttpNotFound();
                }
                updateData.Task = todo.Task;
                updateData.Priority = todo.Priority;
            }

            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Update(int id)
        {
            var model = db.ToDoLists.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View("New", model);
        }

        public ActionResult Delete(int id)
        {
            var delete = db.ToDoLists.Find(id);
            if (delete == null)
            {
                return HttpNotFound();
            }
            db.ToDoLists.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}