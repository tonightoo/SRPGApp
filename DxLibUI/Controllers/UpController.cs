using UseCase.Cross.Up;

namespace DxLibUI.Controllers
{
    internal class UpController
    {
        private IUpUseCase _useCase;

        public UpController(IUpUseCase useCase)
        {
            this._useCase = useCase;
        }

        public void Push()
        {
            _useCase.Handle();
        }

    }
}
