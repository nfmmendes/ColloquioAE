using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.EntityFrameworkCore;
using PlaceBooking.Data;
using PlaceBooking.Models;

namespace PlaceBooking.Controllers
{
    public class LocalUsesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocalUsesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LocalUses
        public async Task<IActionResult> Index()
        {
            return View(await _context.DbLocalUses.ToListAsync());
        }

        // GET: LocalUses/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localUse = await _context.DbLocalUses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (localUse == null)
            {
                return NotFound();
            }

            return View(localUse);
        }

        // GET: LocalUses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocalUses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Arrival,Leave")] LocalUse localUse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(localUse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(localUse);
        }


        public async Task<JsonResult> AssingEntrance(long localId, long personId)
        {

            var booking =  _context.DbBookings.Include(x=>x.Person).Include(x=>x.Local).FirstOrDefault(x => x.Person.Id == personId && x.Local.Id == localId);


            if (booking != null)
            {
                var use = new LocalUse()
                {
                    Booking = booking,
                    Arrival = DateTime.Now,
                    Leave = DateTime.Now
                };
                _context.Add(use);
                _context.SaveChanges(); 
            }

            
            return Json("");
        }

        public async Task<JsonResult> AssingLeave(long localId, long personId)
        {

            var booking = _context.DbLocalUses.Include(x => x.Booking).Include(x => x.Booking.Local).Include(x=>x.Booking.Person)
                                              .FirstOrDefault(x => x.Booking.Person.Id == localId && x.Booking.Local.Id == localId);

            if (booking != null) {
                booking.Leave = DateTime.Now;
                _context.SaveChanges();
            }

            return Json("");
        }



        // GET: LocalUses/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localUse = await _context.DbLocalUses.FindAsync(id);
            if (localUse == null)
            {
                return NotFound();
            }
            return View(localUse);
        }

        // POST: LocalUses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Arrival,Leave")] LocalUse localUse)
        {
            if (id != localUse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(localUse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalUseExists(localUse.Id))
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
            return View(localUse);
        }

        // GET: LocalUses/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localUse = await _context.DbLocalUses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (localUse == null)
            {
                return NotFound();
            }

            return View(localUse);
        }

        // POST: LocalUses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var localUse = await _context.DbLocalUses.FindAsync(id);
            _context.DbLocalUses.Remove(localUse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocalUseExists(long id)
        {
            return _context.DbLocalUses.Any(e => e.Id == id);
        }
    }
}
