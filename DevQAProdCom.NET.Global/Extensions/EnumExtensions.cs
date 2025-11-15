using System.ComponentModel;
using System.Reflection;

namespace DevQAProdCom.NET.Global.Extensions
{
    public static class EnumExtensions
    {
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            return field?.GetCustomAttribute<T>();
        }

        public static string GetDescriptionAttributeValue(this Enum value)
        {
            var descriptionAttribute = value.GetAttribute<DescriptionAttribute>();
            return descriptionAttribute?.Description ?? value.ToString();
        }
    }
}
