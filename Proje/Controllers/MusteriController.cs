using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUrunTakip.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcUrunTakip.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(int sayfa=1)
        {
            //Müşteri Listeleme Kodu
            // var MusteriListele = db.TblMusteri.ToList();

            //musteri listeleme ve sayfalama kodu
            var MusteriSayfala = db.TblMusteri.Where(m=>m.Durum==true).ToList().ToPagedList(sayfa, 5);
            return View(MusteriSayfala);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            //boş deger döndürmemesi için
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TblMusteri p)
        {
            // musteri ekleme sayfası
            db.TblMusteri.Add(p);
            p.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            //Muşteri Silme sayfası ama Tru false şeklinde 
            var deger = db.TblMusteri.Find(id);
            deger.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(TblMusteri p)
        {
            //idiye göre verileri deger sayfaya taşama kodu
            var deger = db.TblMusteri.Find(p.id);
            return View("MusteriGetir", deger);
        }
        public ActionResult Guncelle(TblMusteri p)
        {
            var deger = db.TblMusteri.Find(p.id);
            deger.ad = p.ad;
            deger.soyad = p.soyad;
            deger.sehir = p.sehir;
            deger.bakiye = p.bakiye;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}