using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode.Puzzle21.Part2
{
    public class Solution : ISolution
    {
        private List<Food> _food = new List<Food>();
        private List<string> _allergens = new List<string>();
        private Dictionary<string, List<string>> _ingredientPossibleAllergens = new Dictionary<string, List<string>>();

        public void Run()
        {
            ParseInput(File.ReadAllLines(@"Y2020\Puzzle21\Part2\Input.txt"));

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

            // while there are any ingredients still mapped more than one allergen
            while (_ingredientPossibleAllergens.Any(m => m.Value.Count > 1))
            {
                // go through each ingredient that's currently mapped to one allergen only, and remove that
                // allergen from all the other sets
                foreach (var ingredient in _ingredientPossibleAllergens.Where(i => i.Value.Count == 1))
                {
                    var ingredientsMappedToMoreThanOneAllergen = _ingredientPossibleAllergens.Where(i => i.Value.Count > 1);

                    foreach (var ingredientMappedToMoreThanOneAllergen in ingredientsMappedToMoreThanOneAllergen)
                    {
                        ingredientMappedToMoreThanOneAllergen.Value.Remove(ingredient.Value.First());
                    }
                }
            }

            var dangerousIngredients = string.Join(",", _ingredientPossibleAllergens.OrderBy(i => i.Value.First()).Select(i => i.Key));
            Console.WriteLine(dangerousIngredients);
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