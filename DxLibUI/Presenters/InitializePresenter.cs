using UseCase.Initialize;
using Domain.Models;
using UseCase.UpdateArena;

namespace DxLibUI.Presenters
{
    public class InitializePresenter
        : IInitializePresenter
    {

        private IUpdateArenaPresenter _presenter;

        public InitializePresenter(IUpdateArenaPresenter presenter)
        {
            _presenter = presenter;
        }

        public void Complete(Arena arena)
        {
            _presenter.Complete(arena);
        }
    }
}
