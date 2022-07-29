namespace la_mia_pizzeria_static.Models.Repositories
{
    public class InMemoryPizzaRepository
    {
        private static List<Pizza> Pizzas = new List<Pizza>();

        public void Create(Pizza pizza)
        {
            pizza.PizzaId = Pizzas.Count;
            InMemoryPizzaRepository.Pizzas.Add(pizza);
        }

    }
}
