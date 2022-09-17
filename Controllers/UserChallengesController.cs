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
    public class UserChallengesController : Controller
    {
        private readonly AdventureChallengeContext _context;

        public UserChallengesController(AdventureChallengeContext context)
        {
            _context = context;
        }

        // GET: UserChallenges
        public async Task<IActionResult> Index()
        {
            var adventureChallengeContext = _context.UserChallenges.Include(u => u.Challenge).Include(u => u.Foto).Include(u => u.User);
            return View(await adventureChallengeContext.ToListAsync());
        }

        // GET: UserChallenges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserChallenges == null)
            {
                return NotFound();
            }

            var userChallenge = await _context.UserChallenges
                .Include(u => u.Challenge)
                .Include(u => u.Foto)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userChallenge == null)
            {
                return NotFound();
            }

            return View(userChallenge);
        }

        // GET: UserChallenges/Create
        public IActionResult Create()
        {
            ViewData["ChallengeId"] = new SelectList(_context.Challenges, "Id", "Id");
            ViewData["FotoId"] = new SelectList(_context.Fotos, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserChallenges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ChallengeId,Beschrijving,FotoId,Afgerond")] UserChallenge userChallenge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userChallenge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChallengeId"] = new SelectList(_context.Challenges, "Id", "Id", userChallenge.ChallengeId);
            ViewData["FotoId"] = new SelectList(_context.Fotos, "Id", "Id", userChallenge.FotoId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userChallenge.UserId);
            return View(userChallenge);
        }

        // GET: UserChallenges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserChallenges == null)
            {
                return NotFound();
            }

            var userChallenge = await _context.UserChallenges.FindAsync(id);
            if (userChallenge == null)
            {
                return NotFound();
            }
            ViewData["ChallengeId"] = new SelectList(_context.Challenges, "Id", "Id", userChallenge.ChallengeId);
            ViewData["FotoId"] = new SelectList(_context.Fotos, "Id", "Id", userChallenge.FotoId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userChallenge.UserId);
            return View(userChallenge);
        }

        // POST: UserChallenges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ChallengeId,Beschrijving,FotoId,Afgerond")] UserChallenge userChallenge)
        {
            if (id != userChallenge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userChallenge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserChallengeExists(userChallenge.Id))
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
            ViewData["ChallengeId"] = new SelectList(_context.Challenges, "Id", "Id", userChallenge.ChallengeId);
            ViewData["FotoId"] = new SelectList(_context.Fotos, "Id", "Id", userChallenge.FotoId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userChallenge.UserId);
            return View(userChallenge);
        }

        // GET: UserChallenges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserChallenges == null)
            {
                return NotFound();
            }

            var userChallenge = await _context.UserChallenges
                .Include(u => u.Challenge)
                .Include(u => u.Foto)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userChallenge == null)
            {
                return NotFound();
            }

            return View(userChallenge);
        }

        // POST: UserChallenges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserChallenges == null)
            {
                return Problem("Entity set 'AdventureChallengeContext.UserChallenges'  is null.");
            }
            var userChallenge = await _context.UserChallenges.FindAsync(id);
            if (userChallenge != null)
            {
                _context.UserChallenges.Remove(userChallenge);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserChallengeExists(int id)
        {
          return _context.UserChallenges.Any(e => e.Id == id);
        }
    }
}
