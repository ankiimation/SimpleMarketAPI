using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMarketAPI.Entities;
using SimpleMarketAPI.Models;

namespace SimpleMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private static SIMPLEMARKETContext context = new SIMPLEMARKETContext();

        [HttpGet]
        [Authorize]
        public IActionResult getAllOrders()
        {
            string username = User.Identity.Name;
            var lstOrder = context.Orders.Where(order => order.Username.Equals(username)).ToList();
            var lstOrderResult = new List<OrderEntities>();
            foreach (Orders order in lstOrder)
            {
                var orderResult = new OrderEntities();
                orderResult.OrderId = order.OrderId;
                orderResult.Username = order.Username;
                orderResult.OrderPaymethod = order.OrderPaymethod;
                orderResult.OrderStatus = order.OrderStatus;
                orderResult.lstOrderDetail = context.OrderDetail.Where(orderdetail => orderdetail.OrderId.Equals(order.OrderId)).Select(orderdetail => new OrderEntities.OrderDetailEntities(orderdetail.ProductId, orderdetail.OrderdetailCount)).ToList();

                lstOrderResult.Add(orderResult);

            }

            return Ok(lstOrderResult);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult getOrder(int id)
        {
            string username = User.Identity.Name;
            var order = context.Orders.Where(order => order.Username.Equals(username)).ToList().FirstOrDefault(order => order.OrderId.Equals(id));
            OrderEntities orderResult = null;
            if (order != null)
            {
                orderResult = new OrderEntities();
                orderResult.OrderId = order.OrderId;
                orderResult.Username = order.Username;
                orderResult.OrderPaymethod = order.OrderPaymethod;
                orderResult.OrderStatus = order.OrderStatus;
                orderResult.lstOrderDetail = context.OrderDetail.Where(orderdetail => orderdetail.OrderId.Equals(order.OrderId)).Select(orderdetail => new OrderEntities.OrderDetailEntities(orderdetail.ProductId, orderdetail.OrderdetailCount)).ToList();
            }
           
            return Ok(orderResult);

        }

        [HttpPost]
        [Authorize]
        public IActionResult createOrder()
        {
            string username = User.Identity.Name;
            List<CartDetail> lstCartItem = context.CartDetail.Where(item => item.Username.Equals(username) && item.CartdetailCount > 0).ToList();
            if (lstCartItem.Count == 0)
            {
                return Ok("Cart is empty");
            }
            else
            {
                Orders order = new Orders();
                order.Username = username;
                order.OrderCreatedtime = DateTime.Now;
                order.OrderPaymethod = "UNKNOWN";

                context.Orders.Add(order);
                context.SaveChanges();

                //Add Order Detail
                foreach (CartDetail item in lstCartItem)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = order.OrderId;
                    orderDetail.ProductId = item.ProductId;
                    orderDetail.OrderdetailCount = item.CartdetailCount;
                    item.CartdetailCount = 0;
                    context.OrderDetail.Add(orderDetail);
                    context.CartDetail.Update(item);

                }
                context.SaveChanges();
                return Ok(order);
            }

        }
    }
}