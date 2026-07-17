using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Publico.MVC.Controllers
{
    [Authorize(Roles = "Registrado,Administrador")] // RESTRICTIVO SEGÚN PROYECTO
    public class BilleteraController : Controller
    {
        // GET: BilleteraController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BilleteraController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BilleteraController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BilleteraController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BilleteraController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BilleteraController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BilleteraController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BilleteraController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
