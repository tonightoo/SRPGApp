using UseCase.UpdateArena;

namespace DxLibUI.Controllers
{
    internal class UpdateArenaController
    {

        private IUpdateArenaUseCase _useCase;

        public UpdateArenaController(IUpdateArenaUseCase useCase)
        {
            this._useCase = useCase;
        }


        public void UpdateArena()
        {
            this._useCase.Handle();
        }

    }
}
