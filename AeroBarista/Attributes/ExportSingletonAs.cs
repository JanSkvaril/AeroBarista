namespace AeroBarista.Attributes;

public class ExportSingletonAs : ExportSingleton
{
    public string InterfaceName { get; }

    public ExportSingletonAs(string interfaceName)
	{
        InterfaceName = interfaceName;
    }
}
