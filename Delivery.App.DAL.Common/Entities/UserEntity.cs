using System;
//using AutoMapper;

namespace Delivery.Api.DAL.Common.Entities
{
    public record UserEntity : EntityBase
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }

        public UserEntity(Guid id, string username, string firstname, string surname) : base(id)
        {
            Username = username;
            Firstname = firstname;
            Surname = surname;
        }
    }

    //public class IngredientEntityMapperProfile : Profile
    //{
    //    public IngredientEntityMapperProfile()
    //    {
    //        CreateMap<IngredientEntity, IngredientEntity>();
    //    }
    //}
}
