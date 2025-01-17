using System;
namespace Uth.Recipes.Domain.Categories
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private Category() { }

        public Category(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name can't be empty");

            Name = name;
        }
    }
}
