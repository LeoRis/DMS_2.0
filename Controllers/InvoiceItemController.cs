using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using BusinessLogicLayer;

namespace DMS.Controllers
{
    public class InvoiceItemController : Controller
    {
        InvoiceItemBLL invoiceItemBLL = new InvoiceItemBLL();
        InvoiceBLL invoiceBLL = new InvoiceBLL();

        // GET List: InvoiceItem
        public ActionResult Index()
        {
            List<InvoiceItem> listOfInvoiceItem = invoiceItemBLL.GetAll();
            return View(listOfInvoiceItem);

        }
    }
}