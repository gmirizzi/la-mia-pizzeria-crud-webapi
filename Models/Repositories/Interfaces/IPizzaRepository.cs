namespace la_mia_pizzeria_static.Models.Repositories.Interfaces
{
    public interface IPizzaRepository
    {
        public List<Pizza> GetList();
        public Pizza GetById(int id);
        public void Create(Pizza pizza);
        public void Update(Pizza pizza);
        public void Delete(Pizza pizza);
    }
}
