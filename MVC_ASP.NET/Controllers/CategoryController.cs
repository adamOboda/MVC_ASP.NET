using Microsoft.AspNetCore.Mvc;
using MVC_ASP.NET.Data;
using MVC_ASP.NET.Models;

namespace MVC_ASP.NET.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display Order cannot be the same value");
            }
            if (ModelState.IsValid)
            {
                // Dodawanie objektów do bazy danych i save
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            //id cannot be 0/null so it returns NotFound()
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // if id is valid:
            var categoryFromDb = _db.Categories.Find(id);
            //var cateegoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var cateegoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display Order cannot be the same value");
            }
            if (ModelState.IsValid)
            {
                // Update objektów
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category edited successfully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // if id is valid:
            var categoryFromDb = _db.Categories.Find(id);

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            
            return RedirectToAction("Index");
        }

    }
}
