using UseCase.Repositories;
using Domain.Models;

namespace UseCase.Cross.Left
{
    public class LeftUseCase
        : ILeftUseCase
    {
        private IArenaRepository _repository;

        public LeftUseCase(IArenaRepository repository)
        {
            _repository = repository;
        }

        public void Handle()
        {
            Arena arena = _repository.Load();
            arena.state.Left(arena);
      }
    }
}
