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
  public class RecipeController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public RecipeController()
    {
      _context = new DatabaseContext();
    }

    // GET: api/Recipe
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
    {
      return await _context.Recipes.Include(i => i.Ingredient).Include(i => i.Dish).OrderBy(o => o.DishId).ToListAsync();
    }

    // GET: api/Recipe/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe>> GetRecipe(int id)
    {
      var recipe = await _context.Recipes.FindAsync(id);

      if (recipe == null)
      {
        return NotFound();
      }

      return recipe;
    }

    // PUT: api/Recipe/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
    {
      if (id != recipe.Id)
      {
        return BadRequest();
      }

      _context.Entry(recipe).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!RecipeExists(id))
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

    // POST: api/Recipe
    [HttpPost]
    public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
    {
      _context.Recipes.Add(recipe);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
    }

    // DELETE: api/Recipe/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Recipe>> DeleteRecipe(int id)
    {
      var recipe = await _context.Recipes.FindAsync(id);
      if (recipe == null)
      {
        return NotFound();
      }

      _context.Recipes.Remove(recipe);
      await _context.SaveChangesAsync();

      return recipe;
    }

    private bool RecipeExists(int id)
    {
      return _context.Recipes.Any(e => e.Id == id);
    }
  }
}
