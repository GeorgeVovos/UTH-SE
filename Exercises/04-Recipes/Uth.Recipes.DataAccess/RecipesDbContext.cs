using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Uth.Recipes.Domain;
using Uth.Recipes.Domain.Categories;
using Uth.Recipes.Domain.Images;
using Uth.Recipes.Domain.Ingredients;
using Uth.Recipes.Domain.Recipes;
using Uth.Recipes.Domain.Steps;

namespace Uth.Recipes.DataAccess
{
    public class RecipesDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<StepIngredient> StepIngredients { get; set; }
        public DbSet<StepImage> StepImages { get; set; }
        public DbSet<RecipeImage> RecipeImages { get; set; }

        public RecipesDbContext(DbContextOptions<RecipesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Category
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure Image
            modelBuilder.Entity<Image>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Image>()
                .Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Configure Ingredient
            modelBuilder.Entity<Ingredient>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Ingredient>()
                .Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure Recipe
            modelBuilder.Entity<Recipe>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Description)
                .HasMaxLength(1000);

            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.Category)
                .WithMany()
                .HasForeignKey(r => r.CategoryId);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Steps)
                .WithOne(s => s.Recipe)
                .HasForeignKey(s => s.RecipeId);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Images)
                .WithOne(ri => ri.Recipe)
                .HasForeignKey(ri => ri.RecipeId);

            // Configure Step
            modelBuilder.Entity<Step>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Step>()
                .Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Step>()
                .Property(s => s.Description)
                .HasMaxLength(1000);

            modelBuilder.Entity<Step>()
                .HasMany(s => s.Ingredients)
                .WithOne(si => si.Step)
                .HasForeignKey(si => si.StepId);

            modelBuilder.Entity<Step>()
                .HasMany(s => s.Images)
                .WithOne(si => si.Step)
                .HasForeignKey(si => si.StepId);

            // Configure StepIngredient (Many-to-Many)
            modelBuilder.Entity<StepIngredient>()
                .HasKey(si => new { si.StepId, si.IngredientId });

            modelBuilder.Entity<StepIngredient>()
                .HasOne(si => si.Step)
                .WithMany(s => s.Ingredients)
                .HasForeignKey(si => si.StepId);

            modelBuilder.Entity<StepIngredient>()
                .HasOne(si => si.Ingredient)
                .WithMany()
                .HasForeignKey(si => si.IngredientId);

            // Configure StepImage (Many-to-Many)
            modelBuilder.Entity<StepImage>()
                .HasKey(si => new { si.StepId, si.ImageId });

            modelBuilder.Entity<StepImage>()
                .HasOne(si => si.Step)
                .WithMany(s => s.Images)
                .HasForeignKey(si => si.StepId);

            modelBuilder.Entity<StepImage>()
                .HasOne(si => si.Image)
                .WithMany()
                .HasForeignKey(si => si.ImageId);

            // Configure RecipeImage (Many-to-Many)
            modelBuilder.Entity<RecipeImage>()
                .HasKey(ri => new { ri.RecipeId, ri.ImageId });

            modelBuilder.Entity<RecipeImage>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.Images)
                .HasForeignKey(ri => ri.RecipeId);

            modelBuilder.Entity<RecipeImage>()
                .HasOne(ri => ri.Image)
                .WithMany()
                .HasForeignKey(ri => ri.ImageId);


            SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category("Dessert") { Id = 1 },
                new Category("Main Course") { Id = 2 },
                new Category("Appetizer") { Id = 3 },
                new Category("Side Dish") { Id = 4 },
                new Category("Breakfast") { Id = 5 }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient("Eggs") { Id = 1 },
                new Ingredient("Flour") { Id = 2 },
                new Ingredient("Milk") { Id = 3 },
                new Ingredient("Sugar") { Id = 4 },
                new Ingredient("Chicken") { Id = 5 },
                new Ingredient("Salt") { Id = 6 },
                new Ingredient("Butter") { Id = 7 },
                new Ingredient("Pasta") { Id = 8 },
                new Ingredient("Tomatoes") { Id = 9 },
                new Ingredient("Cheese") { Id = 10 }
            );

            modelBuilder.Entity<StepIngredient>().HasData(
                new StepIngredient { StepId = 1, IngredientId = 2, Quantity = "200gr" }, // Flour
                new StepIngredient { StepId = 1, IngredientId = 1, Quantity = "2" }, // Eggs
                new StepIngredient { StepId = 1, IngredientId = 3, Quantity = "100ml" }, // Milk
                new StepIngredient { StepId = 3, IngredientId = 8, Quantity = "300gr" }, // Pasta
                new StepIngredient { StepId = 4, IngredientId = 10, Quantity = "50gr" }, // Cheese
                new StepIngredient { StepId = 5, IngredientId = 2, Quantity = "200gr" }, // Flour (Cake)
                new StepIngredient { StepId = 9, IngredientId = 7, Quantity = "100gr" } // Butter (Garlic Bread)
            );


            modelBuilder.Entity<Image>().HasData(
                new Image() { Id = 1, Data = GetImageBytes("Uth.Recipes.DataAccess.DemoImages.Pancake01.jpeg"), Name = "Pancake01.jpeg" },
                new Image() { Id = 2, Data = GetImageBytes("Uth.Recipes.DataAccess.DemoImages.Pancake02.jpeg"), Name = "Pancake02.jpeg" },
                new Image() { Id = 3, Data = GetImageBytes("Uth.Recipes.DataAccess.DemoImages.PancakesStep01A.jpg"), Name = "PancakesStep01A.jpg" },
                new Image() { Id = 4, Data = GetImageBytes("Uth.Recipes.DataAccess.DemoImages.PancakesStep01B.jpg"), Name = "PancakesStep01B.jpg" },
                new Image() { Id = 5, Data = GetImageBytes("Uth.Recipes.DataAccess.DemoImages.PancakesStep02A.png"), Name = "PancakesStep02A.png" },
                new Image() { Id = 6, Data = GetImageBytes("Uth.Recipes.DataAccess.DemoImages.PancakesStep02B.jpeg"), Name = "PancakesStep02B.jpeg" }
            );

            modelBuilder.Entity<RecipeImage>().HasData(
                new RecipeImage() { RecipeId = 1, ImageId = 1 },
                new RecipeImage() { RecipeId = 1, ImageId = 2 }
            );

            modelBuilder.Entity<StepImage>().HasData(
                new StepImage() { StepId = 1, ImageId = 3 },
                new StepImage() { StepId = 1, ImageId = 4 }
            );

            modelBuilder.Entity<StepImage>().HasData(
                new StepImage() { StepId = 2, ImageId = 5 },
                new StepImage() { StepId = 2, ImageId = 6 }
            );

            modelBuilder.Entity<Step>().HasData(
                // Pancakes
                new Step
                {
                    Id = 1,
                    RecipeId = 1,
                    Title = "Mix Ingredients",
                    Description = "Combine flour, eggs, milk, and sugar.",
                    Order = 1,
                    Duration = 10
                },
                new Step
                {
                    Id = 2,
                    RecipeId = 1,
                    Title = "Cook Pancakes",
                    Description = "Pour batter onto a hot griddle and cook until golden.",
                    Order = 2,
                    Duration = 15
                },

                // Spaghetti Carbonara
                new Step
                {
                    Id = 3,
                    RecipeId = 2,
                    Title = "Cook Pasta",
                    Description = "Boil pasta in salted water.",
                    Order = 1,
                    Duration = 10
                },
                new Step
                {
                    Id = 4,
                    RecipeId = 2,
                    Title = "Prepare Sauce",
                    Description = "Cook pancetta and mix with eggs and cheese.",
                    Order = 2,
                    Duration = 15
                },

                // Chocolate Cake
                new Step
                {
                    Id = 5,
                    RecipeId = 3,
                    Title = "Prepare Batter",
                    Description = "Mix flour, sugar, eggs, and cocoa powder.",
                    Order = 1,
                    Duration = 15
                },
                new Step
                {
                    Id = 6,
                    RecipeId = 3,
                    Title = "Bake Cake",
                    Description = "Bake at 350°F for 30 minutes.",
                    Order = 2,
                    Duration = 30
                },

                // Caesar Salad
                new Step
                {
                    Id = 7,
                    RecipeId = 4,
                    Title = "Prepare Dressing",
                    Description = "Mix olive oil, lemon juice, and anchovies.",
                    Order = 1,
                    Duration = 10
                },
                new Step
                {
                    Id = 8,
                    RecipeId = 4,
                    Title = "Toss Salad",
                    Description = "Toss lettuce with dressing and croutons.",
                    Order = 2,
                    Duration = 5
                },

                // Garlic Bread
                new Step
                {
                    Id = 9,
                    RecipeId = 5,
                    Title = "Prepare Butter",
                    Description = "Mix butter with garlic and herbs.",
                    Order = 1,
                    Duration = 5
                },
                new Step
                {
                    Id = 10,
                    RecipeId = 5,
                    Title = "Bake Bread",
                    Description = "Spread butter on bread and bake until crispy.",
                    Order = 2,
                    Duration = 10
                }
            );
            

            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    Id = 1,
                    Name = "Pancakes",
                    Description = "Fluffy pancakes perfect for breakfast.",
                    CategoryId = 5, // Breakfast
                    Difficulty = Difficulty.Easy
                },
                new Recipe
                {
                    Id = 2,
                    Name = "Spaghetti Carbonara",
                    Description = "Classic Italian pasta dish.",
                    CategoryId = 2, // Main Course
                    Difficulty = Difficulty.Medium
                },
                new Recipe
                {
                    Id = 3,
                    Name = "Chocolate Cake",
                    Description = "Rich and moist chocolate cake.",
                    CategoryId = 1, // Dessert
                    Difficulty = Difficulty.Medium
                },
                new Recipe
                {
                    Id = 4,
                    Name = "Caesar Salad",
                    Description = "Fresh and crunchy Caesar salad.",
                    CategoryId = 3, // Appetizer
                    Difficulty = Difficulty.Easy
                },
                new Recipe
                {
                    Id = 5,
                    Name = "Garlic Bread",
                    Description = "Crispy and buttery garlic bread.",
                    CategoryId = 4, // Side Dish
                    Difficulty = Difficulty.Easy
                }
            );
        }

        public static Byte[] GetImageBytes(string resourceName)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                var sss = typeof(RecipesDbContext).Assembly.GetManifestResourceNames();
                var manifestResourceStream = typeof(RecipesDbContext).Assembly.GetManifestResourceStream(resourceName);

                while ((read = manifestResourceStream
                           .Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }
    }

}
