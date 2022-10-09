using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Enums;
using Xunit;

namespace Delivery.API.DAL.IntegrationTests;

public class OrderRepositoryTests
{
    public OrderRepositoryTests()
    {
        dbFixture = new InMemoryDatabaseFixture();
    }

    private readonly IDatabaseFixture dbFixture;

    [Fact]
    public void GetById_Returns_Requested_Recipe_Including_Their_IngredientAmounts()
    {
        //arrange
        var orderRepository = dbFixture.GetOrderRepository();

        //act
        var order = orderRepository.GetById(dbFixture.OrderGuids[0]);

        //assert
        Assert.Equal(dbFixture.OrderGuids[0], order.Id);
        Assert.Equal("Fast please", order.Note);

        Assert.Equal(2, order.DishAmounts.Count);
        var dishAmount1 =
            Assert.Single(order.DishAmounts.Where(entity => entity.Id == dbFixture.DishAmountGuids[0]));
        var dishAmount2 =
            Assert.Single(order.DishAmounts.Where(entity => entity.Id == dbFixture.DishAmountGuids[1]));

        Assert.Equal(dbFixture.OrderGuids[0], dishAmount1.OrderId);
        Assert.Equal(dbFixture.OrderGuids[0], dishAmount2.OrderId);

        Assert.NotNull(dishAmount1.Dish);
        Assert.Equal("Pizza", dishAmount1.Dish.Name);

        Assert.NotNull(dishAmount2.Dish);
        Assert.Equal("Funghi", dishAmount2.Dish.Description);
    }

    /*
    [Fact]
    public void Insert_Saves_Recipe_And_IngredientAmounts()
    {
        //arrange
        var recipeRepository = dbFixture.GetRecipeRepository();

        var ingredientAmountId = Guid.NewGuid();

        var recipeId = Guid.NewGuid();
        var foodType = FoodType.MainDish;
        var duration = TimeSpan.FromMinutes(5);
        var newRecipe = new RecipeEntity("Name", "Description", duration, foodType, "ImageUrl")
        {
            Id = recipeId,
            IngredientAmounts = new List<IngredientAmountEntity>()
            {
                new IngredientAmountEntity(ingredientAmountId, 5, Unit.Pieces, recipeId,
                    dbFixture.IngredientGuids[0])
            }
        };
        
        //act
        recipeRepository.Insert(newRecipe);

        //assert
        var recipe = dbFixture.GetRecipeDirectly(recipeId);
        Assert.NotNull(recipe);
        Assert.Equal(foodType,recipe.FoodType);
        Assert.Equal(duration,recipe.Duration);

        var ingredientAmount = dbFixture.GetIngredientAmountDirectly(ingredientAmountId);
        Assert.NotNull(ingredientAmount);
    }

    [Fact]
    public void Update_Saves_NewIngredientAmount()
    {
        //arrange
        var recipeRepository = dbFixture.GetRecipeRepository();

        var recipeId = dbFixture.RecipeGuids[0];
        var recipe = dbFixture.GetRecipeDirectly(recipeId);
        var ingredientAmountId = Guid.NewGuid();

        var newIngredientAmount =
            new IngredientAmountEntity(ingredientAmountId, 5, Unit.Pieces, recipeId, dbFixture.IngredientGuids[0]);

        //act
        recipe.IngredientAmounts.Add(newIngredientAmount);
        recipeRepository.Update(recipe);

        //assert
        var recipeFromDb = dbFixture.GetRecipeDirectly(recipeId);
        Assert.NotNull(recipeFromDb);
        Assert.Single(recipeFromDb.IngredientAmounts.Where(t => t.Id == ingredientAmountId));

        var ingredientAmountFromDb = dbFixture.GetIngredientAmountDirectly(ingredientAmountId);
        Assert.NotNull(ingredientAmountFromDb);

    }

    [Fact]
    public void Update_Also_Updates_IngredientAmount()
    {
        //arrange
        var recipeRepository = dbFixture.GetRecipeRepository();

        var recipeId = dbFixture.RecipeGuids[0];
        var recipe = dbFixture.GetRecipeDirectly(recipeId);
        var ingredientAmount = recipe.IngredientAmounts.First();
        
        //act
        ingredientAmount.Amount = 3;
        recipeRepository.Update(recipe);

        //assert
        var recipeFromDb = dbFixture.GetRecipeDirectly(recipeId);
        Assert.NotNull(recipeFromDb);
        var ingredientAmountFromDb = recipeFromDb.IngredientAmounts.First();
        Assert.Equal(ingredientAmount.Id,ingredientAmountFromDb.Id);
        Assert.Equal(3,ingredientAmountFromDb.Amount);
    }

    [Fact]
    public void Update_Removes_IngredientAmount()
    {
        //arrange
        var recipeRepository = dbFixture.GetRecipeRepository();

        var recipeId = dbFixture.RecipeGuids[0];
        var recipe = dbFixture.GetRecipeDirectly(recipeId);
        var ingredientAmountId = recipe.IngredientAmounts.First().Id;

        //act
        recipe.IngredientAmounts.Clear();
        recipeRepository.Update(recipe);

        //assert
        var recipeFromDb = dbFixture.GetRecipeDirectly(recipeId);
        Assert.NotNull(recipeFromDb);
        Assert.Empty(recipeFromDb.IngredientAmounts);

        var ingredientAmountFromDb = dbFixture.GetIngredientAmountDirectly(ingredientAmountId.Value);
        Assert.Null(ingredientAmountFromDb);
    }
*/
}
