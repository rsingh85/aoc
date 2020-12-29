using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode.Puzzle21.Part1
{
    public class Solution : ISolution
    {
        private List<Food> _food = new List<Food>();
        private List<string> _allergens = new List<string>();
        private Dictionary<string, List<string>> _ingredientPossibleAllergens = new Dictionary<string, List<string>>();

        public void Run()
        {
            ParseInput(File.ReadAllLines(@"Puzzle21\Part1\Input.txt"));

            foreach (var allergen in _allergens)
            {
                var foodsThatMayContainAllergen =
                    _food.Where(f => f.Allergens.Contains(allergen)).ToList();

                var foodIngredientsThatMayContainAllergen = foodsThatMayContainAllergen.Select(f => f.Ingredients).ToList();

                var ingredientIntersectionAcrossFoodsThatMayContainAllergen = foodIngredientsThatMayContainAllergen
                    .Skip(1)
                    .Aggregate(
                        new HashSet<string>(foodIngredientsThatMayContainAllergen.First()),
                        (h, e) => { h.IntersectWith(e); return h; }
                    );

                foreach (var ingredient in ingredientIntersectionAcrossFoodsThatMayContainAllergen)
                {
                    if (_ingredientPossibleAllergens.ContainsKey(ingredient))
                    {
                        _ingredientPossibleAllergens[ingredient].Add(allergen);
                    }
                    else
                    {
                        _ingredientPossibleAllergens.Add(ingredient, new List<string> { allergen });
                    }
                }
            }

            var allIngredients = _food.SelectMany(f => f.Ingredients).Distinct().ToList();

            var nonAllergenIngredientCount = 0;
            foreach (var ingredient in allIngredients)
            {
                if (!_ingredientPossibleAllergens.ContainsKey(ingredient))
                {
                    // this ingredient doesn't have any allergens!
                    nonAllergenIngredientCount += _food.Where(f => f.Ingredients.Contains(ingredient)).Count();
                }
            }

            Console.WriteLine(nonAllergenIngredientCount);
        }

        private void ParseInput(string[] lines)
        {
            var id = 1;

            foreach (var line in lines)
            {
                var ingredients = Regex.Match(line, @"^[a-z\s]+").Value
                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                var allergens = Regex.Match(line, @"\(contains\s([a-z\s,]+)").Groups[1].Value
                    .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                var food = new Food
                {
                    Id = id++,
                    Ingredients = new List<string>(),
                    Allergens = new List<string>()
                };

                foreach (var ingredient in ingredients)
                {
                    food.Ingredients.Add(ingredient);
                }

                foreach (var allergen in allergens)
                {
                    food.Allergens.Add(allergen);

                    if (!_allergens.Contains(allergen))
                    {
                        _allergens.Add(allergen);
                    }
                }

                _food.Add(food);
            }
        }

        private class Food
        {
            public int Id { get; set; }
            public List<string> Ingredients { get; set; }
            public List<string> Allergens { get; set; }

            public override string ToString()
            {
                return Id.ToString();
            }
        }
    }
}