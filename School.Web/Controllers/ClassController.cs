using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using School.Web.Models;
using AutoMapper;
using School.Web.Models.ViewModels;

namespace School.Web.Controllers
{
    public class ClassController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Class
        public async Task<ActionResult> Index()
        {
            var classs = await db.Classes.Where(x => x.IsDeleted == false).ToListAsync();
            var viewModels = Mapper.Map<List<ClassViewModel>>(classs);

            return View(viewModels);
        }

        // GET: Class/Create
        public ActionResult Create()
        {
            var viewModel = new ClassViewModel();

            return View(viewModel);
        }

        // POST: Class/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClassViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var cls = Mapper.Map<Class>(viewModel);

                db.Classes.Add(cls);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Class/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cls = await db.Classes.FindAsync(id);
            if (cls == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<ClassViewModel>(cls);

            return View(viewModel);
        }

        // POST: Class/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ClassViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var cls = await db.Classes.FindAsync(viewModel.ClassId);

                Mapper.Map(viewModel, cls);

                db.Entry(cls).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Class/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cls = await db.Classes.FindAsync(id);
            if (cls == null)
            {
                return HttpNotFound();
            }

            cls.IsDeleted = true;

            db.Entry(cls).State = EntityState.Modified;

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
