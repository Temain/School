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
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Student
        public async Task<ActionResult> Index()
        {
            var students = await db.Students.Where(x => x.IsDeleted == false).ToListAsync();
            var viewModels = Mapper.Map<List<StudentViewModel>>(students);

            return View(viewModels);
        }

        // GET: Student/Create
        public async Task<ActionResult> Create()
        {
            var viewModel = new StudentViewModel();

            await CreateSelectLists();

            return View(viewModel);
        }

        // POST: Student/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var student = Mapper.Map<Student>(viewModel);

                db.Students.Add(student);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Student/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<StudentViewModel>(student);

            var cls = await db.Classes.Where(x => x.IsDeleted == false)
               .Select(x => new { x.ClassId, ClassName = x.Number + x.Prefix })
               .ToListAsync();
            ViewBag.Classes = new SelectList(cls, "ClassId", "ClassName");

            return View(viewModel);
        }

        // POST: Student/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var student = await db.Students.FindAsync(viewModel.StudentId);

                Mapper.Map(viewModel, student);

                db.Entry(student).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Student/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            student.IsDeleted = true;

            db.Entry(student).State = EntityState.Modified;

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var student = await db.Students.FindAsync(id);
            db.Students.Remove(student);
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

        private async Task CreateSelectLists()
        {
            var cls = await db.Classes.Where(x => x.IsDeleted == false)
                .Select(x => new { x.ClassId, ClassName = x.Number + x.Prefix })
                .ToListAsync();
            ViewBag.Classes = new SelectList(cls, "ClassId", "ClassName");
        }
    }
}
