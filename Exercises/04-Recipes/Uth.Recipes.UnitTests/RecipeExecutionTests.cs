using Uth.Recipes.Domain.RecipeExecution;
using Uth.Recipes.Domain.Recipes;

namespace Uth.Recipes.UnitTests
{
    public class RecipeExecutionTests
    {
        [Fact]
        public void StepBasedRecipeExecution_NoAction_Success()
        {
            //Arrange
            List<RecipeStepExecutionData> steps = CreateStepData();
            StepBasedRecipeExecution executionStrategy = new StepBasedRecipeExecution(steps);

            //Act
            //Do Nothing


            //Assert
            Assert.Equal(1, executionStrategy.CurrentStepIndex);
            Assert.Equal(0, executionStrategy.CurrentProgress);
        }

        [Fact]
        public void StepBasedRecipeExecution_Once_Success()
        {
            //Arrange
            List<RecipeStepExecutionData> steps = CreateStepData();
            StepBasedRecipeExecution executionStrategy = new StepBasedRecipeExecution(steps);

            //Act
            executionStrategy.MoveToNextStep();


            //Assert
            Assert.Equal(2, executionStrategy.CurrentStepIndex);
            Assert.Equal(33.333, Math.Round(executionStrategy.CurrentProgress, 3));
        }

        [Fact]
        public void StepBasedRecipeExecution_Success()
        {
            //Arrange
            List<RecipeStepExecutionData> steps = CreateStepData();
            StepBasedRecipeExecution executionStrategy = new StepBasedRecipeExecution(steps);

            //Act
            executionStrategy.MoveToNextStep();
            executionStrategy.MoveToNextStep();
            executionStrategy.MoveToNextStep();

            //Assert
            Assert.True(executionStrategy.IsCompleted());
            Assert.Equal(100, executionStrategy.CurrentProgress);
        }

        [Fact]
        public void DurationBasedRecipeExecution_NoAction_Success()
        {
            //Arrange
            List<RecipeStepExecutionData> steps = CreateStepData();
            DurationBasedRecipeExecution executionStrategy = new DurationBasedRecipeExecution(steps);

            //Act
            //Do Nothing


            //Assert
            Assert.Equal(1, executionStrategy.CurrentStepIndex);
            Assert.Equal(0, executionStrategy.CurrentProgress);
        }

        [Fact]
        public void DurationBasedRecipeExecution_Once_Success()
        {
            //Arrange
            List<RecipeStepExecutionData> steps = CreateStepData();
            DurationBasedRecipeExecution executionStrategy = new DurationBasedRecipeExecution(steps);

            //Act
            executionStrategy.MoveToNextStep();


            //Assert
            Assert.Equal(2, executionStrategy.CurrentStepIndex);
            Assert.Equal(12.5, Math.Round(executionStrategy.CurrentProgress, 3));
        }

        [Fact]
        public void DurationBasedRecipeExecution_Twice_Success()
        {
            //Arrange
            List<RecipeStepExecutionData> steps = CreateStepData();
            DurationBasedRecipeExecution executionStrategy = new DurationBasedRecipeExecution(steps);

            //Act
            executionStrategy.MoveToNextStep();
            executionStrategy.MoveToNextStep();

            //Assert
            Assert.Equal(3, executionStrategy.CurrentStepIndex);
            Assert.Equal(50, Math.Round(executionStrategy.CurrentProgress, 3));
        }

        [Fact]
        public void DurationBasedRecipeExecution_Success()
        {
            //Arrange
            List<RecipeStepExecutionData> steps = CreateStepData();
            DurationBasedRecipeExecution executionStrategy = new DurationBasedRecipeExecution(steps);

            //Act
            executionStrategy.MoveToNextStep();
            executionStrategy.MoveToNextStep();
            executionStrategy.MoveToNextStep();

            //Assert
            Assert.True(executionStrategy.IsCompleted());
            Assert.Equal(100, executionStrategy.CurrentProgress);
        }


        private static List<RecipeStepExecutionData> CreateStepData()
        {
            return new List<RecipeStepExecutionData>()
            {
                new RecipeStepExecutionData(1, 5),
                new RecipeStepExecutionData(2, 15),
                new RecipeStepExecutionData(3, 20),
            };
        }
    }
}
