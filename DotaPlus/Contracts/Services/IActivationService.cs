using System.Threading.Tasks;

namespace DotaPlus.Contracts.Services
{
    public interface IActivationService
    {
        Task ActivateAsync(object activationArgs);
    }
}
