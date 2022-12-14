using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdventureChallenge.Models;
using Newtonsoft.Json;
using AdventureChallenge.Models.FormModels;
using System.Drawing;
using System.Linq.Expressions;

namespace AdventureChallenge.Controllers
{
    public class ChallengeController : Controller
    {
        private readonly AdventureChallengeContext _context;

        public ChallengeController(AdventureChallengeContext context)
        {
            _context = context;
        }

        // GET: Challenges
        public async Task<IActionResult> Index()
        {
            return View(await _context.Challenges.ToListAsync());
        }

        // User Search function
        public async Task<IActionResult> Search()
        {
            //gets user session
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user"));
            var challangeId = CheckChallengeStart(user.Id);
            //check if user has a active challenge; 0 = no active challenge
            if (challangeId != 0)
            {
                return RedirectToAction("Details", "Challenge", new { id = challangeId });
            }
            return View();
        }

        //Post data from the search form
        [HttpPost, ActionName("Search")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search([Bind("Id,Prijs,Tijdstip,Personen,Status,Tijdduur,Opdracht,Icon0,Icon1,Icon2,Icon3,Icon4,Icon5,Icon6,Icon7,Icon8")] SearchForm searchForm)
        {
            //checks if all data is given
            if (ModelState.IsValid)
            {
                //switches to the right database searching field for the given amount of people
                string pAantal;
                switch (searchForm.Personen)
                {
                    case 1:
                        pAantal = "1";
                        break;
                    case 2:
                        pAantal = "2";
                        break;
                    case <= 4:
                        pAantal = "3-4";
                        break;
                    case > 4:
                        pAantal = ">4";
                        break;
                    default:
                        pAantal = null;
                        break;
                }
                //querries for a challenge matching the params
                var challenge = _context.Challenges.Where(u => u.Prijs <= searchForm.Prijs && u.Tijdstip == searchForm.Tijdstip && u.Personen == pAantal && u.Status == "Actief" && u.Tijdduur <= searchForm.Tijdduur).FirstOrDefault();
                //var challenge = _context.Challenges.Where(c => c.ChallengeHints.Any(ch => ch.Hint.Id != null && ch.Hint.Id.Equals(hintId))                ).FirstOrDefault(); vastgelopen op de search querry met de ook de juiste hints; als een icon binne komt als false moet hij niet op id in de search querry en als de op true is wel, dus dat is 81 verschillende querrys
                if (challenge == null)
                {
                    ModelState.AddModelError("CustomError", "Geen challenge voldoet aan gegeven eisen");
                }
                else
                {
                    var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user"));
                    //creates a userChallenge 
                    var userChallenge = new UserChallenge { UserId = user.Id, ChallengeId = challenge.Id, Beschrijving = null, FotoId = null, Afgerond = false };
                    _context.Add(userChallenge);
                    _context.SaveChanges();
                    int userChallengeId = (int)userChallenge.Id;

                    return RedirectToAction("Details", "Challenge", new { challenge.Id });
                }
            }
            return View();
        }

        private int CheckChallengeStart(int id)
        {
            var challenge = _context.UserChallenges.Where(u => u.UserId == id && !u.Afgerond).FirstOrDefault();
            if (challenge != null)
            {
                return challenge.ChallengeId;
            }
            return 0;
        }

        // GET: Challenges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Challenges == null)
            {
                return NotFound();
            }
            //gets the challenge
            var challenge = await _context.Challenges
                .FirstOrDefaultAsync(m => m.Id == id);
            //List<Hint> hintList = await _context.Hints.Join();
            List<Hint> hintList = _context.Hints.Join(_context.ChallengeHints, h => h.Id,
                                            ch => ch.Hint.Id, (h, ch) => new
                                            {
                                                HintID = h.Id,
                                                HintBeschrijving = h.Beschrijving,
                                                HintFontIcoon = h.FontIcoon,
                                                ChallengeID = ch.ChallengeId
                                            }).Where(x => x.ChallengeID == challenge.Id)
                                            .Select(x => new Hint
                                            {
                                                Id = x.HintID,
                                                Beschrijving = x.HintBeschrijving,
                                                FontIcoon = x.HintFontIcoon
                                            }).ToList();
            //var challenge = _context.Challenges.Where(c => c.ChallengeHints.Any(ch => ch.Hint.Id != null && ch.Hint.Id.Equals(hintId))                ).FirstOrDefault();
            if (challenge == null)
            {
                return NotFound();
            }
            var ChallengeForm = new ChallengeForm { ChallengeId = challenge.Id, Prijs = challenge.Prijs, Tijdstip = challenge.Tijdstip, Personen = challenge.Personen, Status = challenge.Status, Tijdduur = challenge.Tijdduur, Opdracht = challenge.Opdracht, Hints = hintList, Afgerond = true };

            return View(ChallengeForm);
        }
        //Post data from the search form
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details([Bind("ChallengeId,Prijs,Tijdstip,Personen,Tijdduur,Opdracht,Hints,Beschrijving,Afgerond,Foto")] ChallengeForm challengeForm)
        {
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user"));
            string beschrijving = challengeForm.Beschrijving;
            var userChallenge = await _context.UserChallenges.FirstOrDefaultAsync(m => m.UserId == user.Id && m.ChallengeId == challengeForm.ChallengeId && m.Afgerond == false);

            if (challengeForm.Beschrijving != null)
            {
                userChallenge.Beschrijving = challengeForm.Beschrijving;
                userChallenge.Afgerond = true;
                //userChallenge.Foto = challengeForm.Foto;

                try
                {
                    _context.Update(userChallenge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            else
            {
                return View(challengeForm);
            }
            return RedirectToAction("Index", "Home");
        }













































        // GET: Challenges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Challenges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Prijs,Tijdstip,Personen,Status,Tijdduur,Opdracht")] Challenge challenge, bool Icon0, bool Icon1, bool Icon2, bool Icon3, bool Icon4, bool Icon5, bool Icon6, bool Icon7, bool Icon8)
        {
            if (ModelState.IsValid)
            {
                _context.Add(challenge);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                List<bool> hints = new List<bool> { Icon0, Icon1, Icon2, Icon3, Icon4, Icon5, Icon6, Icon7, Icon8 };
                List<string> hintNames = new List<string> { };
                int i = 0;
                foreach (var hint in hints)
                {
                    if (hint)
                    {
                        switch (i)
                        {
                            case 0:
                                _context.Add(new ChallengeHint { ChallengeId = challenge.Id, HintId = 1 });
                                await _context.SaveChangesAsync();
                                break;
                            case 1:
                                _context.Add(new ChallengeHint { ChallengeId = challenge.Id, HintId = 2 });
                                await _context.SaveChangesAsync();
                                break;
                            case 2:
                                _context.Add(new ChallengeHint { ChallengeId = challenge.Id, HintId = 5 });
                                await _context.SaveChangesAsync();
                                break;
                            case 3:
                                _context.Add(new ChallengeHint { ChallengeId = challenge.Id, HintId = 6 });
                                await _context.SaveChangesAsync();
                                break;
                            case 4:
                                _context.Add(new ChallengeHint { ChallengeId = challenge.Id, HintId = 7 });
                                await _context.SaveChangesAsync();
                                break;
                            case 5:
                                _context.Add(new ChallengeHint { ChallengeId = challenge.Id, HintId = 8 });
                                await _context.SaveChangesAsync();
                                break;
                            case 6:
                                _context.Add(new ChallengeHint { ChallengeId = challenge.Id, HintId = 9 });
                                await _context.SaveChangesAsync();
                                break;
                            case 7:
                                _context.Add(new ChallengeHint { ChallengeId = challenge.Id, HintId = 10 });
                                await _context.SaveChangesAsync();
                                break;
                            case 8:
                                _context.Add(new ChallengeHint { ChallengeId = challenge.Id, HintId = 11 });
                                await _context.SaveChangesAsync();
                                break;
                            default:
                                break;
                        }
                    }
                    i++;
                }
                return RedirectToAction("Index", "Home");
            }
            return View(challenge);
        }













        // GET: Challenges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Challenges == null)
            {
                return NotFound();
            }
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge == null)
            {
                return NotFound();
            }
            return View(challenge);
        }

        // POST: Challenges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Prijs,Tijdstip,Personen,Status,Tijdduur,Opdracht")] Challenge challenge)
        {
            if (id != challenge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(challenge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChallengeExists(challenge.Id))
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
            return View(challenge);
        }








        // GET: Challenges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Challenges == null)
            {
                return NotFound();
            }

            var challenge = await _context.Challenges
                .FirstOrDefaultAsync(m => m.Id == id);
            if (challenge == null)
            {
                return NotFound();
            }

            return View(challenge);
        }

        // POST: Challenges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Challenges == null)
            {
                return Problem("Entity set 'AdventureChallengeContext.Challenges'  is null.");
            }
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge != null)
            {
                _context.Challenges.Remove(challenge);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChallengeExists(int id)
        {
            return _context.Challenges.Any(e => e.Id == id);
        }
    }
}
