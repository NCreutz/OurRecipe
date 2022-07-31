using Microsoft.AspNetCore.Mvc;
using OurRecipesWeb.Models;
using OurRecipesWeb.Services;
using System.Text.RegularExpressions;

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
                IEnumerable<Recipe> recipes = _cosmosDbService.GetAllItemsCached();
                IEnumerable<Recipe> enumerable = search(searchRecipe, recipes);
                searchRecipe.result = enumerable;
                return View(searchRecipe);
            }

            return View(searchRecipe);
        }

        [ActionName("Show")]
        public async Task<IActionResult> Show()
        {
            return View(await _cosmosDbService.GetItemsAsync("SELECT * FROM c"));
        }

        private IEnumerable<Recipe> search(SearchRecipe searchRecipe, IEnumerable<Recipe> recipes)
        {
            List<string> stringList = searchRecipe.Title.Split(' ').ToList();
            List<string> stringListChanged = AddANDOperator(stringList);

            string regex = String.Format("({0})", String.Join("", stringListChanged.ToArray()));

            var parser = new Regex(regex, RegexOptions.Compiled);
            IEnumerable<Recipe> result =
                recipes.Where(recipe => parser.IsMatch(recipe.Title)).ToList();
            return result;
        }

        private List<string> AddANDOperator(List<string> searchList)
        {
            List<string> stringListChanged = new List<string>();
            foreach (string word in searchList)
            {
                stringListChanged.Add("(?=.*" + word + ")");
            }

            return stringListChanged;
        }

    }
}
