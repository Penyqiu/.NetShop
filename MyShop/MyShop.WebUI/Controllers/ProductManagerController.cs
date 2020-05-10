using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRespository context;
        ProductCategoryRespository productCategories;

        public ProductManagerController()
        {
            context = new ProductRespository();
            productCategories = new ProductCategoryRespository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductsCategories> products = context.Collection().ToList();

            return View(products);
        }
        public ActionResult Create()
        {
            ProductMenagerViewModel viewModel = new ProductMenagerViewModel();
           
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(ProductsCategories product)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductsCategories product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductMenagerViewModel viewModel = new ProductMenagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductsCategories product,string Id)
        {
            ProductsCategories productToEdit = context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            ProductsCategories productToDelete = context.Find(Id);

            if(productToDelete==null)
            {
                return HttpNotFound();

            }
            else
            {
                return View(productToDelete);
            }

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductsCategories productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();

            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }

        }
    }
}