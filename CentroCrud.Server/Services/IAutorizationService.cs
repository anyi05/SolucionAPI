using CentroCrud.Server.Models.Custom;

namespace CentroCrud.Server.Services
{
  public interface IAutorizationService
  {
    Task<AutorizationResponse> DevolverToken(AutorizationRequest autorization);
  }
}
