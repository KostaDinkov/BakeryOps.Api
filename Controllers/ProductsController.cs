using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Models;

namespace Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly OrdersDB db;

        public ProductsController(OrdersDB db)
        {
            this.db = db;   
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(db.Products.ToListAsync());
        }
    }
}
