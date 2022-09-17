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
    public class ChallengeHintsController : Controller
    {
        private readonly AdventureChallengeContext _context;

        public ChallengeHintsController(AdventureChallengeContext context)
        {
            _context = context;
        }

        // GET: ChallengeHints
        public async Task<IActionResult> Index()
        {
            var adventureChallengeContext = _context.ChallengeHints.Include(c => c.Challenge).Include(c => c.Hint);
            return View(await adventureChallengeContext.ToListAsync());
        }

        // GET: ChallengeHints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChallengeHints == null)
            {
                return NotFound();
            }

            var challengeHint = await _context.ChallengeHints
                .Include(c => c.Challenge)
                .Include(c => c.Hint)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (challengeHint == null)
            {
                return NotFound();
            }

            return View(challengeHint);
        }

        // GET: ChallengeHints/Create
        public IActionResult Create()
        {
            ViewData["ChallengeId"] = new SelectList(_context.Challenges, "Id", "Id");
            ViewData["HintId"] = new SelectList(_context.Hints, "Id", "Id");
            return View();
        }

        // POST: ChallengeHints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ChallengeId,HintId")] ChallengeHint challengeHint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(challengeHint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChallengeId"] = new SelectList(_context.Challenges, "Id", "Id", challengeHint.ChallengeId);
            ViewData["HintId"] = new SelectList(_context.Hints, "Id", "Id", challengeHint.HintId);
            return View(challengeHint);
        }

        // GET: ChallengeHints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChallengeHints == null)
            {
                return NotFound();
            }

            var challengeHint = await _context.ChallengeHints.FindAsync(id);
            if (challengeHint == null)
            {
                return NotFound();
            }
            ViewData["ChallengeId"] = new SelectList(_context.Challenges, "Id", "Id", challengeHint.ChallengeId);
            ViewData["HintId"] = new SelectList(_context.Hints, "Id", "Id", challengeHint.HintId);
            return View(challengeHint);
        }

        // POST: ChallengeHints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChallengeId,HintId")] ChallengeHint challengeHint)
        {
            if (id != challengeHint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(challengeHint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChallengeHintExists(challengeHint.Id))
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
            ViewData["ChallengeId"] = new SelectList(_context.Challenges, "Id", "Id", challengeHint.ChallengeId);
            ViewData["HintId"] = new SelectList(_context.Hints, "Id", "Id", challengeHint.HintId);
            return View(challengeHint);
        }

        // GET: ChallengeHints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChallengeHints == null)
            {
                return NotFound();
            }

            var challengeHint = await _context.ChallengeHints
                .Include(c => c.Challenge)
                .Include(c => c.Hint)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (challengeHint == null)
            {
                return NotFound();
            }

            return View(challengeHint);
        }

        // POST: ChallengeHints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChallengeHints == null)
            {
                return Problem("Entity set 'AdventureChallengeContext.ChallengeHints'  is null.");
            }
            var challengeHint = await _context.ChallengeHints.FindAsync(id);
            if (challengeHint != null)
            {
                _context.ChallengeHints.Remove(challengeHint);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChallengeHintExists(int id)
        {
          return _context.ChallengeHints.Any(e => e.Id == id);
        }
    }
}
