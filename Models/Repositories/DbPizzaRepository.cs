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
    }
}
