using System.Collections.Generic;

namespace recipe_api.Modal
{
  public class Dish
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Origin { get; set; }

    // Navegation Properties
    public List<Recipe> Recipes { get; set; }

  }
}