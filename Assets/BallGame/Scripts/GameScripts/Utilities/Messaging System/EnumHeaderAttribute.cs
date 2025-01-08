using System;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class EnumHeaderAttribute : Attribute
{
    public string Header { get; }

    public EnumHeaderAttribute(string header)
    {
        Header = header;
    }
}
