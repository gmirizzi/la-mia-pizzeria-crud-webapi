using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private IPizzaRepository pizzaRepository;
        public PizzasController(IPizzaRepository _pizzaRepository)
        {
            this.pizzaRepository = _pizzaRepository;
        }

        [HttpGet]
        public IActionResult GetPizzas(string? search)
        {
            List<Pizza> pizzas = this.pizzaRepository.GetListByFilter(search);
            return Ok(pizzas);
        }

        [HttpGet]
        public IActionResult GetPizza(int id)
        {
            Pizza pizza = this.pizzaRepository.GetById(id);
            return Ok(pizza);
        }
    }
}
