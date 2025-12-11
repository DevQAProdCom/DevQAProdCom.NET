namespace DevQAProdCom.NET.Global.Extensions
{
    public static class IEnumerableExtensions
    {
        public static T[] Insert<T>(this T[] array, int index, T @object)
        {
            List<T> list = array.ToList();
            list.Insert(index, @object);
            return list.ToArray();
        }
    }
}
