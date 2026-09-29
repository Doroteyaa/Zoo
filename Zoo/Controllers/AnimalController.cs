using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Zoo.ViewModels.Animal;
using ZooCon;
using ZooCon.Entities;

namespace Zoo.Controllers
{
    public class AnimalController : Controller
    {
        private readonly ZooDbContext context;

        public AnimalController(ZooDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var animals = await context.Animals
                .Include(d => d.Zoo)
                .ToListAsync();

            var model = new List<AnimalIndexViewModel>();

            foreach (var animal in animals)
            {
                model.Add(new AnimalIndexViewModel
                {
                    Id = animal.Id,
                    Name = animal.Name,
                    Species = animal.Species,
              
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var animal = await context.Animals
                .Include(d => d.Zoo)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            var model = new AnimalDetailsViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                Species = animal.Species,
                WeightKg = animal.WeightKg,
                Age = animal.Age,
                ZooName = animal.Zoo.Name
            };

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Zoos = new SelectList(
            await context.Zoos
         .Where(h => !h.IsDeleted)
         .ToListAsync(),
             "Id",
            "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnimalCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var animal = new Animal
                {
                    Name = model.Name,
                    Species = model.Species,
                    Age = model.Age,
                    WeightKg = model.WeightKg,
                    ZooId = model.ZooId
                };

                context.Animals.Add(animal);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
           
            ViewBag.Zoos = new SelectList(
    await context.Zoos
        .Where(h => !h.IsDeleted)
        .ToListAsync(),
    "Id",
    "Name");

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var animal = await context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            var model = new AnimalEditViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                Species = animal.Species,
                Age = animal.Age,
                WeightKg = animal.WeightKg,
                ZooId = animal.ZooId
            };

            ViewBag.Zoos = new SelectList(
                    await context.Zoos
                        .Where(h => !h.IsDeleted)
                        .ToListAsync(),
                    "Id",
                    "Name");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AnimalEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var animal = await context.Animals.FindAsync(id);

                if (animal == null)
                {
                    return NotFound();
                }

                animal.Name = model.Name;
                animal.Species = model.Species;
                animal.Age = model.Age;
                animal.WeightKg = model.WeightKg;
                animal.ZooId = model.ZooId;

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Zoos = new SelectList(
                  await context.Zoos
                      .Where(h => !h.IsDeleted)
                      .ToListAsync(),
                  "Id",
                  "Name");

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var animal = await context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            var model = new AnimalDeleteViewModel
            {
                Id = animal.Id,
               Name = animal.Name,
               Species = animal.Species
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await context.Animals.FindAsync(id);

            if (animal != null)
            {
                context.Animals.Remove(animal);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
