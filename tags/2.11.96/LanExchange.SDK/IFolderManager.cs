namespace LanExchange.SDK
{
    public interface IFolderManager
    {
        string CurrentPath { get; }
        string ConfigFileName { get; }
        string ExeFileName { get; }
        string TabsConfigFileName { get; }
        string SystemAddonsPath { get; }
        string UserAddonsPath { get; }
        string[] GetAddonsFiles();
        string[] GetLanguagesFiles();
        string GetAddonFileName(bool isSystem, string addonName);
    }
}