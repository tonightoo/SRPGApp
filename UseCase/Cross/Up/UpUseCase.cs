using UseCase.Repositories;
using Domain.Models;

namespace UseCase.Cross.Up
{
    public class UpUseCase
        : IUpUseCase
    {

        private IArenaRepository repository;


        public UpUseCase(IArenaRepository repository)
        {
            this.repository = repository;
        }

        public void Handle()
        {
            Arena arena = repository.Load();
            arena.state.Up(arena);
        }
    }
}
