using StrEnum;

namespace EnumHasConversionDapper.Models;

public class WineType : StringEnum<WineType>
{
    public static readonly WineType Red = Define("Red");
    public static readonly WineType White = Define("White");
    public static readonly WineType Rose = Define("Rose");
}