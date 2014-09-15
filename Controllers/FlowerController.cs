using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Flw.Models;

namespace Flw.Controllers
{

    public class PageInfo
    {
        public int pageCount;
        public List<Entity1> lst = new List<Entity1>();
    }


    public class FlowerController : Controller
    {

        public ApplicationDbContext context = new ApplicationDbContext();

        //Model1Container dbContext = new Model1Container();
       
 
        public ActionResult FlowerList(int id)
        {
            PageInfo pi = new PageInfo();
            List<Entity1> list = new List<Entity1>();
             int pageSize = 2;
             Array arr = context.Entities.ToArray();
             pi.pageCount = arr.Length / pageSize;
             for (int i = pageSize * id; i < pageSize * (id + 1); i++)
             {
                 if(i<arr.Length)
                 list.Add((Entity1)arr.GetValue(i));
             }
             pi.lst = list;
             return View(pi);
        }

        [HttpPost]
        public ActionResult Search(string text)
        {
            List<Entity1> list = new List<Entity1>();
            foreach (Entity1 item in context.Entities)
            {
                if (item.name.Contains(text))
                {
                    list.Add(item);
                    break;
                }
            }
            return View("Res", list);
        }

        public ActionResult AddFlower(string name, string color, string availability)
        {
             Entity1 entity = new Entity1();
            entity.name = name;
            entity.color = color;
            entity.availability = availability;
            if (name != null)
            {
                context.Entities.Add(entity);
                context.SaveChanges();
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            foreach (Entity1 item in context.Entities)
            {
                if (item.Id == id)
                {
                    context.Entities.Remove(item);
                    break;
                }
            }
            context.SaveChanges();
            return View("FlowerList", context.Entities);
        }
        

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Entity1 e = null;
            foreach (Entity1 item in context.Entities)
                {
                    if (item.Id == id)
                    {
                        e = item;
                        break;
                    }
                }

            if (ModelState.IsValid)
            {
                UpdateModel(e, collection);
                context.SaveChanges();
                return RedirectToAction("FlowerList", context.Entities);
            }
            else return View(e);
        }


        public ActionResult Edit(int Id)
        {
            Entity1 e = null;
            foreach (Entity1 item in context.Entities)
                {
                    if (item.Id == Id)
                    {
                        e = item;
                        break;
                    }
                }
            return View(e);
        }
	}
}