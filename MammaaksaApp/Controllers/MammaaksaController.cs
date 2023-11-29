using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MammaaksaApp.Data;
using MammaaksaApp.Models;
using Microsoft.Data.SqlClient;
using System.Security.Principal;

namespace MammaaksaApp.Controllers
{
    public class MammaaksaController : Controller
    {
        private readonly MammaaksaDbContext _context;

        public MammaaksaController(MammaaksaDbContext context)
        {
            _context = context;
        }
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Account accounts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accounts);
                await _context.SaveChangesAsync();
                //return Content("Registration successful!");
                return RedirectToAction("Login", "Mammaaksa");
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        void connectionString()
        {
            con.ConnectionString = "server=.; database=Mammaaksa; integrated security = true; TrustServerCertificate=True";
        }
        [HttpPost]
        public IActionResult Verify(Account acc)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from accounts where maqaa = '" + acc.maqaa + "' and jecha_icciti='" + acc.jecha_icciti + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                //HttpContext.Session.SetString("StuId", acc.StuId);
                //HttpContext.Session.SetString("user", acc.Role = "");

                return RedirectToAction("Index", "Mammaaksa");


            }
            else
            {
                con.Close();
                return View("Login");
            }
        }
        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Mammaaksa");

        }

        // GET: Mammaaksa
        public async Task<IActionResult> Index()
        {
              return _context.Mammaaksa != null ? 
                          View(await _context.Mammaaksa.ToListAsync()) :
                          Problem("Entity set 'MammaaksaDbContext.Mammaaksa'  is null.");
        }

        // GET: Mammaaksa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mammaaksa == null)
            {
                return NotFound();
            }

            var mamaaksa = await _context.Mammaaksa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mamaaksa == null)
            {
                return NotFound();
            }

            return View(mamaaksa);
        }

        // GET: Mammaaksa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mammaaksa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,gaaffii,deebii")] Mamaaksa mamaaksa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mamaaksa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mamaaksa);
        }

        // GET: Mammaaksa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mammaaksa == null)
            {
                return NotFound();
            }

            var mamaaksa = await _context.Mammaaksa.FindAsync(id);
            if (mamaaksa == null)
            {
                return NotFound();
            }
            return View(mamaaksa);
        }

        // POST: Mammaaksa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,gaaffii,deebii")] Mamaaksa mamaaksa)
        {
            if (id != mamaaksa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mamaaksa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MamaaksaExists(mamaaksa.Id))
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
            return View(mamaaksa);
        }

        // GET: Mammaaksa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mammaaksa == null)
            {
                return NotFound();
            }

            var mamaaksa = await _context.Mammaaksa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mamaaksa == null)
            {
                return NotFound();
            }

            return View(mamaaksa);
        }

        // POST: Mammaaksa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mammaaksa == null)
            {
                return Problem("Entity set 'MammaaksaDbContext.Mammaaksa'  is null.");
            }
            var mamaaksa = await _context.Mammaaksa.FindAsync(id);
            if (mamaaksa != null)
            {
                _context.Mammaaksa.Remove(mamaaksa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MamaaksaExists(int id)
        {
          return (_context.Mammaaksa?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
