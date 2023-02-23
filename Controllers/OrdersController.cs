using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Data;
using Orders.Models;
using Orders.Models.DTOs;
using Orders.Services;
using System.Globalization;
using System.Linq;

namespace Orders.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly OrdersDB db;
        private readonly IConfiguration configuration;
        private readonly IOrdersService ordersService;

        public OrdersController(OrdersDB db, IConfiguration configuration, IOrdersService ordersService)
        {
            this.db = db;
            this.configuration = configuration;
            this.ordersService = ordersService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await db.Orders.FindAsync(id);
            if(order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("between")]

        public async Task<IActionResult> GetOrdersBetween([FromQuery] string startDate,[FromQuery] string endDate) 
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var format = "dd-MM-yyyy";
            var date1Parsed = DateTime.ParseExact(startDate, format, provider);
            var date2Parsed = DateTime.ParseExact(endDate, format, provider);

            var orders = await db.Orders.Where(order => order.PickupDate <= date2Parsed && order.PickupDate >= date1Parsed).ToListAsync();

            return Ok(orders);
        }

        [HttpGet("forDate/{date}")]
        public async Task<IActionResult> GetOrdersForDate(string date)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var format = "dd-MM-yyyy";
            var dateParsed = DateTime.ParseExact(date, format, provider);

            var orders = await db.Orders.Where(order => order.PickupDate.Day == dateParsed.Day && order.PickupDate.Month == dateParsed.Month && order.PickupDate.Year == dateParsed.Year).ToListAsync();

            return Ok(orders);
        }

        [HttpPost]

        public async Task<IActionResult> PostOrder(OrderDTO orderDto)
        {
            var orderId = await ordersService.AddOrder(orderDto);
            

            return Redirect($"/api/orders/{orderId}");
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutOrder(int id, OrderDTO update)
        {
            var order = await db.Orders.FindAsync(id);
            if (order is null)
            {
                return NotFound();
            }

            order.OperatorId = (int)update.Id;
            order.PickupDate = (DateTime)update.PickupDate;
            
            order.ClientName = update.ClientName;
            order.ClientPhone = update.ClientPhone;
            order.AdvancePaiment = (decimal)update.AdvancePaiment;
            order.IsPaid = (bool)update.IsPaid;
            order.Status = (Status)update.Status;
            
            //TODO clear the old order items from the database
            order.OrderItems = new List<OrderItem>();

            foreach (var item in update.OrderItems)
            {

                var orderItem = new OrderItem();

                orderItem.Description = item.Description;
                orderItem.CakeFoto = item.CakeFoto;
                orderItem.CakeTitle = item.CakeTitle;
                orderItem.ProductId = item.ProductId;
                orderItem.ProductAmount = item.ProductAmount;
                orderItem.IsInProgress = item.IsInProgress;
                orderItem.IsComplete = item.IsComplete;
                order.OrderItems.Add(orderItem);
            }

            await db.SaveChangesAsync();
            return Ok(order);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await db.Orders.FindAsync(id);

            if(order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
