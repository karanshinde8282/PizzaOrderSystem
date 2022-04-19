using System.Collections.Generic;

namespace PizzaServiceWebAPI.Model
{
    public class PizzaDetails : PizzaDetailsBase
    {
        public int Pizza_Type { get; set; }
        public List<PizzaIngredientsDetails> IngredientsDetailsList { get; set; } = new List<PizzaIngredientsDetails>();
    }
    public class PizzaIngredientsDetails : PizzaDetailsBase
    {

        public PizzaIngredientsDetailsType Ingredients_Type { get; set; }
        public List<PizzaIngredientsList> IngredientsSelectedList { get; set; } = new List<PizzaIngredientsList>();
        public List<PizzaIngredientsList> IngredientsList { get; set; } = new List<PizzaIngredientsList>();
    }

    public class PizzaIngredientsList : PizzaDetailsBase
    {
    }

    public enum PizzaIngredientsDetailsType
    {
        PizzaCrust,
        Sauce,
        Cheese,
        Toppings
    }
}
