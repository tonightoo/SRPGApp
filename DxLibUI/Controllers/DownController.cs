using UseCase.Cross.Down;

namespace DxLibUI.Controllers
{
    internal class DownController
    {

        private IDownUseCase _useCase;

        public DownController(IDownUseCase useCase)
        {
            _useCase = useCase;
        }

        public void Push()
        {
            _useCase.Handle();
        }

    }
}
