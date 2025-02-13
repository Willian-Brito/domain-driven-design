namespace NerdStore.Modules.Pagamentos.AntiCorruption.Config;
public interface IConfigManager
{
    string GetValue(string node);
}