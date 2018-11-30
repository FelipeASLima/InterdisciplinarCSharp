using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Inter.Models;

namespace Inter.Controllers
{
    [Authorize]
    public class StatusesController : Controller
    {
        private readonly Services.IStatusService _service;

        public StatusesController(Services.IStatusService service)
        {
            _service = service;
        }

        // GET: Statuses
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetStatussAsync());
        }

        // GET: Statuses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _service.GetStatusAsync(id.Value);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // GET: Statuses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Statuses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] Status status)
        {
            if (ModelState.IsValid)
            {
                await _service.SaveStatusAsync(status);
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        // GET: Statuses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _service.GetStatusAsync(id.Value);
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }

        // POST: Statuses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] Status status)
        {
            if (id != status.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _service.SaveStatusAsync(status);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusExists(status.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        // GET: Statuses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _service.GetStatusAsync(id.Value);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // POST: Statuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var status = await _service.GetStatusAsync(id.Value);
            await _service.DeleteAsync(status);
            return RedirectToAction(nameof(Index));
        }

        private bool StatusExists(int? id)
        {
            return _service.GetStatusAsync(id.Value) != null;
        }
    }
}