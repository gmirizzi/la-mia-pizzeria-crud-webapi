﻿using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    [Authorize]
    public class PizzeController : Controller
    {
        private DbPizzaRepository pizzaRepository;
        public PizzeController()
        {
            this.pizzaRepository = new DbPizzaRepository();
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
                PizzeriaContext db = new PizzeriaContext();
                model.Pizza.IngredientsList = new List<Ingredient>();
                foreach (string ingredientId in model.SelectedIngredients)
                {
                    Ingredient ingredient = db.Ingredients.Find(int.Parse(ingredientId));
                    model.Pizza.IngredientsList.Add(ingredient);
                }
                pizzaRepository.Create(model.Pizza);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
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
                        foreach (string ingredientId in model.SelectedIngredients)
                        {
                            Ingredient ingredient = db.Ingredients.Find(int.Parse(ingredientId));
                            current.IngredientsList.Add(ingredient);
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

