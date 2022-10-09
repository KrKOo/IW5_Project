using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;
using Delivery.Api.DAL.Memory;
using Delivery.Api.DAL.Memory.Repositories;
using Delivery.Common.Enums;
using Newtonsoft.Json;

namespace Delivery.API.DAL.IntegrationTests;

public class InMemoryDatabaseFixture : IDatabaseFixture
{
    public DishAmountEntity? GetDishAmountDirectly(Guid dishAmountId)
    {
        var dishAmount = inMemoryStorage.Value.DishAmounts.SingleOrDefault(t => t.Id == dishAmountId);

        return DeepClone(dishAmount);
    }

    public OrderEntity? GetOrderDirectly(Guid orderId)
    {
        var order = inMemoryStorage.Value.Orders.SingleOrDefault(t => t.Id == orderId);
        if (order == null)
        {
            return null;
        }

        order.DishAmounts = inMemoryStorage.Value.DishAmounts.Where(t => t.OrderId == orderId).ToList();


        return DeepClone(order);
    }

    private T DeepClone<T>(T input)
    {
        var json = JsonConvert.SerializeObject(input);
        return JsonConvert.DeserializeObject<T>(json);
    }

    public IOrderRepository GetOrderRepository()
    {
        return new OrderRepository(inMemoryStorage.Value);
    }

    public IList<Guid> DishGuids { get; } = new List<Guid>
    {
        new("df935095-8709-4040-a2bb-b6f97cb416dc"),
        new("23b3902d-7d4f-4213-9cf0-112348f56238")
    };

    public IList<Guid> DishAmountGuids { get; } = new List<Guid>
    {
        new("0d4fa150-ad80-4d46-a511-4c666166ec5e"),
        new("87833e66-05ba-4d6b-900b-fe5ace88dbd8")
    };

    public IList<Guid> OrderGuids { get; } = new List<Guid> { new("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e") };
    public IList<Guid> RestaurantGuids { get; } = new List<Guid> { new("08b44b69-00df-4478-ab5b-fd726aa3fbb9") };

    private readonly Lazy<Storage> inMemoryStorage;

    public InMemoryDatabaseFixture()
    {
        inMemoryStorage = new Lazy<Storage>(CreateInMemoryStorage);
    }

    private Storage CreateInMemoryStorage()
    {
        var storage = new Storage(false);
        SeedStorage(storage);
        return storage;
    }

    private void SeedStorage(Storage storage)
    {
        storage.Dishes.Add(new DishEntity(DishGuids[0], "Pizza", "Salami", 4.20, "4, 2"));
        storage.Dishes.Add(new DishEntity(DishGuids[1], "Pizza", "Funghi", 4.20, "4, 2"));


        storage.DishAmounts.Add(
            new DishAmountEntity(DishAmountGuids[0], 2, DishGuids[0], OrderGuids[0])
            {
                Id = DishAmountGuids[0]
            });

        storage.DishAmounts.Add(
            new DishAmountEntity(DishAmountGuids[1], 1, DishGuids[1], OrderGuids[0])
            {
                Id = DishAmountGuids[1]
            });

        storage.Orders.Add(new OrderEntity(
            OrderGuids[0],
            "266 Keeling Land",
            TimeSpan.FromMinutes(15),
            "Fast please",
            OrderStates.Sent,
            RestaurantGuids[0])
        {
            Id = OrderGuids[0]
        });
    }
}
