using CoreMVCCodeFirst_1.Models.ContextClasses;
using CoreMVCCodeFirst_1.Models.Entities;
using CoreMVCCodeFirst_1.Models.ViewModels.CategoryPageVms;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVCCodeFirst_1.Controllers
{
    public class CategoryController : Controller
    {
        MyContext _db;

        public CategoryController(MyContext db)
        {
            _db = db;
        }


        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category) //Alternatif olarak parametre isminin kısıtlı olmasını istemiyorsanız parametre tipinin basına [Bind(Prefix ="Category")] diyebilirsiniz
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            TempData["message"] = $"{category.CategoryName} isimli kategori basarılı bir şekilde eklenmiştir";
            return RedirectToAction("GetCategories"); //Burada kişiyi Action'a yönlendirmeye özen gösterin...View'a gönderirseniz ilgili View'in kendine has olan Action'i calısmayacagı icin sorun yasarsınız...
        }

        public IActionResult GetCategories()
        {
            GetCategoriesPageVM gcVm = new()
            {
                Categories = _db.Categories.ToList()
            };
            return View(gcVm);
        }


        public IActionResult UpdateCategory(int id)
        {
            UpdateCategoryPageVM ucVm = new()
            {
                Category = _db.Categories.Find(id)
            };
            return View(ucVm);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            Category original = _db.Categories.Find(category.ID);
            original.CategoryName = category.CategoryName;
            original.Description = category.Description;
            _db.SaveChanges();
            TempData["message"] = "Güncelleme basarılı";
            return RedirectToAction("GetCategories");
        }


        public IActionResult DeleteCategory(int id)
        {
            _db.Categories.Remove(_db.Categories.Find(id));
            _db.SaveChanges();
            TempData["message"] = "Silme işlemi basarılı bir şekilde gerçekleşti";
            return RedirectToAction("GetCategories");
        }
    }
}
