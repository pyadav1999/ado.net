using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ado.net_prac.Models;

namespace ado.net_prac.Controllers
{
    public class HomeController : Controller
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Product()
        {
            List<ProductModel> model = new List<ProductModel>();
            if (Session["username"]!=null&&Session["password"]!=null)
            {
                
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    string query = "select * from Register where UserName=@username and Password=@pass";
                    SqlCommand user = new SqlCommand(query, con);
                    user.Parameters.AddWithValue("@username", Session["username"]);
                    user.Parameters.AddWithValue("@pass", Session["password"]);
                    SqlDataAdapter data = new SqlDataAdapter(user);
                    DataTable datatbl = new DataTable();
                    data.Fill(datatbl);
                    Session["Name"] = datatbl.Rows[0]["Name"];
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Products", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        model.Add(
                            new ProductModel
                            {
                                Id = Convert.ToInt32(dr["ProductId"]),
                                ProductName = Convert.ToString(dr["ProductName"]),
                                Price = Convert.ToInt32(dr["Price"]),
                                Qauntity = Convert.ToInt32(dr["Quantity"])

                            }
                            );
                    }
                    con.Close();
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
           
           
            return View(model);
        }
    }
}