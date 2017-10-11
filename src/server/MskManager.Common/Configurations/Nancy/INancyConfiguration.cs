namespace MskManager.Common.Configurations.Nancy
{
    public interface INancyConfiguration
    {
        string Domain { get; }
        int Port { get; }
    }
}
