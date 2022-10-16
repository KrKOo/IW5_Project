﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;
using Delivery.Common.Models.Order;

namespace Delivery.Api.BL.Facades
{
    public class OrderFacade : IOrderFacade
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public OrderFacade(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository; 
            this.mapper = mapper;
        }

        public Guid Create(OrderDetailModel orderModel)
        {
            MergeDishAmounts(orderModel);
            var orderEntity = mapper.Map<OrderEntity>(orderModel);
            return orderRepository.Insert(orderEntity);
        }

        public Guid CreateOrUpdate(OrderDetailModel orderModel)
        {
            return orderRepository.Exists(orderModel.Id)
                ? Update(orderModel)!.Value
                : Create(orderModel);
        }

        public void Delete(Guid id)
        {
            orderRepository.Remove(id);
        }

        public List<OrderListModel> GetAll()
        {
            var orderEntities = orderRepository.GetAll();
            return mapper.Map<List<OrderListModel>>(orderEntities);
        }

        public OrderDetailModel? GetById(Guid id)
        {
            var orderEntity = orderRepository.GetById(id);
            return mapper.Map<OrderDetailModel>(orderEntity);
        }

        public Guid? Update(OrderDetailModel orderModel)
        {
            MergeDishAmounts(orderModel);

            var orderEntity = mapper.Map<OrderEntity>(orderModel);
            orderEntity.DishAmounts = orderModel.DishAmounts.Select(t => 
                new DishAmountEntity
                (
                    t.Id,
                    t.Amount,
                    t.Dish.Id,
                    orderEntity.Id
                )).ToList();
                
            var result = orderRepository.Update(orderEntity);
            return result;
        }

        public void MergeDishAmounts(OrderDetailModel order)
        {
            var result = new List<OrderDetailDishModel>();
            var orderAmountGroups = order.DishAmounts.GroupBy(t => $"{t.Dish.Id}");

            foreach(var orderAmountGroup in orderAmountGroups)
            {
                var orderAmountFirst = orderAmountGroup.First();
                var totalAmount = orderAmountGroup.Sum(t => t.Amount);
                var orderAmount = orderAmountFirst with { Amount = totalAmount };

                result.Add(orderAmount);
            }

            order.DishAmounts = result;
        }
    }
}