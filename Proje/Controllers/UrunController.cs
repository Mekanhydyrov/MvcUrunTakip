using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUrunTakip.Models.Entity;

namespace MvcUrunTakip.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(String p)
        {
            // var UrunListele = db.TblUrun.Where(x => x.Durum == true).ToList();

            //Arama kodu ve Listeleme Kodu
            var urunler = db.TblUrun.Where(x => x.Durum == true);
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(x => x.ad.Contains(p));
            }
            return View(urunler.ToList());
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            //DrowpDownListe veri Yazdırma kodu
            var deger = (from i in db.TblKategori.ToList()
                         select new SelectListItem
                         {
                             Text = i.ad,
                             Value = i.id.ToString()
                         }).ToList();
            ViewBag.dgr = deger;

            //boş deger dondurmemesi için kod
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(TblUrun Ekel)
        {
            //DropDownList Ekleme Kodu
            var ktg = db.TblKategori.Where(m => m.id == Ekel.TblKategori.id).FirstOrDefault();
            Ekel.TblKategori = ktg;

            //Ekleme kodu
            db.TblUrun.Add(Ekel);
            Ekel.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(TblUrun p)
        {
            //DrowpDownListe veri yazdırma kodu
            var kategori = (from i in db.TblKategori.ToList()
                            select new SelectListItem
                            {
                                Text = i.ad,
                                Value = i.id.ToString()
                            }).ToList();
            ViewBag.ktg = kategori;

            //Verileri deger Sayfaya taşamak kodu
            var deger = db.TblUrun.Find(p.id);

            return View("UrunGetir", deger);
        }
        public ActionResult Guncelle(TblUrun p)
        {
            //degerleri veri tabana yazdırma kodu
            var deger = db.TblUrun.Find(p.id);
            deger.ad = p.ad;
            deger.marka = p.marka;
            deger.stok = p.stok;
            deger.AlisFiyat = p.AlisFiyat;
            deger.SatisFiyat = p.SatisFiyat;

            //DrowpDownList Veri Tabana Ekleme kodu
            var ktg = db.TblKategori.Where(m => m.id == p.TblKategori.id).FirstOrDefault();
            deger.Kategori = ktg.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(TblUrun p)
        {
            //Durumunu False ederek gizleme yani silme işlemi 
            var deger = db.TblUrun.Find(p.id);
            deger.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}