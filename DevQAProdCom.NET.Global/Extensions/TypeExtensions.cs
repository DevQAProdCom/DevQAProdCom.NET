namespace DevQAProdCom.NET.Global.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsAssignableToType(this Type from, Type to)
        {
            if (!to.IsGenericType || !from.IsGenericType)
                return false;

            Type toGenericType = to.GetGenericTypeDefinition();
            Type fromGenericType = from.GetGenericTypeDefinition();

            if (toGenericType != fromGenericType)
                return false;

            Type[] toArgs = to.GetGenericArguments();
            Type[] fromArgs = from.GetGenericArguments();

            for (int i = 0; i < toArgs.Length; i++)
            {
                if (!toArgs[i].IsAssignableFrom(fromArgs[i]))
                    return false;
            }

            return true;
        }
    }
}
