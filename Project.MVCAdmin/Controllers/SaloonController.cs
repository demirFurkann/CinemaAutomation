using Project.BLL.Repositories.ConcRep;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCAdmin.Controllers
{
    public class SaloonController : Controller
    {
        SaloonRepository _saloonRep;
        SeansRepository _seansRep;
        

        public SaloonController()
        {
            _saloonRep = new SaloonRepository();
            _seansRep= new SeansRepository();

        }
        // GET: Saloon
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Saloon salon)
        {
            if (ModelState.IsValid)
            {
                _saloonRep.Add(salon);
                

                return RedirectToAction("Index", "Home"); // Ekleme işlemi başarılı, ana sayfaya yönlendir.
            }

            return View(salon);
        }



        //public ActionResult Listele()
        //{
        //    var salonlar = _saloonRep.salon.ToList();

        //    return View(salonlar);
        //}
    }
}