using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private BookDBContext db = new BookDBContext();

        public ActionResult Index(string searchString)
        {
            var books = from b in db.Books select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Name.Contains(searchString));
            }
            return View(books);
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookRecords = db.BookRecords.Where(e => e.BookId.Equals(book.Id)).OrderByDescending(e=>e.Id);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "Id,Name,ValidStatus")] Book book, string update)
        {
            // Save Book Status
            book.ValidStatus = (update == "Return Book");
            db.Entry(book).State = EntityState.Modified;

            // Save Book Record
            if(update == "Return Book")
            {
                BookRecord record = db.BookRecords.Where(e => e.BookId.Equals(book.Id) && e.ReturnTime == null).OrderByDescending(e=>e.Id).FirstOrDefault();
                if (record != null)
                {
                    record.ReturnTime = DateTime.Now;
                    db.Entry(record).State = EntityState.Modified;
                }
            }
            else
            {
                BookRecord record = new BookRecord();
                record.BookId = book.Id;
                record.CreatedTime = DateTime.Now;
                db.BookRecords.Add(record);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}