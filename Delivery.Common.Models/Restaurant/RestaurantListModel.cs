﻿namespace Delivery.Common.Models.Restaurant
{
    public record RestaurantListModel : IWithId
    {
        public required Guid Id { get; init; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Address { get; set; }
        public required string Gps { get; set; }
        public required double Revenue { get; set; }
        public string? LogoUrl { get; set; }
    }
}
