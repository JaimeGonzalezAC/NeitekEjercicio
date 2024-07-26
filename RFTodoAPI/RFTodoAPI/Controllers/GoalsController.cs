using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFTodoAPI.Data;
using RFTodoAPI.Models;

namespace RFTodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly ApiContext _context;

        public GoalsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Goals
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Goals>>> GetGoals()
        //{
        //  if (_context.Goals == null)
        //  {
        //      return NotFound();
        //  }
            
        //    return await _context.Goals.ToListAsync();
        //}

        // GET: api/Goals with
        [HttpGet("withtask")]
        public async Task<ActionResult<IEnumerable<GoalDto>>> GetGoalsWithTaskCounts()
        {
            if (_context.Goals == null)
            {
                return NotFound();
            }

            var goalsWithTaskCounts = await _context.Goals
                .Select(g => new GoalDto
                {
                    GoalId = g.GoalId,
                    Name = g.Name,
                    CreatedDate = g.CreatedDate,
                    TaskCount = g.Tasks.Count(),
                    CompletedTaskCount = g.Tasks.Count(t => t.Status == "Completada"),
                    CompletionPercentage = g.Tasks.Count() == 0 ? 0 : (double)g.Tasks.Count(t => t.Status == "Completada") / g.Tasks.Count() * 100
                })
                .ToListAsync();

            return goalsWithTaskCounts;
        }

        // GET: api/Goals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Goals>> GetGoals(Guid id)
        {
          if (_context.Goals == null)
          {
              return NotFound();
          }
            var goals = await _context.Goals.FindAsync(id);

            if (goals == null)
            {
                return NotFound();
            }

            return goals;
        }

        // PUT: api/Goals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoals(Guid id, Goals goals)
        {
            if (id != goals.GoalId)
            {
                return BadRequest();
            }

            _context.Entry(goals).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoalsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Goals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Goals>> PostGoals(Goals goals)
        {
          if (_context.Goals == null)
          {
              return Problem("Entity set 'ApiContext.Goals'  is null.");
          }
            _context.Goals.Add(goals);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGoals", new { id = goals.GoalId }, goals);
        }

        // DELETE: api/Goals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoals(Guid id)
        {
            if (_context.Goals == null)
            {
                return NotFound();
            }
            var goals = await _context.Goals.FindAsync(id);
            if (goals == null)
            {
                return NotFound();
            }

            _context.Goals.Remove(goals);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GoalsExists(Guid id)
        {
            return (_context.Goals?.Any(e => e.GoalId == id)).GetValueOrDefault();
        }
    }
}
