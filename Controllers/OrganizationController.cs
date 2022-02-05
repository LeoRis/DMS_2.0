using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using BusinessLogicLayer;

namespace DMS.Controllers
{
    public class OrganizationController : Controller
    {
        //DataAccessLayer.OrganizationDAL organizationDAL = new OrganizationDAL();
        //DataAccessLayer.AddressDAL addressDAL = new AddressDAL();

        OrganizationBLL organizationBLL = new OrganizationBLL();
        AddressBLL addressBLL = new AddressBLL();

        // Da se napravi pa da se kreira objekt tuka i da se koristi za ponataka 
        //Da se zamenat site CRUD operaci da odat preky BLL namesto preku DAL vo ovoj kontroiler 

        // GET: Organization
        public ActionResult Index()
        {
            List <Organization> ListOfOrganization = organizationBLL.GetAll();
            return View(ListOfOrganization);
        }

        // CRUD Metodi

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost, ActionName("Create")]
        public ActionResult Create(Models.OrganizationAddEditModel organizationFromView)
        {

            Organization organizationToCheck = organizationBLL.GetByVatNumberAndByCompanyId(organizationFromView.VatNumber, organizationFromView.CompanyId);
            if (ModelState.IsValid)
            {

                if (organizationToCheck != null)
                {
                    TempData["Message"] = "Компанијата постои";
                    TempData.Keep();
                }
                else
                {

                    Address address = new Address();
                    Int32 addressIdFromDatabase = 0;

                    address.City = organizationFromView.City;
                    address.Country = organizationFromView.Country;
                    address.PostCode = organizationFromView.PostCode;
                    address.Street = organizationFromView.Street;
                    address.Province = organizationFromView.Province;
                    address.State = organizationFromView.State;
                    address.Number = organizationFromView.Number;
                    address.IsActive = organizationFromView.IsActive;
                    address.IsDeleted = organizationFromView.IsDeleted;


                    addressIdFromDatabase = addressBLL.InsertReturnId(address);

                    Organization organization = new Organization();

                    organization.OrganizationName = organizationFromView.OrganizationName;
                    organization.CompanyId = organizationFromView.CompanyId;
                    organization.VatNumber = organizationFromView.VatNumber;
                    organization.Description = organizationFromView.Description;
                    organization.Email = organizationFromView.Email;
                    organization.PhoneNumber = organizationFromView.PhoneNumber;
                    organization.Type = organizationFromView.Type;
                    organization.AddressId = addressIdFromDatabase;
                    organization.IsActive = organizationFromView.IsActive;
                    organization.IsDeleted = organizationFromView.IsDeleted;

                    organizationBLL.Insert(organization);

                    TempData["Message"] = "Успешно ја креиравте компанијата " + organization.OrganizationName;

                    return RedirectToAction("Details", new { id = organization.OrganizationId });
                }
            }

            return View();

        }

        public ActionResult Edit(int id)
        {

            Organization organization = organizationBLL.GetById(id);
            Address address = addressBLL.GetById(organization.AddressId);

            Models.OrganizationAddEditModel organizatoAtEditModelObject = new Models.OrganizationAddEditModel();

            organizatoAtEditModelObject.OrganizationId = organization.OrganizationId;
            organizatoAtEditModelObject.OrganizationName = organization.OrganizationName;
            organizatoAtEditModelObject.CompanyId = organization.CompanyId;
            organizatoAtEditModelObject.VatNumber = organization.VatNumber;
            organizatoAtEditModelObject.Description = organization.Description;
            organizatoAtEditModelObject.Email = organization.Email;
            organizatoAtEditModelObject.PhoneNumber = organization.PhoneNumber;
            organizatoAtEditModelObject.Type = organization.Type;
            organizatoAtEditModelObject.IsActive = organization.IsActive;
            organizatoAtEditModelObject.IsDeleted = organization.IsDeleted;


            organizatoAtEditModelObject.AddressId = address.AddressId;
            organizatoAtEditModelObject.Street = address.Street;
            organizatoAtEditModelObject.Number = address.Number;
            organizatoAtEditModelObject.PostCode = address.PostCode;
            organizatoAtEditModelObject.City = address.City;
            organizatoAtEditModelObject.State = address.State;
            organizatoAtEditModelObject.Province = address.Province;
            organizatoAtEditModelObject.Country = address.Country;
            return View(organizatoAtEditModelObject);
        }


        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(Models.OrganizationAddEditModel organization)
        {

            Organization organizationForUpdate = new Organization();

            if (ModelState.IsValid)
            {
                organizationForUpdate.OrganizationId = organization.OrganizationId;
                organizationForUpdate.OrganizationName = organization.OrganizationName;
                organizationForUpdate.VatNumber = organization.VatNumber;
                organizationForUpdate.Description = organization.Description;
                organizationForUpdate.Type = organization.Type;
                organizationForUpdate.Email = organization.Email;
                organizationForUpdate.PhoneNumber = organization.PhoneNumber;
                organizationForUpdate.AddressId = organization.AddressId;
                organizationForUpdate.IsActive = organization.IsActive;
                organizationForUpdate.IsDeleted = organization.IsDeleted;
                organizationForUpdate.CompanyId = organization.CompanyId;

                Address addressForUpdate = addressBLL.GetById(organization.AddressId);

                addressForUpdate.Street = organization.Street;
                addressForUpdate.Number = organization.Number;
                addressForUpdate.PostCode = organization.PostCode;
                addressForUpdate.City = organization.City;
                addressForUpdate.State = organization.State;
                addressForUpdate.Province = organization.Province;
                addressForUpdate.Country = organization.Country;

                organizationBLL.Update(organizationForUpdate);
                addressBLL.Update(addressForUpdate);

                return RedirectToAction("Index");
            }
            else
            {
                return View();

            }
            //organizationDAL.Edit(organization);
        }

