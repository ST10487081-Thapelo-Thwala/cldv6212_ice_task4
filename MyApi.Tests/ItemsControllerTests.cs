using Microsoft.EntityFrameworkCore;
using MyApi.Controllers;
using MyApi.Data;
using MyApi.Models;
using Xunit;

namespace MyApi.Tests;

public class ItemsControllerTests
{
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task PostItem_AddsItemToDatabase()
    {
        using var context = CreateContext();
        var controller = new ItemsController(context);
        var newItem = new Item { Name = "Test Widget" };

        await controller.PostItem(newItem);

        Assert.Equal(1, await context.Items.CountAsync());
    }

    [Fact]
    public async Task GetItems_ReturnsAllItems()
    {
        using var context = CreateContext();
        context.Items.Add(new Item { Name = "Widget A" });
        context.Items.Add(new Item { Name = "Widget B" });
        await context.SaveChangesAsync();
        var controller = new ItemsController(context);

        var result = await controller.GetItems();

        Assert.Equal(2, result.Value!.Count());
    }
}