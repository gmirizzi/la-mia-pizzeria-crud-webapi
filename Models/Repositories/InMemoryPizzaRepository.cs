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

        public void Delete(Pizza pizza)
        {
            int indicePizzaDaEliminare = -1;
            for (int i = 0; i < InMemoryPizzaRepository.Pizzas.Count; i++)
            {
                Pizza pizzaToCheck = InMemoryPizzaRepository.Pizzas[i];
                if (pizzaToCheck.PizzaId == pizza.PizzaId)
                {
                    indicePizzaDaEliminare = i;
                }
            }
            if (indicePizzaDaEliminare != -1)
            {
                InMemoryPizzaRepository.Pizzas.RemoveAt(indicePizzaDaEliminare);
            }
        }

        public Pizza GetById(int id)
        {
            Pizza pizzaDaTrovare = null;
            for (int i = 0; i < InMemoryPizzaRepository.Pizzas.Count; i++)
            {
                Pizza pizzaToCheck = InMemoryPizzaRepository.Pizzas[i];
                if (pizzaToCheck.PizzaId == id)
                {
                    pizzaDaTrovare = pizzaToCheck;
                }
            }
            return pizzaDaTrovare;
        }

        public List<Pizza> GetList()
        {
            return InMemoryPizzaRepository.Pizzas;
        }
    }
}
