using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab2_DangHuuPhuc_CSE422.Data;
using Lab2_DangHuuPhuc_CSE422.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Lab2_DangHuuPhuc_CSE422.Controllers
{
    public class DevicesController : Controller
    {
        private readonly Lab2_DangHuuPhuc_CSE422Context _context;

        public DevicesController(Lab2_DangHuuPhuc_CSE422Context context)
        {
            _context = context;
        }

        // GET: Devices
        public async Task<IActionResult> Index(string searchString, int? deviceCategory)
        {
            if (_context.Device == null)
            {
                return Problem("Entity set Device is null.");
            }

            var devices = from device in _context.Device.Include(x => x.Category)
                         select device;

            if (!String.IsNullOrEmpty(searchString))
            {
                devices = devices.Where(device => (!string.IsNullOrEmpty(device.Name) && device.Name.ToUpper().Contains(searchString.ToUpper())) ||
                    (!string.IsNullOrEmpty(device.Code) && device.Code.ToUpper().Contains(searchString.ToUpper())));
            }
            if (deviceCategory != null)
            {
                devices = devices.Where(device => device.CategoryId == deviceCategory);
            }
            var deviceVM = new DeviceViewModel
            {
                Category = new SelectList(_context.Set<Category>(), nameof(Category.Id), nameof(Category.Name), deviceCategory),
                Devices = devices
            };
            ViewBag.Categories = await _context.Category.ToListAsync();
            ViewBag.Status = Enum.GetNames(typeof(Status));
            ViewData["SearchString"] = searchString;
            return View(deviceVM);
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Device
                .FirstOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Category.ToList(), nameof(Category.Id), nameof(Category.Name));
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Code,CategoryId,Status,DateOfEntry")] Device device)
        {
            if (ModelState.IsValid)
            {
                _context.Add(device);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(device);
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var device = await _context.Device.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            var devices = await _context.Device.Include(parameter => parameter.Category).FirstOrDefaultAsync(parameter => parameter.Id == id);
            ViewData["Categories"] = new SelectList(_context.Category.ToList(), nameof(Category.Id), nameof(Category.Name));
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Code,CategoryId,Status,DateOfEntry")] Device device)
        {
            if (id != device.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(device);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.Id))
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
            return View(device);
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Device
                .FirstOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var device = await _context.Device.FindAsync(id);
            if (device != null)
            {
                _context.Device.Remove(device);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(int id)
        {
            return _context.Device.Any(e => e.Id == id);
        }
    }
}
