using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using BusinessLogicLayer;

namespace DMS.Controllers
{
    public class InvoiceController : Controller
    {
        InvoiceBLL invoiceBLL = new InvoiceBLL();
        AgreementBLL agreementBLL = new AgreementBLL();
        OrganizationBLL organizationBLL = new OrganizationBLL();
        PersonBLL personBLL = new PersonBLL();
        

        // GET:List of Invoices
        public ActionResult Index()
        {
            List<Invoice> listOfInvoice = invoiceBLL.GetAll();
            return View(listOfInvoice);
        }

        //GET: Create
        public ActionResult Create()
        {
            ViewBag.AgreementId = new SelectList(agreementBLL.GetAll(), "AgreementId", "AgreementNumber");
            ViewBag.OrganizationId = new SelectList(organizationBLL.GetAll(), "OrganizationId", "OrganizationName");
            ViewBag.PersonId = new SelectList(personBLL.GetAll(), "PersonId", "PersonName");
            return View();
        }

        //POST: Create
        [HttpPost, ActionName("Create")]
        public ActionResult Create(Models.InvoiceAddEditModel invoiceAddEditModel)
        {
            Invoice invoice = new Invoice();

            invoice.InvoiceNumber = invoiceAddEditModel.InvoiceNumber;

            invoice.AgreementId = invoiceAddEditModel.AgreementId;
            invoice.OrganizationId = invoiceAddEditModel.OrganizationId;
            invoice.PersonId = invoiceAddEditModel.PersonId;

            invoice.CreateDate = Convert.ToDateTime(invoiceAddEditModel.CreateDate);
            invoice.InvoiceDate = Convert.ToDateTime(invoiceAddEditModel.InvoiceDate);
            invoice.DueDays = invoiceAddEditModel.DueDays;
            invoice.DueDate = Convert.ToDateTime(invoiceAddEditModel.DueDate);
            invoice.Amount = invoiceAddEditModel.Amount;
            invoice.DiscountAmount = invoiceAddEditModel.DiscountAmount;
            invoice.DiscountPercent = invoiceAddEditModel.DiscountPercent;
            invoice.Vat = invoiceAddEditModel.Vat;
            invoice.VatAmount = invoiceAddEditModel.VatAmount;
            invoice.TotalAmount = invoiceAddEditModel.TotalAmount;
            invoice.Currency = invoiceAddEditModel.Currency;
            invoice.CurrencyRate = invoiceAddEditModel.CurrencyRate;
            invoice.Disclaimer = invoiceAddEditModel.Disclaimer;
            invoice.IsPaid = invoiceAddEditModel.IsPaid;
            invoice.PaidDate = Convert.ToDateTime(invoiceAddEditModel.PaidDate);
            invoice.IsActive = invoiceAddEditModel.IsActive;
            invoice.IsDeleted = invoiceAddEditModel.IsDeleted;

            invoiceBLL.Insert(invoice);

            return RedirectToAction("Index");
        }

        //GET: Edit
        public ActionResult Edit(int id)
        {
            Invoice invoice = invoiceBLL.GetById(id);

            Models.InvoiceAddEditModel invoiceAddEditModel = new Models.InvoiceAddEditModel();
            
            invoiceAddEditModel.InvoiceId = invoice.InvoiceId;

            invoiceAddEditModel.AgreementId = invoice.AgreementId;
            invoiceAddEditModel.OrganizationId = invoice.OrganizationId;
            invoiceAddEditModel.PersonId = invoice.PersonId;

            invoiceAddEditModel.InvoiceNumber = invoice.InvoiceNumber;            
            invoiceAddEditModel.CreateDate = Convert.ToDateTime(invoice.CreateDate);
            invoiceAddEditModel.InvoiceDate = Convert.ToDateTime(invoice.InvoiceDate);
            invoiceAddEditModel.DueDays = invoice.DueDays;
            invoiceAddEditModel.DueDate = Convert.ToDateTime(invoice.DueDate);
            invoiceAddEditModel.Amount = invoice.Amount;
            invoiceAddEditModel.DiscountAmount = invoice.DiscountAmount;
            invoiceAddEditModel.DiscountPercent = invoice.DiscountPercent;
            invoiceAddEditModel.Vat = invoice.Vat;
            invoiceAddEditModel.VatAmount = invoice.VatAmount;
            invoiceAddEditModel.TotalAmount = invoice.TotalAmount;
            invoiceAddEditModel.Currency = invoice.Currency;
            invoiceAddEditModel.CurrencyRate = invoice.CurrencyRate;
            invoiceAddEditModel.Disclaimer = invoice.Disclaimer;
            invoiceAddEditModel.IsPaid = invoice.IsPaid;
            invoiceAddEditModel.PaidDate = Convert.ToDateTime(invoice.PaidDate);
            invoiceAddEditModel.IsActive = invoice.IsActive;
            invoiceAddEditModel.IsDeleted = invoice.IsDeleted;


            return View(invoiceAddEditModel);
        }

