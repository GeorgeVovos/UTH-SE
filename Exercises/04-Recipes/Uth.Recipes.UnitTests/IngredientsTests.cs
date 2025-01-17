using Uth.Recipes.Domain.Ingredients;

namespace Uth.Recipes.UnitTests
{
    public class IngredientsTests
    {
        [Fact]
        public void CreateInvalidIngredient_EmptyName_Fail()
        {
            //Arrange
            Ingredient ingredient = null;

            //Act
            Action act = () => ingredient = new Ingredient(string.Empty);


            //Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void CreateInvalidIngredient_Null_Fail()
        {
            //Arrange
            Ingredient ingredient = null;

            //Act
            Action act = () => ingredient = new Ingredient(null);


            //Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void CreateInvalidIngredient_Null_Success()
        {
            //Arrange
            Ingredient ingredient = null;

            //Act
            ingredient = new Ingredient("Eggs");


            //Assert
            Assert.Equal("Eggs", ingredient.Name);
        }
    }
}
