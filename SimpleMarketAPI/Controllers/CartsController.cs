using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class CartsController : ControllerBase
    {
        private static SIMPLEMARKETContext context = new SIMPLEMARKETContext();

        [HttpGet]
        [Authorize]
        //public IActionResult test()
        //{
        //    string username = User.Identity.Name;
        //    return Ok(username);
        //}

        [HttpGet]
        [Authorize]
        public IActionResult getAllCartItems()
        {
            string username = User.Identity.Name;
            List<CartItemEntities> lstCartItems = context.CartDetail.Where(item => item.Username.Equals(username)).Select(item=>new CartItemEntities(item.Username,item.ProductId, item.CartdetailCount)).ToList();
            return Ok(lstCartItems);
        }

        [HttpPost("{productID}")]
        [Authorize]
        public IActionResult addCartItem(string productId)
        {
            string username = User.Identity.Name;
            var cartItem = context.CartDetail.FirstOrDefault(item => item.Username.Equals(username) && item.ProductId.Equals(productId));
            if(cartItem != null)
            {
                cartItem.CartdetailCount++;
                context.CartDetail.Update(cartItem);
                context.SaveChanges();
            }
            else
            {
                cartItem = new CartDetail();
                cartItem.ProductId = productId;
                cartItem.Username = username;
                cartItem.CartdetailCount = 1;
                context.CartDetail.Add(cartItem);
                context.SaveChanges();
            }
            CartItemEntities cartItemResult = new CartItemEntities( cartItem.Username,cartItem.ProductId, cartItem.CartdetailCount);
  
            return Ok(cartItemResult);

           
        }

        [HttpPut("{productID}/{count}")]
        [Authorize]
        public IActionResult addCartItem( string productId,int count)
        {
            string username = User.Identity.Name;
            if (count <= 0) //CHECK IF COUNT <= 0 and set COUNT = 0
                count = 0;
            var cartItem = context.CartDetail.FirstOrDefault(item => item.Username.Equals(username) && item.ProductId.Equals(productId));
            if (cartItem != null)
            {
                cartItem.CartdetailCount=count;
                context.CartDetail.Update(cartItem);
                context.SaveChanges();
            }
            else
            {
                cartItem = new CartDetail();
                cartItem.ProductId = productId;
                cartItem.Username = username;
                cartItem.CartdetailCount = count;
                context.CartDetail.Add(cartItem);
                context.SaveChanges();
            }
            CartItemEntities cartItemResult = new CartItemEntities(cartItem.Username, cartItem.ProductId, cartItem.CartdetailCount);

            return Ok(cartItemResult);


        }


    }
}