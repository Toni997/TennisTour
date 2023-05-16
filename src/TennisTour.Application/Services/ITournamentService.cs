using TennisTour.Application.Models;
using TennisTour.Application.Models.Tournament;

namespace TennisTour.Application.Services
{
    public interface ITournamentService
    {
        Task<BaseResponseModel> DeleteAsync(Guid id);
        Task<IEnumerable<TournamentResponseModel>> GetAllOrderedByNameWithTournamentEditionsAsync();
        Task<TournamentResponseModel> GetByIdWithTournamentEditionsAsync(Guid id);
    }
}
