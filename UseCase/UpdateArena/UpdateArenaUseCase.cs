using UseCase.Repositories;
using Domain.Models;

namespace UseCase.UpdateArena
{
    public class UpdateArenaUseCase
        : IUpdateArenaUseCase
    {

        private IArenaRepository repository;

        private IUpdateArenaPresenter presenter;

        public UpdateArenaUseCase(IArenaRepository repository, IUpdateArenaPresenter presenter)
        {
            this.repository = repository;
            this.presenter = presenter;
        }

        public void Handle()
        {
            Arena arena = repository.Load();
            presenter.Complete(arena);
        }
    }
}
