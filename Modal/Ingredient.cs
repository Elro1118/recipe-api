using System.Collections.Generic;

namespace recipe_api.Modal
{
  public class Ingredient
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public bool Organic { get; set; } = false;

    public List<Recipe> Recipes { get; set; }
  }
}