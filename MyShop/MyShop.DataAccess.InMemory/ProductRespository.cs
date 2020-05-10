using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core;
using MyShop.Core.Models;
namespace MyShop.DataAccess.InMemory
{
    public class ProductRespository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductsCategories> products = new List<ProductsCategories>();

        public ProductRespository()
        {
            products = cache["products"] as List<ProductsCategories>;
            if(products==null)
            {
                products = new List<ProductsCategories>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(ProductsCategories p)
        {
            products.Add(p);
        }

        public void Update(ProductsCategories product)
        {
            ProductsCategories productToUpdate = products.Find(p => p.Id == product.Id);

            if(productToUpdate!=null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product no found");
            }
        }

        public ProductsCategories Find(string Id)
        {
            ProductsCategories product = products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product no found");
            }
        }

        public IQueryable<ProductsCategories>Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductsCategories productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product no found");
            }
        }
    }
}
