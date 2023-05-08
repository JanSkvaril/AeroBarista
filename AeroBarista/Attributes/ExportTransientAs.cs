namespace AeroBarista.Attributes;

public class ExportTransientAs : ExportTransient
{
    public string InterfaceName { get; }

    public ExportTransientAs(string interfaceName)
    {
        InterfaceName = interfaceName;
    }
}
