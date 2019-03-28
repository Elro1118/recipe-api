namespace recipe_api.Modal
{
  public class Recipe
  {
    public int Id { get; set; }
    public int Quantity { get; set; }
    public string unit { get; set; }

    // Navigation  Properties
    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }
    public int DishId { get; set; }
    public Dish Dish
    {
      get; set;
    }

  }
}