using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zoo.ViewModels.Caretaker;
using ZooCon;
using ZooCon.Entities;

namespace Zoo.Controllers
{
    public class CaretakerController : Controller
    {
        private readonly ZooDbContext context;

        public CaretakerController(ZooDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var caretakers = await context.Caretakers
                .Include(x => x.AnimalCaretakers)
                .ThenInclude(y => y.Animal)
                .ToListAsync();
            var model = new List<CaretakerIndexViewModel>();
            foreach (var caretaker in caretakers)
            {
                model.Add(new CaretakerIndexViewModel
                {
                    Id = caretaker.Id,
                    FirstName = caretaker.FirstName,
                    LastName = caretaker.LastName,
                    Email = caretaker.Email,
                    PhoneNumber = caretaker.PhoneNumber,
                    AnimalName = string.Join(" ",
                    caretaker.AnimalCaretakers
                    .Select(dp => dp.Animal.Name + "-" + dp.Animal.Species))
                });
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CaretakerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var caretaker = new Caretaker
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                foreach (var item in model.AnimalIds)
                {
                    caretaker.AnimalCaretakers.Add(new AnimalCaretaker
                    {
                        AnimalId = item
                    });
                }
                context.Caretakers.Add(caretaker);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await LoadDoctors(model.AnimalIds);
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var caretakers = await context.Caretakers
             .Include(x => x.AnimalCaretakers)
             .ThenInclude(y => y.Animal).FirstOrDefaultAsync(x => x.Id == id);
            if (caretakers == null)
            {
                return NotFound();
            }
            var model = new CaretakerEditViewModel
            {
                Id = caretakers.Id,
                FirstName = caretakers.FirstName,
                LastName = caretakers.LastName,
                Email = caretakers.Email,
                PhoneNumber = caretakers.PhoneNumber,
                AnimalIds = caretakers.AnimalCaretakers.Select(x => x.AnimalId).ToList()
            };
            await LoadDoctors(model.AnimalIds);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CaretakerEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var p = await context.Caretakers.Include(x => x.AnimalCaretakers).FirstOrDefaultAsync(y => y.Id == id);
                if (p == null)
                {
                    return NotFound();
                }
                p.FirstName = model.FirstName;
                p.LastName = model.LastName;
                p.Email = model.Email;
                p.PhoneNumber = model.PhoneNumber;
                p.AnimalCaretakers.Clear();
                foreach (var item in model.AnimalIds)
                {
                    p.AnimalCaretakers.Add(new AnimalCaretaker
                    {
                        AnimalId = item,
                        CaretakerId = p.Id
                    });
                }
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await LoadDoctors();
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var caretakers = await context.Caretakers
             .Include(x => x.AnimalCaretakers)
             .ThenInclude(y => y.Animal).FirstOrDefaultAsync(x => x.Id == id);
            if (caretakers == null)
            {
                return NotFound();
            }
            var model = new CaretakerDetailsViewModel
            {
                Id = caretakers.Id,
                FirstName = caretakers.FirstName,
                LastName = caretakers.LastName,
                Email = caretakers.Email,
                PhoneNumber = caretakers.PhoneNumber,
                AnimalName = string.Join(" ",
                    caretakers.AnimalCaretakers
                    .Select(dp => dp.Animal.Name + "-" + dp.Animal.Species))
            };
            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            await LoadDoctors();
            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var p = await context.Caretakers.FindAsync(id);
            if (p != null)
            {
                context.Caretakers.Remove(p);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int id)
        {
            var p = await context.Caretakers.FindAsync(id);
            if (p == null)
            {
                return NotFound();

            }
            context.Caretakers.Remove(p);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDoctors(List<int> selectedD = null)
        {
            var animals = await context.Animals
            .Select(d => new
            {
                d.Id,
                FullName = d.Name + "-" + d.Species,
            }).ToListAsync();
            ViewBag.Animals = new MultiSelectList(
            animals,
            "Id",
            "FullName",
            selectedD
            );
        }
    }
}

