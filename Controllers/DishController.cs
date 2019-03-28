using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipe_api;
using recipe_api.Modal;

namespace dotnet_sdg_template.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DishController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public DishController()
    {
      _context = new DatabaseContext();
    }

    // GET: api/Dish
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dish>>> GetDishes()
    {
      return await _context.Dishes.ToListAsync();
    }

    // GET: api/Dish/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Dish>> GetDish(int id)
    {
      var dish = await _context.Dishes.FindAsync(id);

      if (dish == null)
      {
        return NotFound();
      }

      return dish;
    }

    // PUT: api/Dish/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDish(int id, Dish dish)
    {
      if (id != dish.Id)
      {
        return BadRequest();
      }

      _context.Entry(dish).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!DishExists(id))
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

    // POST: api/Dish
    [HttpPost]
    public async Task<ActionResult<Dish>> PostDish(Dish dish)
    {
      _context.Dishes.Add(dish);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetDish", new { id = dish.Id }, dish);
    }

    // DELETE: api/Dish/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Dish>> DeleteDish(int id)
    {
      var dish = await _context.Dishes.FindAsync(id);
      if (dish == null)
      {
        return NotFound();
      }

      _context.Dishes.Remove(dish);
      await _context.SaveChangesAsync();

      return dish;
    }

    private bool DishExists(int id)
    {
      return _context.Dishes.Any(e => e.Id == id);
    }
  }
}
