using TennisTour.Application.Models;
using TennisTour.Application.Models.Tournament;

namespace TennisTour.Application.Services
{
    public interface ITournamentService
    {
        Task<UpsertTournamentResponseModel> CreateAsync(UpsertTournamentModel createTournamentModel, CancellationToken cancellationToken = default);
        Task<BaseResponseModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TournamentResponseModel>> GetAllOrderedByNameWithTournamentEditionsAsync(CancellationToken cancellationToken = default);
        Task<TournamentResponseModel> GetByIdWithTournamentEditionsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<UpsertTournamentResponseModel> UpdateAsync(Guid id, UpsertTournamentModel upsertTournamentModel);
    }
}
