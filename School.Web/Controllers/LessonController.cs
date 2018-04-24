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
    public class LessonController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Lesson
        public async Task<ActionResult> Index()
        {
            var lessons = await db.Lessons.Where(x => x.IsDeleted == false).ToListAsync();
            var viewModels = Mapper.Map<List<LessonViewModel>>(lessons);

            return View(viewModels);
        }

        // GET: Lesson/Create
        public ActionResult Create()
        {
            var viewModel = new LessonViewModel();

            return View(viewModel);
        }

        // POST: Lesson/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LessonViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var lesson = Mapper.Map<Lesson>(viewModel);

                db.Lessons.Add(lesson);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Lesson/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lesson = await db.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<LessonViewModel>(lesson);

            return View(viewModel);
        }

        // POST: Lesson/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LessonViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var lesson = await db.Lessons.FindAsync(viewModel.LessonId);

                Mapper.Map(viewModel, lesson);

                db.Entry(lesson).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Lesson/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lesson = await db.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }

            lesson.IsDeleted = true;

            db.Entry(lesson).State = EntityState.Modified;

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Lesson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var lesson = await db.Lessons.FindAsync(id);
            db.Lessons.Remove(lesson);
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
