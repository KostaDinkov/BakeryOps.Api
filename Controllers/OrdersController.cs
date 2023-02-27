using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Data;
using Orders.Models;
using Orders.Models.DTOs;
using Orders.Services;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Orders.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrdersController : Controller
    {  
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        { 
            this.ordersService = ordersService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await ordersService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet]

        public async Task<IActionResult> GetOrdersBetween([FromQuery][Required] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (HttpContext.Request.Query.ContainsKey("endDate"))
            {
                var orders = await ordersService.GetOrdersBetween(startDate, endDate);
                return Ok(orders);
            }
            else
            {
                return Ok(await ordersService.GetOrdersFor(startDate));
            }
        }

        

        [HttpPost]
        public async Task<IActionResult> PostOrder(OrderDTO orderDto)
        {
            var order = await ordersService.AddOrder(orderDto);
            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderDTO update)
        {
            var order = await ordersService.UpdateOrder(id, update);
            return Ok(order);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await ordersService.DeleteOrder(id);
            return Ok(order);
        }
    }
}
