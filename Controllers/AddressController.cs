using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;

namespace DMS.Controllers
{
    public class AddressController : Controller
    {
        DataAccessLayer.AddressDAL addressDAL = new AddressDAL(); // da se povrzeme so bazata kreirame objekt

        // GET: Address
        public ActionResult Index()
        {
            //List<Address> listOfAddresses = addressDAL.GetAllAddresses();


            Address address = new Address();

            // address.AddressId  - ne treba da go vnesuvame
            //address.City = "Skopje";
            //address.Country = "Macedonia";
            //address.PostCode = "1000";
            //address.Street = "Nikola Tesla";
            //address.Province = "";
            //address.State = "Macedonia";
            //address.Number = "34";
            //address.IsActive = true;
            //address.IsDeleted = false;

           //addressDAL.Insert(address);

            List<Address> listOfAddresses = addressDAL.GetActive();
            return View(listOfAddresses);

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public ActionResult Create(Address addressFromView)
        {
            Address address = new Address();

            address.City = addressFromView.City;
            address.Country = addressFromView.Country;
            address.PostCode = addressFromView.PostCode;
            address.Street = addressFromView.Street;
            address.Province = addressFromView.Province;
            address.State = addressFromView.State;
            address.Number = addressFromView.Number;
            address.IsActive = addressFromView.IsActive;
            address.IsDeleted = addressFromView.IsDeleted;

            addressDAL.Insert(address);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Address address = addressDAL.GetById(id);
            addressDAL.DeletePermanently(address);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Address address = addressDAL.GetById(id);

            if (address == null)
            {
                //return RedirectToAction("Error");
                return RedirectToAction("Error", "Address", new { errorMessage = "Zapis so toa ID ne postoi..!" });
            }

            return View(address);
        }

        public ActionResult Error(string errorMessage="Contact your administrator")
        {
            TempData["Error Message"] = errorMessage;
            TempData.Keep();
            return View();
        }
    }
}