using Domain.Models;
using UseCase.Repositories;


namespace UseCase.UserInput
{
    public class UserInputUseCase
        : IUserInputUseCase
    {

        private IArenaRepository _arenaRepository;

        public UserInputUseCase(IArenaRepository arenaRepository)
        {
            this._arenaRepository = arenaRepository;
        }

        public void Down()
        {
            Arena arena = this._arenaRepository.Load();
            arena.state.Down(arena);
        }

        public void Enter()
        {
            Arena arena = this._arenaRepository.Load();
            arena.state.Enter(arena);
        }

        public void Escape()
        {
            Arena arena = this._arenaRepository.Load();
            arena.state.Escape(arena);
        }

        public void Left()
        {
            Arena arena = this._arenaRepository.Load();
            arena.state.Left(arena);
        }

        public void Right()
        {
            Arena arena = this._arenaRepository.Load();
            arena.state.Right(arena);
        }

        public void Up()
        {
            Arena arena = this._arenaRepository.Load();
            arena.state.Up(arena);
        }
    }
}
