using TomadaStore.Models.Models;

namespace TomadaStore.PaymentAPI.Services.Interfaces
{
    public interface IPaymentService
    {
        Task ProcessPaymentAsync(Sale sale);
    }
}
