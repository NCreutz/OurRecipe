using Microsoft.AspNetCore.Mvc;
using OurRecipesWeb.Models;
using OurRecipesWeb.Services;

namespace OurRecipesWeb.Controllers
{
    public class RecipeController : Controller
    {

        private readonly ICosmosDbService _cosmosDbService;

        
        public RecipeController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [ActionName("Search")]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id, Title, Reference, Description, Ingredients, CourseOfAction")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                recipe.Id = Guid.NewGuid().ToString();
                recipe.Created = DateTime.Now;
                recipe.Author = new Author
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Chief One"
                };
                await _cosmosDbService.AddItemAsync(recipe);
                return RedirectToAction("Show");
            }

            return View(recipe);
        }

        [HttpPost]
        [ActionName("Search")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Search([Bind("Title, Ingredients")] SearchRecipe searchRecipe)
        {
            if (ModelState.IsValid)
            {
                return View(searchRecipe);
            }

            return View(searchRecipe);
        }

        [ActionName("Show")]
        public async Task<IActionResult> Show()
        {
            return View(await _cosmosDbService.GetItemsAsync("SELECT * FROM c"));
        }

    }
}
