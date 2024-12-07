namespace AdventOfCode.Y2024.Puzzle5.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this)).ToArray();

            var rules = new List<Rule>();
            var updates = new List<List<int>>();


            foreach (var line in lines)
            {
                if (line.Contains('|'))
                {
                    var split = line.Split('|');

                    rules.Add(new Rule
                    {
                        First = int.Parse(split[0]),
                        Second = int.Parse(split[1])
                    });
                }

                if (line.Contains(','))
                    updates.Add(line.Split(',').Select(int.Parse).ToList());
            }

            var middlePageSum = 0;

            foreach (var update in updates)
            {
                var applicableRules = 
                    rules.Where(r => update.Contains(r.First) && update.Contains(r.Second))
                        .ToList();

                if (!IsUpdatePageOrderValid(update, applicableRules))
                {
                    var correctedUpdatePageOrder = CorrectOrder(update, applicableRules);
                    middlePageSum += correctedUpdatePageOrder[correctedUpdatePageOrder.Count() / 2];
                }
            }

            Console.WriteLine(middlePageSum);
        }

        private static List<int> CorrectOrder(List<int> update, List<Rule> applicableRules)
        {
            var pagesSwapped = true;

            while (pagesSwapped)
            {
                pagesSwapped = false;

                for (var i = 0; i < update.Count(); i++)
                {
                    var currentPageIndex = i;
                    var currentPage = update[i];
                    var rulesForCurrentPage = applicableRules.Where(r => r.First == currentPage || r.Second == currentPage).ToList();
                    
                    foreach (var rule in rulesForCurrentPage)
                    {
                        if (currentPage == rule.First)
                        {
                            var indexOfSecondPageInRule = update.IndexOf(rule.Second);

                            if (currentPageIndex > indexOfSecondPageInRule)
                            {
                                update[currentPageIndex] = rule.Second;
                                update[indexOfSecondPageInRule] = currentPage;
                                pagesSwapped = true;
                                break;
                            }
                        }

                        if (currentPage == rule.Second)
                        {
                            var indexOfFirstPageInRule = update.IndexOf(rule.First);

                            if (currentPageIndex < indexOfFirstPageInRule)
                            {
                                update[currentPageIndex] = rule.First;
                                update[indexOfFirstPageInRule] = currentPage;
                                pagesSwapped = true;
                                break;
                            }
                        }
                    }

                    if (pagesSwapped) break;
                }
            }

            return update;
        }

        private static bool IsUpdatePageOrderValid(List<int> update, List<Rule> applicableRules)
        {
            foreach (var page in update)
            {
                var rulesForPage = applicableRules.Where(r => r.First == page || r.Second == page);

                foreach (var rule in rulesForPage)
                {
                    var indexOfFirst = update.IndexOf(rule.First);
                    var indexOfSecond = update.IndexOf(rule.Second);
                    var indexOfPage = update.IndexOf(page);

                    if (page == rule.First)
                    {
                        if (!(indexOfPage < indexOfSecond))
                            return false;
                    }

                    if (page == rule.Second)
                    {
                        if (!(indexOfPage > indexOfFirst))
                            return false;
                    }
                }
            }

            return true;
        }
    }

    public struct Rule
    {
        public int First { get; set; }
        public int Second { get; set; }
    }
}