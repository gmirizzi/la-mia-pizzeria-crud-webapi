using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPizzas(string? searched)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                if (searched != null)
                {
                    IQueryable<Pizza> pizze = db.Pizzas.Where(pizza => pizza.Name.Contains(searched.ToLower()));
                    return Ok(pizze.ToList());
                }
                else
                {
                    IQueryable<Pizza> pizze = db.Pizzas;
                    return Ok(pizze.ToList());
                }
            }
        }
    }
}
