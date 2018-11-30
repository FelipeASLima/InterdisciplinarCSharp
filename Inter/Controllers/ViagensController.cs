using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inter.Data;
using Inter.Models;
using Microsoft.AspNetCore.Authorization;

namespace Inter.Controllers
{
    [Authorize]
    public class ViagensController : Controller
    {
        private readonly Services.IViagemService _service;
        private readonly Services.IStatusService _serviceStatus;

        public ViagensController(Services.IStatusService serviceStatus, Services.IViagemService service)
        {
            _service = service;
            _serviceStatus = serviceStatus;
        }

        // GET: Viagens
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetViagensAsync());
        }

        // GET: Viagens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["StatusId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
         await _serviceStatus.GetStatussAsync(),
         nameof(Models.Status.Id),
         nameof(Models.Status.Description));

            var viagem = await _service.GetViagemAsync(id.Value);
            if (viagem == null)
            {
                return NotFound();
            }

            return View(viagem);
        }

        // GET: Viagens/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["StatusId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
         await _serviceStatus.GetStatussAsync(),
           nameof(Models.Status.Id),
           nameof(Models.Status.Description));

            return View();
        }

        // POST: Viagens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BairroPartida,BairroChegada,HoraPartida,HoraChegada,DueAt,StatusId")] Viagem viagem)
        {
            if (ModelState.IsValid)
            {
                await _service.SaveViagemAsync(viagem);
                return RedirectToAction(nameof(Index));
            }
            return View(viagem);
        }

        // GET: Viagens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
           await _serviceStatus.GetStatussAsync(),
           nameof(Models.Status.Id),
           nameof(Models.Status.Description));
            var viagem = await _service.GetViagemAsync(id.Value);
            if (viagem == null)
            {
                return NotFound();
            }
            return View(viagem);
        }

        // POST: Viagens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BairroPartida,BairroChegada,HoraPartida,HoraChegada,DueAt,StatusId")] Viagem viagem)
        {
            if (id != viagem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.SaveViagemAsync(viagem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViagemExists(viagem.Id))
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
            return View(viagem);
        }

        // GET: Viagens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viagem = await _service.GetViagemAsync(id.Value);
            if (viagem == null)
            {
                return NotFound();
            }

            return View(viagem);
        }

        // POST: Viagens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var viagem = await _service.GetViagemAsync(id.Value);
            await _service.DeleteAsync(viagem);
            return RedirectToAction(nameof(Index));
        }

        private bool ViagemExists(int? id)
        {
            return _service.GetViagemAsync(id.Value) != null;
        }
    }
}
