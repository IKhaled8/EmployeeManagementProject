using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement;
using EmployeeManagement.Data;

namespace EmployeeManagement.Controllers
{
    public class SystemCodeDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SystemCodeDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SystemCodeDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SystemCodeDetails.Include(s => s.SystemCode);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SystemCodeDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCodeDetail = await _context.SystemCodeDetails
                .Include(s => s.SystemCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemCodeDetail == null)
            {
                return NotFound();
            }

            return View(systemCodeDetail);
        }

        // GET: SystemCodeDetails/Create
        public IActionResult Create()
        {
            ViewData["SystemCodeId"] = new SelectList(_context.SystemCodes, "Id", "Description");
            return View();
        }

        // POST: SystemCodeDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SystemCodeId,Code,Description,OrderNo,CreatedById,CreatedOn,ModifiedById,ModifieddOn")] SystemCodeDetail systemCodeDetail)
        {
            if (ModelState.IsValid)
            {
                systemCodeDetail.CreatedById="Khaled";
                systemCodeDetail.CreatedOn=DateTime.Now;
                systemCodeDetail.ModifiedById="Khaled";
                systemCodeDetail.ModifieddOn=DateTime.Now;
                _context.Add(systemCodeDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else {
                var errors = ModelState.Values.SelectMany(x=>x.Errors);
                string msg = "";
                foreach(var e in errors) {
                        msg += e.ErrorMessage;
                        if(e.Exception!= null) {
                            msg += "---" + e.Exception.Message; 
                        }   
                        msg += "<br/>";
                }
                throw new Exception("Model is not valid: " + msg);
            }
            ViewData["SystemCodeId"] = new SelectList(_context.SystemCodes, "Id", "Description", systemCodeDetail.SystemCodeId);
            return View(systemCodeDetail);
        }

        // GET: SystemCodeDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCodeDetail = await _context.SystemCodeDetails.FindAsync(id);
            if (systemCodeDetail == null)
            {
                return NotFound();
            }
            ViewData["SystemCodeId"] = new SelectList(_context.SystemCodes, "Id", "Description", systemCodeDetail.SystemCodeId);
            return View(systemCodeDetail);
        }

        // POST: SystemCodeDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SystemCodeId,Code,Description,OrderNo,CreatedById,CreatedOn,ModifiedById,ModifieddOn")] SystemCodeDetail systemCodeDetail)
        {
            if (id != systemCodeDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemCodeDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemCodeDetailExists(systemCodeDetail.Id))
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
            ViewData["SystemCodeId"] = new SelectList(_context.SystemCodes, "Id", "Description", systemCodeDetail.SystemCodeId);
            return View(systemCodeDetail);
        }

        // GET: SystemCodeDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCodeDetail = await _context.SystemCodeDetails
                .Include(s => s.SystemCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemCodeDetail == null)
            {
                return NotFound();
            }

            return View(systemCodeDetail);
        }

        // POST: SystemCodeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var systemCodeDetail = await _context.SystemCodeDetails.FindAsync(id);
            if (systemCodeDetail != null)
            {
                _context.SystemCodeDetails.Remove(systemCodeDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemCodeDetailExists(int id)
        {
            return _context.SystemCodeDetails.Any(e => e.Id == id);
        }
    }
}
