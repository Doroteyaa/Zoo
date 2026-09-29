using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoo.ViewModels.Zoo;
using ZooCon;

namespace Zoo.Controllers
{
    public class ZooController : Controller
    {
        private readonly ZooDbContext context;

        public ZooController(ZooDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var zoos = await context.Zoos
                .Where(h => !h.IsDeleted)
                .ToListAsync();

            var model = new List<ZooIndexViewModel>();

            foreach (var zoo in zoos)
            {
                model.Add(new ZooIndexViewModel
                {
                    Id = zoo.Id,
                    Name = zoo.Name,
                    Location = zoo.Location,
                    EstablishedYear = zoo.EstablishedYear,
                });
            }

            return View(model);
        }


        public async Task<IActionResult> Detail(int id)
        {
            var zoo = await context.Zoos.FindAsync(id);

            if (zoo == null)
            {
                return NotFound();
            }

            var model = new ZooDetailViewModel
            {
                Id = zoo.Id,
                Name = zoo.Name,
                Location = zoo.Location,
                Email = zoo.Email,
                PhoneNumber =  zoo.PhoneNumber,
                EstablishedYear = zoo.EstablishedYear,
                CreatedOn = zoo.CreatedOn
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ZooCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var zoo = new ZooCon.Entities.Zoo
                {
                    Name = model.Name,
                    Location = model.Location,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    EstablishedYear = model.EstablishedYear,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                };

                context.Zoos.Add(zoo);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var zoo = await context.Zoos.FindAsync(id);

            if (zoo == null)
            {
                return NotFound();
            }

            var model = new ZooEditViewModel
            {
                Id = zoo.Id,
                Name = zoo.Name,
                Location = zoo.Location,
                Email = zoo.Email,
                PhoneNumber = zoo.PhoneNumber,
                EstablishedYear = zoo.EstablishedYear
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ZooEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var zoo = await context.Zoos.FindAsync(id);

                if (zoo == null)
                {
                    return NotFound();
                }

                zoo.Name = model.Name;
                zoo.Location = model.Location;
                zoo.Email = model.Email;
                zoo.PhoneNumber = model.PhoneNumber;
                zoo.EstablishedYear = model.EstablishedYear;

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var zoo = await context.Zoos.FindAsync(id);

            if (zoo == null)
            {
                return NotFound();
            }

            var model = new ZooDeleteViewModel
            {
                Id = zoo.Id,
                Name = zoo.Name
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zoo = await context.Zoos.FindAsync(id);

            if (zoo != null)
            {
                zoo.IsDeleted = true;

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
