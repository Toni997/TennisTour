using TennisTour.Application.Models;
using TennisTour.Application.Models.Tournament;

namespace TennisTour.Application.Services
{
    public interface ITournamentService
    {
        Task<IEnumerable<TournamentResponseModel>> GetAllOrderedByNameAsync(CancellationToken cancellationToken = default);
        Task<TournamentWithEditionsResponseModel> GetByIdWithTournamentEditionsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<UpsertTournamentResponseModel> CreateAsync(UpsertTournamentModel upsertTournamentModel, CancellationToken cancellationToken = default);
        Task<UpsertTournamentResponseModel> UpdateAsync(Guid id, UpsertTournamentModel upsertTournamentModel);
        Task<BaseResponseModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
