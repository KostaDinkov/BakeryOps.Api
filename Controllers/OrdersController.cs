using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Models;
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

        public OrdersController(OrdersDB db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        [HttpGet("getBetween/{startDate}/{endDate}")]

        public async Task<IActionResult> GetOrdersForPeriod(string startDate, string endDate) 
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
    }
}
