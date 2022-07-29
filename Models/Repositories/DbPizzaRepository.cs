using la_mia_pizzeria_static.Models.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbPizzaRepository : IPizzaRepository
    {
        private PizzeriaContext context;

        public DbPizzaRepository(PizzeriaContext context)
         {
            this.context = context;
        }

       public void Create(Pizza pizza)
        {
                context.Pizzas.Add(pizza);
                context.SaveChanges();
        }

        public void Delete(Pizza pizza)
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {
                context.Pizzas.Remove(pizza);
                context.SaveChanges();
            }
        }

        public Pizza GetById(int id)
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {
                Pizza pizzaFound = context.Pizzas.Where(p => p.PizzaId == id).Include(p =>
                p.Category).Include(p => p.IngredientsList).FirstOrDefault();
                return pizzaFound;
            }
        }

        public List<Pizza> GetList()
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {
                IQueryable<Pizza> pizzas = context.Pizzas;
                return pizzas.ToList();
            }
        }

        public void Update(Pizza pizza)
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {
                context.Update(pizza);
                context.SaveChanges();
            }
        }

        public List<Pizza> GetListByFilter(string search)
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {
                IQueryable<Pizza> pizzas = context.Pizzas;
                if (search != null)
                {
                    pizzas = pizzas.Where(pizza => pizza.Name.ToLower().Contains(search.ToLower()));
                }
                return pizzas.ToList();
            }
        }
    }
}
