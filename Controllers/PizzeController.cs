using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Repositories;
using la_mia_pizzeria_static.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    [Authorize]
    public class PizzeController : Controller
    {
        private IPizzaRepository pizzaRepository;
        public PizzeController(IPizzaRepository _pizzaRepository)
        {
            this.pizzaRepository = _pizzaRepository;
        }
        public IActionResult Index()
        {
            List<Pizza> pizzaList = pizzaRepository.GetList();
            return View(pizzaList);
        }

        public IActionResult Details(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza current = pizzaRepository.GetById(id);

                if (current == null)
                {
                    return NotFound($"La pizza con id {id} non è stato trovata");
                }
                else
                {
                    return View(current);
                }
            }

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }
            else
            {
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    model.Pizza.IngredientsList = new List<Ingredient>();
                    if (model.SelectedIngredients != null)
                    {
                        foreach (string ingredientId in model.SelectedIngredients)
                        {
                            Ingredient ingredient = db.Ingredients.Find(int.Parse(ingredientId));
                            model.Pizza.IngredientsList.Add(ingredient);
                        }
                    }
                    pizzaRepository = new DbPizzaRepository(db);
                    pizzaRepository.Create(model.Pizza);

                    return RedirectToAction(nameof(Index));
                }
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza current = pizzaRepository.GetById(id);

                if (current == null)
                {
                    return NotFound($"La pizza con id {id} non è stato trovata");
                }
                else
                {
                    PizzaViewModel newModel = new PizzaViewModel();
                    newModel.SelectedIngredients = new List<string>();
                    foreach (Ingredient ingredient in current.IngredientsList)
                    {
                        newModel.SelectedIngredients.Add(ingredient.IngredientId.ToString());
                    }
                    newModel.Pizza = current;
                    return View(newModel);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PizzaViewModel model)
        {
            model.Pizza.PizzaId = id;

            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            else
            {
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    Pizza current = pizzaRepository.GetById(id);

                    if (current == null)
                    {
                        return NotFound($"La pizza con id {id} non è stato trovata");
                    }
                    else
                    {
                        current.Name = model.Pizza.Name;
                        current.Description = model.Pizza.Description;
                        current.ImgPath = model.Pizza.ImgPath;
                        current.Price = model.Pizza.Price;
                        current.CategoryId = model.Pizza.CategoryId;
                        current.IngredientsList.Clear();
                        if (model.SelectedIngredients != null)
                        {
                            foreach (string ingredientId in model.SelectedIngredients)
                            {
                                Ingredient ingredient = db.Ingredients.Find(int.Parse(ingredientId));
                                model.Pizza.IngredientsList.Add(ingredient);
                            }
                        }
                        pizzaRepository.Update(current);
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza current = pizzaRepository.GetById(id);

                if (current == null)
                {
                    return NotFound($"La pizza con id {id} non è stato trovata");
                }
                else
                {
                    pizzaRepository.Delete(current);
                    return RedirectToAction(nameof(Index));
                }
            }
        }
    }
}