        public ActionResult Delete(int id)
        {
            Organization organization = organizationBLL.GetById(id);
            //Address addressForDelete = addressBLL.GetById(organization.AddressId); go prefrlame vo BLL
            organizationBLL.DeletePermanently(organization);
            //addressBLL.DeletePermanently(addressForDelete); go prefrlame vo BLL
            return RedirectToAction("Index");
        }

        //public ActionResult Details(int id)
        //{
        //    Organization organization = organizationBLL.GetById(id);
        //    Models.OrganizationAddEditModel organizationViewModel = GetAllElementsFromCustomModel(organization);
        //    return View(organizationViewModel);
        //}
        public ActionResult Details(int id)
        {
            Organization organization = organizationBLL.GetById(id);
            Models.OrganizationAddEditModel organizationViewModel = GetAllElementsFromCustomModel(organization);
            return View(organizationViewModel);
        }

        [NonAction]
        public virtual Models.OrganizationAddEditModel GetAllElementsFromCustomModel(Organization organization)
        {
            Models.OrganizationAddEditModel organizationViewModel = new Models.OrganizationAddEditModel();

            organizationViewModel.OrganizationId = organization.OrganizationId;
            organizationViewModel.OrganizationName = organization.OrganizationName;
            organizationViewModel.CompanyId = organization.CompanyId;
            organizationViewModel.VatNumber = organization.VatNumber;
            organizationViewModel.Description = organization.Description;
            organizationViewModel.Email = organization.Email;
            organizationViewModel.PhoneNumber = organization.PhoneNumber;
            organizationViewModel.Type = organization.Type;
            organizationViewModel.IsActive = organization.IsActive;
            organizationViewModel.IsDeleted = organization.IsDeleted;

            organizationViewModel.AddressId = organization.AddressId;
            organizationViewModel.Street = organization.Address.Street;
            organizationViewModel.Number = organization.Address.Number;
            organizationViewModel.PostCode = organization.Address.PostCode;
            organizationViewModel.City = organization.Address.City;
            organizationViewModel.State = organization.Address.State;
            organizationViewModel.Province = organization.Address.Province;
            organizationViewModel.Country = organization.Address.Country;

            return organizationViewModel;
        } 
        //public virtual Models.OrganizationAddEditModel GetAllElementsFromCustomModel(Organization organization)
        //{
        //    Models.OrganizationAddEditModel organizationViewModel = new Models.OrganizationAddEditModel();

        //    organizationViewModel.AddressId = organization.AddressId;
        //    organizationViewModel.AddressIsActive = organization.Address.IsActive;
        //    organizationViewModel.Email = organization.Address.Email;
        //    organizationViewModel.CompanyId = organization.CompanyId;
        //   // organizationViewModel.GlobalCompanyId = organization.GlobalCompanyId;
        //    organizationViewModel.IsActive = organization.IsActive;
        //    organizationViewModel.IsDeleted = organization.IsDeleted;
        //    organizationViewModel.AddressIsDeleted = organization.Address.IsDeleted;
        //    organizationViewModel.CellPhone = organization.Address.CellPhone;
        //    organizationViewModel.City = organization.Address.City;
        //    organizationViewModel.Country = organization.Address.Country;
        //    organizationViewModel.Fax = organization.Address.Fax;
        //    organizationViewModel.LegalName = organization.LegalName;
        //    organizationViewModel.Municipality = organization.Address.Municipality;
        //    organizationViewModel.Number = organization.Address.Number;
        //    organizationViewModel.NumberOfSuspension = organization.NumberOfSuspension;
        //    organizationViewModel.OrganizationActivityDescription = organization.OrganizationActivity.OrganizationActivityDescription;
        //    organizationViewModel.OrganizationActivityId = organization.OrganizationActivityId;
        //    organizationViewModel.OrganizationActivityNACE = organization.OrganizationActivity.NaceCode;
        //    organizationViewModel.OrganizationId = organization.OrganizationId;
        //    organizationViewModel.PostalCode = organization.Address.PostalCode;
        //    organizationViewModel.StateProvince = organization.Address.StateProvince;
        //    organizationViewModel.Street = organization.Address.Street;
        //    organizationViewModel.Telephone = organization.Address.Telephone;
        //    organizationViewModel.VatNumber = organization.VatNumber;
        //    organizationViewModel.Website = organization.Website;

        //    return organizationViewModel;
        //}

    }
}