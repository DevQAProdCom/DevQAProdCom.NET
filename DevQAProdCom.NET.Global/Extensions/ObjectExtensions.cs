using System.Reflection;
using System.Text.Json;

namespace DevQAProdCom.NET.Global.Extensions
{
    public static class ObjectExtensions
    {
        public static T CloneJson<T>(this object @object) where T : class
        {
            if (@object == null)
                return null;

            try
            {
                var sourceObject = JsonSerializer.Serialize(@object);
                T destinationObject = JsonSerializer.Deserialize<T>(sourceObject);

                return destinationObject;
            }
            catch
            {
                return null;
            }
        }

        public static T GetFieldValue<T>(this object obj, string name)
        {
            // Set the flags so that private and public fields from instances will be found
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var field = obj.GetType().GetField(name, bindingFlags);
            return (T)field?.GetValue(obj);
        }

        public static T AsOrException<T>(this object? @object) where T : class
        {
            if (@object == null)
                throw new ArgumentNullException($"'Null' object was provided for cat operation to type '{typeof(T).FullName}'.");

            T result = @object as T;

            if (result != null)
                return result;

            throw new InvalidCastException($"Unable to cast object of type '{@object.GetType().FullName}' to type '{typeof(T).FullName}'.");
        }

        public static string ToJson(this object @object, JsonSerializerOptions? options = null)
        {
            if (@object == null)
                return string.Empty;

            return JsonSerializer.Serialize(@object, options ?? new JsonSerializerOptions());
        }
    }
}
