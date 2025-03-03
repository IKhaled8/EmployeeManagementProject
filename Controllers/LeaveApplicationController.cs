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
    public class LeaveApplicationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveApplicationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaveApplication
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LeaveApplications
            .Include(l => l.Duration)
            .Include(l => l.Employee)
            .Include(l => l.LeaveType)
            .Include(l => l.Status);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaveApplication/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // GET: LeaveApplication/Create
        public IActionResult Create()
        {
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x=>x.SystemCode).Where(y=>y.SystemCode.Code=="LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            return View();
        }

        // POST: LeaveApplication/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,NoOfDays,StartDate,EndDate,DurationId,LeaveTypeId,Attachment,Description,StatusId,ApprovedById,ApprovedOn,CreatedById,CreatedOn,ModifiedById,ModifieddOn")] LeaveApplication leaveApplication)
        {
            var PendingStatus = await _context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Pending")
                .FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                leaveApplication.CreatedOn = DateTime.Now;
                leaveApplication.CreatedById = "Khaled-222222";
                _context.Add(leaveApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

        
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x=>x.SystemCode).Where(y=>y.SystemCode.Code=="LeaveDuration"), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }
        // GET: LeaveApplication/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x=>x.SystemCode).Where(y=>y.SystemCode.Code=="LeaveDuration"), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // POST: LeaveApplication/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,NoOfDays,StartDate,EndDate,DurationId,LeaveTypeId,Attachment,Description,StatusId,ApprovedById,ApprovedOn,CreatedById,CreatedOn,ModifiedById,ModifieddOn")] LeaveApplication leaveApplication)
        {
            if (id != leaveApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var PendingStatus = await _context.SystemCodeDetails
                    .Include(x => x.SystemCode)
                    .Where(y => y.SystemCode.Code == "Pending" && y.SystemCode.Code == "LeaveApprovalStatus")
                    .FirstOrDefaultAsync();

                try
                {
                    leaveApplication.ModifieddOn = DateTime.Now;
                    leaveApplication.ModifiedById = "Khaled";
                    // leaveApplication.StatusId = PendingStatus.Id;
                    _context.Update(leaveApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveApplicationExists(leaveApplication.Id))
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
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // GET: LeaveApplication/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            return View(leaveApplication);
        }

        // POST: LeaveApplication/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication != null)
            {
                _context.LeaveApplications.Remove(leaveApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveApplicationExists(int id)
        {
            return _context.LeaveApplications.Any(e => e.Id == id);
        }
    }
}



// else {
//                 var errors = ModelState.Values.SelectMany(x=>x.Errors);
//                 string msg = "";
//                 foreach(var e in errors) {
//                         msg += e.ErrorMessage;
//                         if(e.Exception!= null) {
//                             msg += "---" + e.Exception.Message; 
//                         }   
//                         msg += "<br/>";
//                 }   
//                 throw new Exception("Model is not valid: " + msg);
//             }
