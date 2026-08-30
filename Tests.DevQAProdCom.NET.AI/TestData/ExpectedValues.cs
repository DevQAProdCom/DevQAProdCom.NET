namespace Tests.DevQAProdCom.NET.AI.TestData
{
    internal class ExpectedValues
    {
        internal const string ANSWER_QUESTION_WHAT_IS_MY_FAVORITE_ANIMAL = "Answer question: 'What is my favorite animal?'";
        internal const string MY_FAVORITE_ANIMAL = "MY FAVORITE ANIMAL IS";

        private static string GetMyFavoriteAnimalIs(string entity, string animal)
        {
            return $"'{entity}' {MY_FAVORITE_ANIMAL} {animal}";

        }

        public static string GetMyFavoriteAnimalIsLion(string entity)
        {
            return GetMyFavoriteAnimalIs(entity, "LION");
        }
    }
}
