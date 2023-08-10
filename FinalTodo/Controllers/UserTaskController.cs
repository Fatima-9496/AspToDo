using FinalTodo.Data;
using FinalTodo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace FinalTodo.Controllers
{
    public class UserTaskController : Controller
    {
        public AppDbContext _context;
        public UserTaskController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            List<UserTask> userTasks = _context.UserTasks.ToList();
            return View(userTasks);
        }

        public IActionResult Details(int id)
        {
            

            var userTask = _context.UserTasks.FirstOrDefault(m => m.TaskId == id);
            if (userTask != null)
            {
                return View(userTask);
                
            }
            else
                {
                return NotFound();
            }

            
        }
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,Title,Description,DueDate,IsCompleted")] UserTask uTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uTask);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserTasks == null)
            {
                return NotFound();
            }

            var uTask = await _context.UserTasks.FindAsync(id);
            if (uTask == null)
            {
                return NotFound();
            }
            return View(uTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,Title,Description,DueDate,IsCompleted")] UserTask uTask)
        {
            if (id != uTask.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(uTask.TaskId))
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
            return View(uTask);
        }

        
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uTask =  _context.UserTasks
                .FirstOrDefault(m => m.TaskId == id);
            if (uTask == null)
            {
                return NotFound();
            }

            return View(uTask);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var uTask = await _context.UserTasks.FindAsync(id);
            if (uTask != null)
            {
                _context.UserTasks.Remove(uTask);
            }
            else
            {
                return Problem("Entity set   is null.");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return (_context.UserTasks?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}
