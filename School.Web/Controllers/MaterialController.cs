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
    public class MaterialController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Material
        public async Task<ActionResult> Index()
        {
            var materials = await db.Materials.Where(x => x.IsDeleted == false).ToListAsync();
            var viewModels = Mapper.Map<List<MaterialViewModel>>(materials);

            return View(viewModels);
        }

        // GET: Material/Create
        public ActionResult Create()
        {
            var viewModel = new MaterialViewModel();

            return View(viewModel);
        }

        // POST: Material/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MaterialViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var material = Mapper.Map<Material>(viewModel);

                db.Materials.Add(material);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Material/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var material = await db.Materials.FindAsync(id);
            if (material == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<MaterialViewModel>(material);

            return View(viewModel);
        }

        // POST: Material/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MaterialViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var material = await db.Materials.FindAsync(viewModel.MaterialId);

                Mapper.Map(viewModel, material);

                db.Entry(material).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Material/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var material = await db.Materials.FindAsync(id);
            if (material == null)
            {
                return HttpNotFound();
            }

            material.IsDeleted = true;

            db.Entry(material).State = EntityState.Modified;

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Material/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var material = await db.Materials.FindAsync(id);
            db.Materials.Remove(material);
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
