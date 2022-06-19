using System;

namespace DotaPlus.Contracts.Services
{
    public interface IPageService
    {
        Type GetPageType(string key);
    }
}
