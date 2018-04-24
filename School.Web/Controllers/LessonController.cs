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
            var cls = await db.Classes.Where(x => x.IsDeleted == false)
                 .Select(x => new { x.ClassId, ClassName = x.Number + x.Prefix })
                 .ToListAsync();
            ViewBag.Classes = new SelectList(cls, "ClassId", "ClassName");

            var firstSeptember = new DateTime(DateTime.Today.Year - 1, 9, 1);
            //beware different cultures, see other answers
            var startOfFirstWeek = firstSeptember.AddDays(1 - (int)(firstSeptember.DayOfWeek));
            var weeks =
                Enumerable
                    .Range(0, 54)
                    .Select(i => new {
                        weekStart = startOfFirstWeek.AddDays(i * 7)
                    })
                    .TakeWhile(x => x.weekStart.Year <= firstSeptember.Year + 1)
                    .Select(x => new {
                        x.weekStart,
                        weekFinish = x.weekStart.AddDays(4)
                    })
                    .SkipWhile(x => x.weekFinish < firstSeptember.AddDays(1))
                    .Select((x, i) => new SelectListItem {
                        Text = x.weekStart.ToShortDateString() + " - " + x.weekFinish.ToShortDateString(),
                        // WeekNumber = i + 1,
                        Value = x.weekStart.ToShortDateString(),
                        Selected = DateTime.Now.StartOfWeek(DayOfWeek.Monday).ToShortDateString() == x.weekStart.ToShortDateString()
                    });

            ViewBag.Weeks = weeks; // new SelectList(weeks);

            return View();
        }

        // GET: Lesson
        public async Task<ActionResult> GetLessons(int? classId, int? disciplineId, string weekStart)
        {
            var startDate = DateTime.Parse(weekStart);
            var endDate = startDate.AddDays(8);
            var lessons = await db.Lessons.Where(x => x.DisciplineId == disciplineId && x.Discipline.ClassId == classId
                && x.LessonDate >= startDate && x.LessonDate <= endDate
                && x.IsDeleted == false).ToListAsync();
            var viewModels = Mapper.Map<List<LessonViewModel>>(lessons);

            var discipline = await db.Disciplines.SingleOrDefaultAsync(d => d.DisciplineId == disciplineId);

            ViewBag.NumberOfLessons = discipline.NumberOfLessons;
            ViewBag.TeacherName = discipline.Teacher.LastName + " " + (discipline.Teacher.FirstName != null ? discipline.Teacher.FirstName : "") + " "
                + (discipline.Teacher.MiddleName != null ? discipline.Teacher.MiddleName : "");
            ViewBag.AddedLessons = await db.Lessons.Where(x => x.DisciplineId == disciplineId 
                    && x.Discipline.ClassId == classId
                    && x.IsDeleted == false)
                .CountAsync();

            var students = await db.Students.Where(s => s.IsDeleted == false).ToListAsync();
            ViewBag.Students = Mapper.Map<List<StudentViewModel>>(students);

            return PartialView("_Lesson", viewModels);
        }

        public async Task<ActionResult> GetDisciplines(int? classId)
        {
            var disciplines = await db.Disciplines.Where(d => d.ClassId == classId && d.IsDeleted == false)
                .ToListAsync();

            var viewModels = Mapper.Map<List<DisciplineViewModel>>(disciplines);

            return Json(viewModels, JsonRequestBehavior.AllowGet);
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

                return Json("Success");
            }

            return null;
        }

        // POST: Lesson/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateDetail(LessonDetailViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var lessonDetail = Mapper.Map<LessonDetail>(viewModel);

                db.LessonDetail.Add(lessonDetail);
                await db.SaveChangesAsync();

                return Json("Success");
            }

            return null;
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


    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }

}
