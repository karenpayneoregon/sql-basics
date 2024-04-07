namespace EnumHasConversionDapper1.Classes;

public static class ExtensionMethods
{
    public static int IntValue(this Enum sender) 
        => Convert.ToInt32(sender);
}