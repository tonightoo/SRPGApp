using UseCase.Repositories;
using Domain.Models;

namespace UseCase.Enter
{
    public class EnterUseCase
        : IEnterUseCase
    {

        private readonly IArenaRepository _arenaRepository;

        public EnterUseCase(IArenaRepository arenaRepository)
        {
            _arenaRepository = arenaRepository;
        }


        public void Handle()
        {
            Arena arena = _arenaRepository.Load();
            arena.state.Enter(arena);
       }
    }
}
