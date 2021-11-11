using ado.net_prac.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ado.net_prac.Controllers
{
    public class AccountController : Controller
    {
        string constring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(RegisterModel model)
        {
            using(SqlConnection con=new SqlConnection(constring))
            {
                con.Open();
                string query = "Select * from Register where UserName=@UserName and Password=@Password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserName", model.Username);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                //cmd.Parameters["@UserName"].Value = model.Username;
                //cmd.Parameters["@Password"].Value = model.Password;
                SqlDataReader dr = cmd.ExecuteReader();

                if(dr.Read())
                {
                    Session["username"] = model.Username;
                    Session["password"] = model.Password;
                    return RedirectToAction("Product", "Home");
                }
                
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            using(SqlConnection con=new SqlConnection(constring))
            {
                con.Open();
                string query = "insert into Register values(@Name,@UserName,@Password)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@UserName", model.Username);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                int i=cmd.ExecuteNonQuery();
                if (i >= 1)
                {
                    return View("Product");
                }
               
            }
            return View();
        }
    }
}