using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using MvcWithoutEF.Models;

namespace MvcWithoutEF.Controllers
{
    public class ProductController : Controller
    {
        string Constr = @"Data Source=Reverside-pc;Initial Catalog=SeanTeqInventory;Integrated Security=True;Pooling=False";
        ProductModel pm = new ProductModel();
        // GET: Product
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dt = new DataTable();

            using (SqlConnection objCon = new SqlConnection(Constr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Product", objCon);
               
                da.Fill(dt);
            }
                return View(dt);
        }

      


        
        [HttpGet]
        public ActionResult Create()
        {
             
            return View(pm);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel prd)
        {
            using (SqlConnection con = new SqlConnection(Constr))
            {
                con.Open();
                string Qstr = "INSERT INTO Product VALUES(@ProductName,@Price,@Count)";

                SqlCommand cmd = new SqlCommand(Qstr, con);
                cmd.Parameters.AddWithValue("@ProductName", prd.ProductName);
                cmd.Parameters.AddWithValue("@Price", prd.Price);
                cmd.Parameters.AddWithValue("@Count", prd.Count);
                cmd.ExecuteNonQuery();

            }

                return RedirectToAction("Index");
           
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel pd = new ProductModel();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(Constr))
            {
                con.Open();
                string Qrystr = "SELECT * FROM Product WHERE ProductId = @ProductId";
                SqlDataAdapter da = new SqlDataAdapter(Qrystr, con);
                da.SelectCommand.Parameters.AddWithValue("@ProductId", id);
                da.Fill(dt);
            }

            if (dt.Rows.Count == 1)
            {
                pd.ProductId = Convert.ToInt32(dt.Rows[0][0].ToString());
                pd.ProductName = dt.Rows[0][1].ToString();
                pd.Price = Convert.ToDecimal(dt.Rows[0][2].ToString());
                pd.Count = Convert.ToInt32(dt.Rows[0][3].ToString());
                return View(pd);

            }
            else

                return RedirectToAction("Index");
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel prd)
        {
            using (SqlConnection con = new SqlConnection(Constr))
            {
                con.Open();
                string Qstr = " UPDATE Product SET ProductName = @ProductName,Price = @Price,Count = @Count WHERE ProductId = @ProductId ";

                SqlCommand cmd = new SqlCommand(Qstr, con);
                cmd.Parameters.AddWithValue("@ProductId", prd.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", prd.ProductName);
                cmd.Parameters.AddWithValue("@Price", prd.Price);
                cmd.Parameters.AddWithValue("@Count", prd.Count);
                cmd.ExecuteNonQuery();

            }
            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(Constr))
            {
                con.Open();
                string Qstr = "DELETE FROM Product WHERE ProductId = @ProductId";

                SqlCommand cmd = new SqlCommand(Qstr, con);
                cmd.Parameters.AddWithValue("@ProductId",id);
               
                cmd.ExecuteNonQuery();

            }
            return RedirectToAction("Index");
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
