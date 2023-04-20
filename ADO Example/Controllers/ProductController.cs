using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADO_Example.DAL;
using ADO_Example.Models;

namespace ADO_Example.Controllers
{
    public class ProductController : Controller
    {

        Product_DAL _prodcutDAL = new Product_DAL();

        // GET: Product
        public ActionResult Index()
        {
            var productList = _prodcutDAL.GetAllProducts(); 
            if(productList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently Product Not Available in The Database.";
            }

            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
   
            var product = _prodcutDAL.GetById(id);
            if(product != null)
            {
                return View(product);
            }
            TempData["erroMessage"] = "Product Details not Found with Id : {id}";
            return RedirectToAction("Index");
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _prodcutDAL.InsertProduct(product);
                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Product details Saved Successfully....!";
                    }
                    else
                    {
                        TempData["ErroMessage"] = "Product is Alerady Exist in Database / Unable to save the Product Details....!";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,Product product)
        {
            try
            {
                // TODO: Add delete logic here
                bool check = false;

                
                    check = _prodcutDAL.CheckDelete(id);
                    if (check)
                    {
                        TempData["SuccessMessage"] = "Product details Delete Successfully....!";
                    }
                    else
                    {
                        TempData["erroMessage"] = "Product details Not Delete Successfully....!";
                    }
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
