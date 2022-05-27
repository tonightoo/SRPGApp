using UseCase.Repositories;
using Domain.Models;

namespace UseCase.Escape
{
    public class EscapeUseCase
        : IEscapeUseCase
    {

        private readonly IArenaRepository _arenaRepository;

        public EscapeUseCase(IArenaRepository arenaRepository)
        {
            _arenaRepository = arenaRepository;
        }

        public void Handle()
        {
            Arena arena = _arenaRepository.Load();
            arena.state.Escape(arena);
        }
    }
}
