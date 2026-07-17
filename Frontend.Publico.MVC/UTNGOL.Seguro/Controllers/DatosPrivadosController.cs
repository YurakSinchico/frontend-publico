using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UTNGOL.Seguro.Controllers
{
    public class DatosPrivadosController : Controller
    {
        // GET: DatosPrivadosController
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // GET: DatosPrivadosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DatosPrivadosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DatosPrivadosController/Create
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

        // GET: DatosPrivadosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DatosPrivadosController/Edit/5
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

        // GET: DatosPrivadosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DatosPrivadosController/Delete/5
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
