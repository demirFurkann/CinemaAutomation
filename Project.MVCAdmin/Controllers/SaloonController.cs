using Project.BLL.Repositories.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCAdmin.Models.CustomTools;
using Project.MVCAdmin.Models.PageVMs;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project.MVCAdmin.Controllers
{
    public class SaloonController : Controller
    {
        SaloonRepository _saloonRep;
        public SaloonController()
        {
            _saloonRep = new SaloonRepository();

        }
        public ActionResult ListSaloons()
        {
            List<SaloonVM> saloons = _saloonRep.
                Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).Select(x => new SaloonVM
                {
                    Capacity = x.Capacity,
                    SaloonNumber = x.SaloonNumber,

                }).ToList();

            ListSaloonPageVM lspvm = new ListSaloonPageVM
            {
                Saloons = saloons,
            };

            return View(lspvm);

        }

        [HttpGet]
        public ActionResult AddSaloon()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UpdateSaloon(int id)
        {

            var saloonVM = _saloonRep.Where(x => x.ID == id).Select(x => new SaloonVM
            {
                ID = x.ID,
                Capacity = x.Capacity,
                SaloonNumber = x.SaloonNumber,
            }).FirstOrDefault();
            
            return View(saloonVM);
        }

        [HttpPost]
        public ActionResult AddSaloon(string SaloonNumber,string Capacity)//SaloonVM salon)
        {
            if (ModelState.IsValid)
            {
                var saloon = new Saloon()
                {
                    SaloonNumber = SaloonNumber,
                    Capacity = Convert.ToInt32(Capacity)
                };
                _saloonRep.Add(saloon);


                return RedirectToAction("ListSaloons", "Saloon"); // Ekleme işlemi başarılı, ana sayfaya yönlendir.
            }

            return View();
        }

        [HttpPost]
        public ActionResult UpdateSaloon(SaloonVM saloon)
        {
            Saloon updated = _saloonRep.Find(saloon.ID);
            updated.Capacity = saloon.Capacity;
            updated.SaloonNumber = saloon.SaloonNumber;


            _saloonRep.Update(updated);
            return RedirectToAction("ListSaloons");

        }
        public ActionResult DeleteSaloon(int id)
        {
            _saloonRep.Delete(_saloonRep.Find(id));

            return RedirectToAction("ListSaloons");

        }
    }
}