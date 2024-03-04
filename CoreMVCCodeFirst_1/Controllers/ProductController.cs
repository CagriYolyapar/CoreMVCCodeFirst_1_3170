using CoreMVCCodeFirst_1.Models.ContextClasses;
using CoreMVCCodeFirst_1.Models.Entities;
using CoreMVCCodeFirst_1.Models.ViewModels.ProductPageVms;
using CoreMVCCodeFirst_1.Models.ViewModels.PureVMs;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVCCodeFirst_1.Controllers
{
    public class ProductController : Controller
    {
        MyContext _db;
        public ProductController(MyContext db)
        {
            _db = db;
        }
        
        public IActionResult GetProducts()
        {
           

            List<ProductVM> products =  _db.Products.Select(x => new ProductVM
            {
                ID = x.ID,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                CategoryName = x.Category.CategoryName
            }).ToList();

            GetProductsPageVM gpVm = new GetProductsPageVM()
            {
                Products = products
            };

            return View(gpVm);
        }

        public IActionResult CreateProduct()
        {
            CreateUpdateProductPageVM cpVm = new CreateUpdateProductPageVM()
            {
                Categories = _db.Categories.ToList()
            };
            return View(cpVm);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return RedirectToAction("GetProducts");
        }

        public IActionResult UpdateProduct(int id)
        {
            CreateUpdateProductPageVM cuPVm = new()
            {
                Product = _db.Products.Find(id),
                Categories = _db.Categories.ToList()
            };

            return View(cuPVm);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            Product originalProduct = _db.Products.Find(product.ID);
            originalProduct.ProductName = product.ProductName;  
            originalProduct.UnitPrice = product.UnitPrice;
            originalProduct.CategoryID = product.CategoryID;
            _db.SaveChanges();
            return RedirectToAction("GetProducts");
        }

        public IActionResult DeleteProduct(int id)
        {
            _db.Products.Remove(_db.Products.Find(id));
            _db.SaveChanges();
            return RedirectToAction("GetProducts");
        }
    }
}
