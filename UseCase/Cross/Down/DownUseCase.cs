using UseCase.Repositories;
using Domain.Models;

namespace UseCase.Cross.Down
{
    public class DownUseCase
        : IDownUseCase
    {

        private IArenaRepository _repository;

        public DownUseCase(IArenaRepository repository)
        {
            _repository = repository;
        }


        public void Handle()
        {
            Arena arena = _repository.Load();
            arena.state.Down(arena);
        }
    }
}
