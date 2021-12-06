using FluentValidation;
using Northwind.Business.Abstract;
using Northwind.Business.Utilities;
using Northwind.Business.ValidationRules.FluentValidation;
using Northwind.DataAccess.Abstract;
using Northwind.DataAccess.Concrete;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;

using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal productDal;
        public ProductManager(IProductDal _productDal)
        {
            productDal = _productDal;
        }

        public List<Product> GelAll()
        {
            return productDal.GetAll();
        }

        public List<Product> GetProductByCategory(int categoryId)
        {
            return productDal.GetAll(p => p.CategoryID == categoryId);
        }

        public List<Product> GetProductsByProductName(string productname)
        {
            return productDal.GetAll(p => p.ProductName.ToLower().Contains(productname.ToLower()));
        }
        public void Add(Product product)
        {
            ValidationTool.Validate(new ProductValidator(), product);
            productDal.Add(product);
        }
        public void Update(Product product)
        {
            ValidationTool.Validate(new ProductValidator(), product);
            productDal.Update(product);
        }
        public void Delete(Product product)
        {
            try
            {
                productDal.Delete(product);
            }
            catch
            {

                throw new Exception("Silme Gerçekleşemedi");
            }

        }

    }
}
