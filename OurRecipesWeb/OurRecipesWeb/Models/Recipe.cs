using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OurRecipesWeb.Models
{
    public class Recipe
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [Required]
        [MaxLength(100)]
        [JsonProperty(PropertyName = "title")]
        public string? Title { get; set; }

        [JsonProperty(PropertyName = "created")]
        [DataType(DataType.Date)]
        public DateTime? Created { get; set; }

        [JsonProperty(PropertyName = "description")]
        [Required]
        [MaxLength(1000)]
        public string? Description { get; set; }

        [JsonProperty(PropertyName = "courseOfAction")]
        [Required]
        [MaxLength(5000)]
        public string? CourseOfAction { get; set; }

        [JsonProperty(PropertyName = "ingredients")]
        [Required]
        [MaxLength(5000)]
        public string? Ingredients { get; set; }

        [JsonProperty(PropertyName = "reference")]
        [MaxLength(200)]
        public string? Reference { get; set; }

        [JsonProperty(PropertyName = "author")]
        public Author? Author { get; set; }


    }
}
