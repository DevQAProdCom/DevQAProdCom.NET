namespace DevQAProdCom.NET.Global.Helpers
{
    public static class DataGenerationHelper
    {
        public static List<List<string>> GeneratePairwiseCombinations(List<string> elements)
        {
            var result = new List<List<string>>();

            for (int i = 0; i < elements.Count; i++)
            {
                for (int j = i + 1; j < elements.Count; j++)
                {
                    var combination = new List<string>() { elements[i], elements[j] };
                    result.Add(combination);
                }
            }

            return result;
        }

        public static List<List<string>> GenerateAllCombinations(List<string> elements)
        {
            var result = new List<List<string>>();

            int n = elements.Count;
            int totalCombinations = (1 << n); // 2^n combinations

            for (int i = 1; i < totalCombinations; i++) // Start with с 1, to exlude empty combination
            {
                var combination = new List<string>();

                for (int j = 0; j < n; j++)
                {
                    if ((i & (1 << j)) != 0) // Check, if j element is incuded in current combination
                    {
                        combination.Add(elements[j]);
                    }
                }

                result.Add(combination);
            }

            return result;
        }
    }
}
