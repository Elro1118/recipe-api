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
  public class IngredientController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public IngredientController()
    {
      _context = new DatabaseContext();
    }

    // GET: api/Ingredient
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
    {
      return await _context.Ingredients.ToListAsync();
    }

    // select ingredientId from ingredients 
    // where ingredientId Not In (select p.ingredientID from recipes as p on dishes as d
    // inner join p.ingredientID = d.ingredientID where d.ingredientID = id )

    // [HttpGet("/IngredientsListOutDish/{id}")]
    // public async Task<ActionResult<IEnumerable<Ingredient>>> IngredientsListOutDish(int id)
    // {
    //   var result1 = _context.Ingredients.Include(i => i.Recipes).Where(w => w.Id == id);

    //   // return await _context.Ingredients.Include(i => i.Recipes).Where(w => w.Id == id).ToListAsync();


    // }

    // GET: api/Ingredient/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Ingredient>> GetIngredient(int id)
    {
      var ingredient = await _context.Ingredients.FindAsync(id);

      if (ingredient == null)
      {
        return NotFound();
      }

      return ingredient;
    }

    // PUT: api/Ingredient/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutIngredient(int id, Ingredient ingredient)
    {
      if (id != ingredient.Id)
      {
        return BadRequest();
      }

      _context.Entry(ingredient).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!IngredientExists(id))
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

    // POST: api/Ingredient
    [HttpPost]
    public async Task<ActionResult<Ingredient>> PostIngredient(Ingredient ingredient)
    {
      _context.Ingredients.Add(ingredient);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetIngredient", new { id = ingredient.Id }, ingredient);
    }

    // DELETE: api/Ingredient/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Ingredient>> DeleteIngredient(int id)
    {
      var ingredient = await _context.Ingredients.FindAsync(id);
      if (ingredient == null)
      {
        return NotFound();
      }

      _context.Ingredients.Remove(ingredient);
      await _context.SaveChangesAsync();

      return ingredient;
    }

    private bool IngredientExists(int id)
    {
      return _context.Ingredients.Any(e => e.Id == id);
    }
  }
}
