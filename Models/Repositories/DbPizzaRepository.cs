using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbPizzaRepository
    {
        public void Create(Pizza pizza)
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {
                context.Pizzas.Add(pizza);
                context.SaveChanges();
            }
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
    }
}
