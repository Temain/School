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
    public class TeacherController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Teacher
        public async Task<ActionResult> Index()
        {
            var teachers = await db.Teachers.Where(x => x.IsDeleted == false).ToListAsync();
            var viewModels = Mapper.Map<List<TeacherViewModel>>(teachers);

            return View(viewModels);
        }

        // GET: Teacher/Create
        public ActionResult Create()
        {
            var viewModel = new TeacherViewModel();

            return View(viewModel);
        }

        // POST: Teacher/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TeacherViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var teacher = Mapper.Map<Teacher>(viewModel);

                db.Teachers.Add(teacher);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Teacher/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<TeacherViewModel>(teacher);

            return View(viewModel);
        }

        // POST: Teacher/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TeacherViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var teacher = await db.Teachers.FindAsync(viewModel.TeacherId);

                Mapper.Map(viewModel, teacher);

                db.Entry(teacher).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Teacher/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }

            teacher.IsDeleted = true;

            db.Entry(teacher).State = EntityState.Modified;

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var teacher = await db.Teachers.FindAsync(id);
            db.Teachers.Remove(teacher);
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
