using Project.BLL.Repositories.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCAdmin.Models.PageVMs;
using Project.VM.PureVMs;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Project.MVCAdmin.Controllers
{
    public class BoxOfficeController : Controller
    {
        BoxOfficeRepository _boxOfficeRep;
        public BoxOfficeController()
        {
            _boxOfficeRep = new BoxOfficeRepository();
        }
        
        public ActionResult Index()
        {
            List<BoxOfficeVM> boxOffices = _boxOfficeRep.Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted)
                .Select(x => new BoxOfficeVM
                {
                    ID = x.ID,
                    OfficeNumber = x.OfficeNumber,
                }).ToList();
            ListBoxOfficePageVM opvm = new ListBoxOfficePageVM
            {
                BoxOffices = boxOffices,
            };

            return View(opvm);
        }
        
        public ActionResult AddBoxOffice()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBoxOffice(BoxOfficeVM boxOffice)
        {
            BoxOffice b = new BoxOffice
            {
                OfficeNumber = boxOffice.OfficeNumber,
            };
            _boxOfficeRep.Add(b);
            return RedirectToAction("Index");
        }

        public ActionResult UpdateBoxOffice(int id)
        {
            BoxOfficeVM boxOffice = _boxOfficeRep.Where(x => x.ID == id).Select(x => new BoxOfficeVM
            {
                ID = x.ID,
                OfficeNumber = x.OfficeNumber,
            }).FirstOrDefault();

            AddUpdateBoxOfficePageVM opvm = new AddUpdateBoxOfficePageVM
            {
                BoxOffice = boxOffice,
            };
            return View(opvm);
        }
        [HttpPost]
        public ActionResult UpdateBoxOffice(BoxOfficeVM boxOffice)
        {
            BoxOffice updated = _boxOfficeRep.Find(boxOffice.ID);
            updated.OfficeNumber = boxOffice.OfficeNumber;

            _boxOfficeRep.Update(updated);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteBoxOffice(int id)
        {
            _boxOfficeRep.Delete(_boxOfficeRep.Find(id));

            return RedirectToAction("Index");
        }
    }
}