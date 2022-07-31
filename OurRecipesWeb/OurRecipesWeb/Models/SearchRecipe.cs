namespace OurRecipesWeb.Models
{
    public class SearchRecipe
    {
        public string? Title { get; set; }
        public string? Ingredients { get; set; }

        public IEnumerable<OurRecipesWeb.Models.Recipe>? result { get; set; }
    }
}
