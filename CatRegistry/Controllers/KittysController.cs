using CatRegistry.Data;
using CatRegistry.Domain;
using CatRegistry.Dto;
using CatRegistry.Models.Kittys;
using CatRegistry.ServiceInterface;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace CatRegistry.Controllers
{
    public class KittysController : Controller
    {


        private readonly KittyContext _context;
        private readonly IKittyServices _kittyServices;
        private readonly IFileServices _fileServices;

        public KittysController(KittyContext context, IKittyServices kittyServices, IFileServices fileServices)
        {
            _context = context;
            _kittyServices = kittyServices;
            _fileServices = fileServices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Name = HttpContext.Session.GetString("Name");
            ViewBag.Name = Name;    

            var resultingInventory = _context.Kittys
                .OrderByDescending(y => y.KittyDescription)
                .Select(x => new KittyIndexViewModel
                {
                    KittyId = x.KittyId,
                    KittySpeciesName = x.KittySpeciesName,
                    KittyRegionOfOrigin = x.KittyRegionOfOrigin,
                    KittyDescription = x.KittyDescription,
                });
            return View(resultingInventory);
        }
        [HttpGet]
        public IActionResult Create()
        {
            KittyCreateViewModel vm = new();
            return View("Create", vm);
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KittyCreateViewModel vm)
        {
            var dto = new KittyDto
            {
                KittySpeciesName = vm.KittySpeciesName,
                KittyRegionOfOrigin = vm.KittyRegionOfOrigin,
                KittyDescription = vm.KittyDescription,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    ID = x.ImageID,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    KittyId = x.KittyId,
                }).ToArray()
            };
            var result = await _kittyServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            //if (id == null)
            //{
            // return NotFound();
            //}

            var kitty = await _kittyServices.DetailsAsync(id);

            if (kitty == null)
            {
                return NotFound();
            }


            var images = await _context.FileToDatabase
                .Where(c => c.KittyId == id)
                .Select(y => new KittyImageViewModel
                {
                    KittyId = y.Id,
                    ImageID = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new KittyDetailsViewModel();
            vm.KittyId = kitty.KittyId;
            vm.KittySpeciesName = kitty.KittySpeciesName;
            vm.KittyRegionOfOrigin = kitty.KittyRegionOfOrigin;
            vm.KittyDescription = kitty.KittyDescription;
            vm.Image.AddRange(images);

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            if (id == null) { return NotFound(); }

            var kitty = await _kittyServices.DetailsAsync(id);

            if (id == null) { return NotFound(); }

            var images = await _context.FileToDatabase
                .Where(x => x.KittyId == id)
                .Select(y => new KittyImageViewModel
                {
                    KittyId = y.KittyId,
                    ImageID = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new KittyCreateViewModel();
            vm.KittySpeciesName = kitty.KittySpeciesName;
            vm.KittyRegionOfOrigin = kitty.KittyRegionOfOrigin;
            vm.KittyDescription = kitty.KittyDescription;

            return View("Update", vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null) { return NotFound(); }

            var kitty = await _kittyServices.DetailsAsync(id);

            if (id == null) { return NotFound(); }
            ;

            var images = await _context.FileToDatabase
                .Where(x => x.KittyId == id)
                .Select(y => new KittyImageViewModel
                {
                    KittyId = y.Id,
                    ImageID = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();
            var vm = new KittyDeleteViewModel();

            vm.KittyId = kitty.KittyId;
            vm.KittySpeciesName = kitty.KittySpeciesName;
            vm.KittyRegionOfOrigin = kitty.KittyRegionOfOrigin;
            vm.KittyDescription = kitty.KittyDescription;
            vm.Image.AddRange(images);

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var ToDelete = await _kittyServices.Delete(id);

            if (ToDelete == null) { return RedirectToAction("Index"); }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveImage(KittyImageViewModel vm)
        {
            var dto = new FileToDatabase()
            {
                Id = vm.ImageID
            };
            var image = await _fileServices.RemoveImageFromDatabase(dto);
            if (image == null) { return RedirectToAction("Index"); }
            return RedirectToAction("Index");
        }
    }
}