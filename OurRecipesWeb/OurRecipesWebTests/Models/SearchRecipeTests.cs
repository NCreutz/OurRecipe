using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OurRecipesWeb.Models;

namespace OurRecipesWebTests.Models
{
    [TestClass]
    public class SearchRecipeTests
    {
        [TestMethod]
        public void SearchTitle()
        {
            Recipe recipe1 = new Recipe();
            recipe1.Title = "ds";
            Recipe recipe2 = new Recipe();
            recipe2.Title = "Græsk Pastasalat";
            Recipe recipe3 = new Recipe();
            recipe3.Title = "Pandekager med kylling";
            Recipe recipe4 = new Recipe();
            recipe4.Title = "Vegetar taco med blomkål og mangosalsa";
            IEnumerable<Recipe> recipes = new List<Recipe>() { recipe1, recipe2, recipe3, recipe4};

            var parser = new Regex(@"Græsk Pastasalat", RegexOptions.Compiled);
            IEnumerable<Recipe> result =
                recipes.Where(recipe => parser.IsMatch(recipe.Title)).ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.IsNotNull(result.First(recipe => recipe.Title == recipe2.Title));

            parser = new Regex(@"Græsk", RegexOptions.Compiled);
            result =
                recipes.Where(recipe => parser.IsMatch(recipe.Title)).ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.IsNotNull(result.First(recipe => recipe.Title == recipe2.Title));
            
            parser = new Regex(@"Pastasalat", RegexOptions.Compiled);
            result =
                recipes.Where(recipe => parser.IsMatch(recipe.Title)).ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.IsNotNull(result.First(recipe => recipe.Title == recipe2.Title));

            parser = new Regex(@"Pasta", RegexOptions.Compiled);
            result =
                recipes.Where(recipe => parser.IsMatch(recipe.Title)).ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.IsNotNull(result.First(recipe => recipe.Title == recipe2.Title));

            String[] pattern_list = { "Græsk", "Pasta" };
            String regex = String.Format("({0})", String.Join("|", pattern_list));

            parser = new Regex(regex, RegexOptions.Compiled);
            result =
                recipes.Where(recipe => parser.IsMatch(recipe.Title)).ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.IsNotNull(result.First(recipe => recipe.Title == recipe2.Title));

            String[] pattern_listA = { "Pasta", "Græsk" };
             regex = String.Format("({0})", String.Join("|", pattern_listA));

            parser = new Regex(regex, RegexOptions.Compiled);
            result =
                recipes.Where(recipe => parser.IsMatch(recipe.Title)).ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.IsNotNull(result.First(recipe => recipe.Title == recipe2.Title));

            String[] pattern_list2 = { "æsk", "ast" };
            regex = String.Format("({0})", String.Join("|", pattern_list2));

            parser = new Regex(regex, RegexOptions.Compiled);
            result =
                recipes.Where(recipe => parser.IsMatch(recipe.Title)).ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.IsNotNull(result.First(recipe => recipe.Title == recipe2.Title));

            String[] pattern_list3 = {"ast", "æsk" };
            regex = String.Format("({0})", String.Join("|", pattern_list3));

            parser = new Regex(regex, RegexOptions.Compiled);
            result =
                recipes.Where(recipe => parser.IsMatch(recipe.Title)).ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.IsNotNull(result.First(recipe => recipe.Title == recipe2.Title));

            String[] pattern_list4 = { "(?=.*r)(?=.*med)" };
            regex = String.Format("({0})", String.Join("", pattern_list4));

            parser = new Regex(regex, RegexOptions.Compiled);
            result =
                recipes.Where(recipe => parser.IsMatch(recipe.Title)).ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsNotNull(result.First(recipe => recipe.Title == recipe3.Title));


            List<string> stringList = recipe3.Title.Split(' ').ToList();
            List<string> stringListChanged = AddANDOperator(stringList);

            regex = String.Format("({0})", String.Join("", stringListChanged.ToArray()));

            parser = new Regex(regex, RegexOptions.Compiled);
            result =
                recipes.Where(recipe => parser.IsMatch(recipe.Title)).ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.IsNotNull(result.First(recipe => recipe.Title == recipe3.Title));

        }

        public List<string> AddANDOperator(List<string> searchList)
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
