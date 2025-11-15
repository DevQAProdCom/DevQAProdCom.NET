namespace DevQAProdCom.NET.Global.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<IEnumerable<T>> CrossJoin<T>(this IEnumerable<IEnumerable<T>> lists)
        {
            if (lists.Count() == 0)
            {
                yield return Enumerable.Empty<T>();
            }
            else
            {
                var head = lists.First();
                var tail = lists.Skip(1);
                foreach (var item in head)
                {
                    foreach (var result in CrossJoin(tail))
                    {
                        yield return new[] { item }.Concat(result);
                    }
                }
            }
        }
    }
}
