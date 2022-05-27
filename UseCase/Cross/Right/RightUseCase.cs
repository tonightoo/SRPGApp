using UseCase.Repositories;
using Domain.Models;

namespace UseCase.Cross.Right
{
    public class RightUseCase
        : IRightUseCase
    {
        private IArenaRepository _repositry;


        public RightUseCase(IArenaRepository repository)
        {
            this._repositry = repository;   
        }

        public void Handle()
        {
            Arena arena = _repositry.Load();
            arena.state.Right(arena);
       }
    }
}
