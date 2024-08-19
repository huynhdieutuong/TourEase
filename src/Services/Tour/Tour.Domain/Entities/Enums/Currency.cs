using System.ComponentModel;

namespace Tour.Domain.Entities.Enums;
public enum Currency
{
    [Description("$")]
    USD,

    [Description("đ")]
    VND
}
