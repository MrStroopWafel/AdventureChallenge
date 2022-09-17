using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdventureChallenge.Models;

namespace AdventureChallenge.Controllers
{
    public class HintsController : Controller
    {
        private readonly AdventureChallengeContext _context;

        public HintsController(AdventureChallengeContext context)
        {
            _context = context;
        }

        // GET: Hints
        public async Task<IActionResult> Index()
        {
              return View(await _context.Hints.ToListAsync());
        }

        // GET: Hints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hints == null)
            {
                return NotFound();
            }

            var hint = await _context.Hints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hint == null)
            {
                return NotFound();
            }

            return View(hint);
        }

        // GET: Hints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Beschrijving,FontIcoon")] Hint hint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hint);
        }

        // GET: Hints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hints == null)
            {
                return NotFound();
            }

            var hint = await _context.Hints.FindAsync(id);
            if (hint == null)
            {
                return NotFound();
            }
            return View(hint);
        }

        // POST: Hints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Beschrijving,FontIcoon")] Hint hint)
        {
            if (id != hint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HintExists(hint.Id))
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
            return View(hint);
        }

        // GET: Hints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hints == null)
            {
                return NotFound();
            }

            var hint = await _context.Hints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hint == null)
            {
                return NotFound();
            }

            return View(hint);
        }

        // POST: Hints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hints == null)
            {
                return Problem("Entity set 'AdventureChallengeContext.Hints'  is null.");
            }
            var hint = await _context.Hints.FindAsync(id);
            if (hint != null)
            {
                _context.Hints.Remove(hint);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HintExists(int id)
        {
          return _context.Hints.Any(e => e.Id == id);
        }
    }
}
