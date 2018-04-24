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
    public class DisciplineController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Discipline
        public async Task<ActionResult> Index()
        {
            var disciplines = await db.Disciplines.Where(x => x.IsDeleted == false).ToListAsync();
            var viewModels = Mapper.Map<List<DisciplineViewModel>>(disciplines);

            return View(viewModels);
        }

        // GET: Discipline/Create
        public async Task<ActionResult> Create()
        {
            var viewModel = new DisciplineViewModel();

            await CreateSelectLists();

            return View(viewModel);
        }

        // POST: Discipline/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DisciplineViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var discipline = Mapper.Map<Discipline>(viewModel);

                db.Disciplines.Add(discipline);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Discipline/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var discipline = await db.Disciplines.FindAsync(id);
            if (discipline == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<DisciplineViewModel>(discipline);

            await CreateSelectLists();

            return View(viewModel);
        }

        // POST: Discipline/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DisciplineViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var discipline = await db.Disciplines.FindAsync(viewModel.DisciplineId);

                Mapper.Map(viewModel, discipline);

                db.Entry(discipline).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Discipline/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var discipline = await db.Disciplines.FindAsync(id);
            if (discipline == null)
            {
                return HttpNotFound();
            }

            discipline.IsDeleted = true;

            db.Entry(discipline).State = EntityState.Modified;

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Discipline/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var discipline = await db.Disciplines.FindAsync(id);
            db.Disciplines.Remove(discipline);
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

            var teachers = await db.Teachers.Where(x => x.IsDeleted == false)
                .Select(x => new {
                    x.TeacherId,
                    TeacherName = x.LastName + " " + (x.FirstName != null ? x.FirstName : "") + " "
                    + (x.MiddleName != null ? x.MiddleName : "")
                })
                .ToListAsync();
            ViewBag.Teachers = new SelectList(teachers, "TeacherId", "TeacherName");
        }
    }
}
