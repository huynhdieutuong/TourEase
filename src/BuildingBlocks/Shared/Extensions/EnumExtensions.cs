using System.ComponentModel;
using System.Reflection;

namespace BuildingBlocks.Shared.Extensions;
public static class EnumExtensions
{
    public static string GetEnumDescription<T>(this T enumValue) where T : Enum
    {
        FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Length > 0 ? attributes[0].Description : enumValue.ToString();
    }
}
