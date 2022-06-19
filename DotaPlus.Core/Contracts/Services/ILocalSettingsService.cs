using OpenDotaApi.Enums;
using System.Threading.Tasks;

namespace DotaPlus.Contracts.Services
{
    public interface ILocalSettingsService
    {
        Task<T> ReadSettingAsync<T>(string key);

        Task SaveSettingAsync<T>(string key, T value);

        bool ContainsSetting(string key);

        string GetConstantsFile(EnumConstants constant);
    }
}
