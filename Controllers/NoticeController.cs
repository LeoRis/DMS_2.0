using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using BusinessLogicLayer;

namespace DMS.Controllers
{
    public class NoticeController : Controller
    {
        NoticesBLL noticesBLL = new NoticesBLL();
        InvoiceBLL invoiceBLL = new InvoiceBLL();
        OrganizationBLL organizationBLL = new OrganizationBLL();
        PersonBLL personBLL = new PersonBLL();

        //CRUD Metodi

        //Get list: Notice 
        public ActionResult Index()
        {
            List<Notice> ListOfNotices = noticesBLL.GetAll();
            return View(ListOfNotices);
        }

        //GET: Create
        public ActionResult Create()
        {
            ViewBag.InvoiceId = new SelectList(invoiceBLL.GetAll(), "InvoiceId", "InvoiceNumber");
            ViewBag.OrganizationId = new SelectList(organizationBLL.GetAll(), "OrganizationId", "OrganizationName");
            ViewBag.PersonId = new SelectList(personBLL.GetAll(), "PersonId", "PersonName");
            return View();
        }

        //POST:Create
        [HttpPost, ActionName("Create")]
        public ActionResult Create(Models.NoticeAddEditModel noticeAddEditModel)
        {
            Notice notice = new Notice();

            notice.NoticeText = noticeAddEditModel.NoticeText;
            notice.NoticeNumber = noticeAddEditModel.NoticeNumber;
            notice.NoticeDate = noticeAddEditModel.NoticeDate;
            notice.InvoiceId = noticeAddEditModel.InvoiceId;
            notice.OrganizationId = noticeAddEditModel.OrganizationId;
            notice.PersonId = noticeAddEditModel.PersonId;
            notice.IsActive = noticeAddEditModel.IsActive;
            notice.IsDeleted = noticeAddEditModel.IsDeleted;

            noticesBLL.Insert(notice);

            return RedirectToAction("Index");
        }
        //GET:Edit
        public ActionResult Edit(int id)
        {
            Notice notice = noticesBLL.GetById(id);
            Models.NoticeAddEditModel noticeAddEditModel = new Models.NoticeAddEditModel();

            noticeAddEditModel.NoticeId = notice.NoticeId;
            noticeAddEditModel.NoticeText = notice.NoticeText;
            noticeAddEditModel.NoticeNumber = notice.NoticeNumber;
            noticeAddEditModel.NoticeDate =Convert.ToDateTime(notice.NoticeDate);
            noticeAddEditModel.InvoiceId = notice.InvoiceId;
            noticeAddEditModel.OrganizationId = notice.OrganizationId;
            noticeAddEditModel.PersonId = notice.PersonId;
            noticeAddEditModel.IsActive = notice.IsActive;
            noticeAddEditModel.IsDeleted = notice.IsDeleted;

            return View(noticeAddEditModel);
        }

        //POST:Edit
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(Models.NoticeAddEditModel noticeAddEditModel)
        {
            Notice noticeForUpdate = new Notice();

            noticeForUpdate.NoticeId = noticeAddEditModel.NoticeId;
            noticeForUpdate.NoticeText = noticeAddEditModel.NoticeText;
            noticeForUpdate.NoticeNumber = noticeAddEditModel.NoticeNumber;
            noticeForUpdate.NoticeDate =Convert.ToDateTime(noticeAddEditModel.NoticeDate);
            noticeForUpdate.InvoiceId = noticeAddEditModel.InvoiceId;
            noticeForUpdate.OrganizationId = noticeAddEditModel.OrganizationId;
            noticeForUpdate.PersonId = noticeAddEditModel.PersonId;
            noticeForUpdate.IsActive = noticeAddEditModel.IsActive;
            noticeForUpdate.IsDeleted = noticeAddEditModel.IsDeleted;

            noticesBLL.Update(noticeForUpdate);

            return RedirectToAction("Index");
        }

        //Delete
        public ActionResult Delete(int id)
        {
            Notice notice = noticesBLL.GetById(id);
            noticesBLL.DeletePermanently(notice);
            return RedirectToAction("Index");
        }

        //Details
        public ActionResult Details(int id)
        {
            Notice notice = noticesBLL.GetById(id);
            Models.NoticeAddEditModel noticeViewModel = GetAllElementsFromModel(notice);
            return View(noticeViewModel);
        }

        [NonAction]
        public virtual Models.NoticeAddEditModel GetAllElementsFromModel(Notice notice)
        {
            Models.NoticeAddEditModel noticeViewModel = new Models.NoticeAddEditModel();

            noticeViewModel.NoticeId = notice.NoticeId;
            noticeViewModel.NoticeText = notice.NoticeText;
            noticeViewModel.NoticeNumber = notice.NoticeNumber;
            noticeViewModel.NoticeDate = notice.NoticeDate;
            noticeViewModel.InvoiceId = notice.InvoiceId;
            noticeViewModel.OrganizationId = notice.OrganizationId;
            noticeViewModel.PersonId = notice.PersonId;
            noticeViewModel.IsActive = notice.IsActive;
            noticeViewModel.IsDeleted = notice.IsDeleted;

            return noticeViewModel;
        }
    }
}