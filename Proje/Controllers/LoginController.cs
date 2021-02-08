using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.SqlServer.Server;
using MvcUrunTakip.Models.Entity;
using System.Web.Security;

namespace MvcUrunTakip.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(TblAdmin p)
        {
            var bilgi = db.TblAdmin.FirstOrDefault(x => x.Kullanici == p.Kullanici && x.Sifre == p.Sifre);
            if (bilgi != null)
            {
                FormsAuthentication.SetAuthCookie(bilgi.Kullanici, false);
                return RedirectToAction("Index", "Musteri");
            }
            else
            {
                return View();
            }
        }
    }
}
