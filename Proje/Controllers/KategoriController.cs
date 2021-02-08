using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUrunTakip.Models.Entity;

namespace MvcUrunTakip.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {
            var KategoriListele = db.TblKategori.Where(m => m.Durum == true).ToList();
            return View(KategoriListele);
        }

        [HttpGet]
        public ActionResult YeniKategori()
        {
            //boş deger döndürmemesi için kod
            return View();
        }

        [HttpPost]
        public ActionResult YeniKategori(TblKategori pEkle)
        {
            //kategori ekleme kodu
            db.TblKategori.Add(pEkle);

            //sadece durumu true olanlar için
            pEkle.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(TblKategori p)
        {
            //Kategori silme kodu
            var deger = db.TblKategori.Find(p.id);
            deger.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(TblKategori p)
        {
            //verileri idiye göre deger sayfaya taşamak
            var deger = db.TblKategori.Find(p.id);
            return View("KategoriGetir",deger);
        }
        public ActionResult Guncelle(TblKategori p)
        {
            //Güncelleme kodu
            var deger = db.TblKategori.Find(p.id);
            deger.ad = p.ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}