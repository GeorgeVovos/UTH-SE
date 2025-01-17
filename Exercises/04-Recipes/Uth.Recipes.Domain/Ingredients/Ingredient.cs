using System;

namespace Uth.Recipes.Domain.Ingredients
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get;  set; }
        
        public Ingredient() { }

        public Ingredient(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Ingredient name can't be empty");

            Name = name;
        }

    }
}