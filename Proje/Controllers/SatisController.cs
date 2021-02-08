using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUrunTakip.Models.Entity;

namespace MvcUrunTakip.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {
            var SatisListele = db.TblSatis.Where(m=>m.Durum==true).ToList();
            return View(SatisListele);
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
            //DrowpDownListe urunu yazdırma kodu
            List<SelectListItem> urun = (from i in db.TblUrun.Where(m=>m.Durum==true).ToList()
                         select new SelectListItem
                         {
                             Text = i.ad,
                             Value = i.id.ToString()
                         }).ToList();
            ViewBag.urn = urun;

            //DrowpDownListe Personeli Yazdırma kodu
            List<SelectListItem> personel = (from i in db.TblPersonel.ToList()
                            select new SelectListItem
                            {
                                Text = i.ad +" "+ i.soyad,
                                Value = i.id.ToString()
                            }).ToList();
            ViewBag.prs = personel;

            //DrowpDownListe Müşteri için Yazdırma kodu
            List<SelectListItem> musteri = (from i in db.TblMusteri.Where(m => m.Durum == true).ToList()
                           select new SelectListItem
                           {
                               Text = i.ad +" "+ i.soyad,
                               Value = i.id.ToString()
                           }).ToList();
            ViewBag.mst = musteri;

            //DrowpDownListe SatışFiyat için Yazdırma kodu
            List<SelectListItem> fiyat = (from i in db.TblUrun.Where(m => m.Durum == true).ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.SatisFiyat.ToString(),
                                                Value = i.id.ToString()
                                            }).ToList();
            ViewBag.fyt = fiyat;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(TblSatis p)
        {
            // DrowpDownListeki Urunu Veri Tabana Ekleme kodu
            var urun = db.TblUrun.Where(m => m.id == p.TblUrun.id).FirstOrDefault();
            p.TblUrun = urun;

            // DrowpDownListeki personerli Veri Tabana Ekleme kodu
            var personel = db.TblPersonel.Where(m => m.id == p.TblPersonel.id).FirstOrDefault();
            p.TblPersonel = personel;

            // DrowpDownListeki personerli Veri Tabana Ekleme kodu
            var muster = db.TblMusteri.Where(m => m.id == p.TblMusteri.id).FirstOrDefault();
            p.TblMusteri = muster;

            // DrowpDownListeki personerli Veri Tabana Ekleme kodu
            var fiyat = db.TblUrun.Where(m => m.id == p.TblUrun.id).FirstOrDefault();
            p.TblUrun = fiyat;

            db.TblSatis.Add(p);
            p.Durum = true;

            //eklendigi tarihi ekleme kodu
            p.tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(TblSatis p)
        {
            //satış silme yada gizleme kodu
            var deger = db.TblSatis.Find(p.id);
            deger.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisGetir(TblSatis p)
        {
            // DrowpDownListe urunu yazdırma kodu
            List<SelectListItem> urun = (from i in db.TblUrun.Where(m => m.Durum == true).ToList()
                                         select new SelectListItem
                                         {
                                             Text = i.ad,
                                             Value = i.id.ToString()
                                         }).ToList();
            ViewBag.urn = urun;

            //DrowpDownListe Personeli Yazdırma kodu
            List<SelectListItem> personel = (from i in db.TblPersonel.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.ad + " " + i.soyad,
                                                 Value = i.id.ToString()
                                             }).ToList();
            ViewBag.prs = personel;

            //DrowpDownListe Müşteri için Yazdırma kodu
            List<SelectListItem> musteri = (from i in db.TblMusteri.Where(m => m.Durum == true).ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.ad + " " + i.soyad,
                                                Value = i.id.ToString()
                                            }).ToList();
            ViewBag.mst = musteri;

            //DrowpDownListe SatışFiyat için Yazdırma kodu
            List<SelectListItem> fiyat = (from i in db.TblUrun.Where(m => m.Durum == true).ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.SatisFiyat.ToString(),
                                              Value = i.id.ToString()
                                          }).ToList();
            ViewBag.fyt = fiyat;

            var deger = db.TblSatis.Find(p.id);
            return View("SatisGetir",deger);
        }
        public ActionResult Guncelle(TblSatis p)
        {
            //Güncelleme Satış Kodu
            var deger = db.TblSatis.Find(p.id);
            var urun = db.TblUrun.Where(m => m.id == p.TblUrun.id).FirstOrDefault();
            deger.TblUrun = urun;

            var personel = db.TblPersonel.Where(m => m.id == p.TblPersonel.id).FirstOrDefault();
            deger.TblPersonel = personel;

            var musteri = db.TblMusteri.Where(m => m.id == p.TblMusteri.id).FirstOrDefault();
            deger.TblMusteri = musteri;

            var fiyat = db.TblUrun.Where(m => m.id == p.TblUrun.SatisFiyat).FirstOrDefault();
            deger.TblUrun = fiyat;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}