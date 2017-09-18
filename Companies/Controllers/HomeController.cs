using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Companies.BusinessLogic;
using Companies.Models;

namespace Companies.Controllers
{
    public class HomeController : Controller
    {
        private readonly CompanyContext _db = new CompanyContext();

        public ActionResult Index()
        {
            var companies = _db.Companies.OrderBy(c => c.Name).ToList();
            EarningCalculator.GetTotalEarnings(companies, null);
            return View(companies);
        }

        public ActionResult Create()
        {
            var companiesList = new SelectList(_db.Companies.OrderBy(o => o.Name).ToList(), "CompanyId", "Name");
            ViewBag.CompaniesList = companiesList;
            return PartialView("Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                company.CompanyId = Guid.NewGuid();
                _db.Companies.Add(company);
                _db.SaveChanges();
                
                return Content("Done");
            }
            var companiesList = new SelectList(_db.Companies.OrderBy(o => o.Name).ToList(), "CompanyId", "Name");
            ViewBag.CompaniesList = companiesList;
            return PartialView(company);

        }
        
        public ActionResult Edit(Guid id)
        {
            var company = _db.Companies.Find(id);
            if (company != null)
            {
                var companiesList =  new SelectList(_db.Companies.OrderBy(o => o.Name).ToList(), "CompanyId", "Name", company.ParentId);
                ViewBag.CompaniesList = companiesList;
                return PartialView("Edit", company);
            }
            return View("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(company).State = EntityState.Modified;
                _db.SaveChanges();
                return Content("Done");
            }
            var companiesList = new SelectList(_db.Companies.OrderBy(o => o.Name).ToList(), "CompanyId", "Name", company.ParentId);
            ViewBag.CompaniesList = companiesList;
            return PartialView(company);
        }
        
        public ActionResult Delete(Guid id)
        {
            var company = _db.Companies.Find(id);
            if (company != null)
            {
                return PartialView("Delete", company);
            }
            return View("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteRecord(Company comp)
        {
            var company = _db.Companies.Find(comp.CompanyId);
            if (company == null) return RedirectToAction("Index");

            var childs = _db.Companies.Where(i => i.ParentId == company.CompanyId);
            if (childs.Any())
            {
                foreach (var child in childs)
                {
                    child.ParentId = null;
                    _db.Entry(child).State = EntityState.Modified;
                }
            }
            _db.Companies.Remove(company);
            _db.SaveChanges();
            return Content("Done");
        }
    }
}