        //POST: Edit
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(Models.InvoiceAddEditModel invoiceAddEditModel)
        {
            Invoice invoiceForUpdate = new Invoice();

            invoiceForUpdate.InvoiceId = invoiceAddEditModel.InvoiceId;

            invoiceForUpdate.AgreementId = invoiceAddEditModel.AgreementId;
            invoiceForUpdate.OrganizationId = invoiceAddEditModel.OrganizationId;
            invoiceForUpdate.PersonId = invoiceAddEditModel.PersonId;

            invoiceForUpdate.InvoiceNumber = invoiceAddEditModel.InvoiceNumber;
            invoiceForUpdate.CreateDate = Convert.ToDateTime(invoiceAddEditModel.CreateDate);
            invoiceForUpdate.InvoiceDate = Convert.ToDateTime(invoiceAddEditModel.InvoiceDate);
            invoiceForUpdate.DueDays = invoiceAddEditModel.DueDays;
            invoiceForUpdate.DueDate = Convert.ToDateTime(invoiceAddEditModel.DueDate);
            invoiceForUpdate.Amount = invoiceAddEditModel.Amount;
            invoiceForUpdate.DiscountAmount = invoiceAddEditModel.DiscountAmount;
            invoiceForUpdate.DiscountPercent = invoiceAddEditModel.DiscountPercent;
            invoiceForUpdate.Vat = invoiceAddEditModel.Vat;
            invoiceForUpdate.VatAmount = invoiceAddEditModel.VatAmount;
            invoiceForUpdate.TotalAmount = invoiceAddEditModel.TotalAmount;
            invoiceForUpdate.Currency = invoiceAddEditModel.Currency;
            invoiceForUpdate.CurrencyRate = invoiceAddEditModel.CurrencyRate;
            invoiceForUpdate.Disclaimer = invoiceAddEditModel.Disclaimer;
            invoiceForUpdate.IsPaid = invoiceAddEditModel.IsPaid;
            invoiceForUpdate.PaidDate = Convert.ToDateTime(invoiceAddEditModel.PaidDate);
            invoiceForUpdate.IsActive = invoiceAddEditModel.IsActive;
            invoiceForUpdate.IsDeleted = invoiceAddEditModel.IsDeleted;

            invoiceBLL.Update(invoiceForUpdate);

            return RedirectToAction("Index");
        }

        //Delete 
        public ActionResult Delete(int id)
        {
            Invoice invoice = invoiceBLL.GetById(id);
            invoiceBLL.DeletePermanently(invoice);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Invoice invoice = invoiceBLL.GetById(id);
            Models.InvoiceAddEditModel invoiceViewModel = GetAllElementsFromModel(invoice);

            return View(invoiceViewModel);
        }

        [NonAction]
        public virtual Models.InvoiceAddEditModel GetAllElementsFromModel(Invoice invoice)
        {
            Models.InvoiceAddEditModel invoiceViewModel = new Models.InvoiceAddEditModel();

            invoiceViewModel.InvoiceId = invoice.InvoiceId;

            invoiceViewModel.AgreementId = invoice.AgreementId;
            invoiceViewModel.OrganizationId = invoice.OrganizationId;
            invoiceViewModel.PersonId = invoice.PersonId;

            invoiceViewModel.InvoiceNumber = invoice.InvoiceNumber;
            invoiceViewModel.CreateDate = Convert.ToDateTime(invoice.CreateDate);
            invoiceViewModel.InvoiceDate = Convert.ToDateTime(invoice.InvoiceDate);
            invoiceViewModel.DueDays = invoice.DueDays;
            invoiceViewModel.DueDate = Convert.ToDateTime(invoice.DueDate);
            invoiceViewModel.Amount = invoice.Amount;
            invoiceViewModel.DiscountAmount = invoice.DiscountAmount;
            invoiceViewModel.DiscountPercent = invoice.DiscountPercent;
            invoiceViewModel.Vat = invoice.Vat;
            invoiceViewModel.VatAmount = invoice.VatAmount;
            invoiceViewModel.TotalAmount = invoice.TotalAmount;
            invoiceViewModel.Currency = invoice.Currency;
            invoiceViewModel.CurrencyRate = invoice.CurrencyRate;
            invoiceViewModel.Disclaimer = invoice.Disclaimer;
            invoiceViewModel.IsPaid = invoice.IsPaid;
            invoiceViewModel.PaidDate = Convert.ToDateTime(invoice.PaidDate);
            invoiceViewModel.IsActive = invoice.IsActive;
            invoiceViewModel.IsDeleted = invoice.IsDeleted;

            return invoiceViewModel;

        }

    }
}