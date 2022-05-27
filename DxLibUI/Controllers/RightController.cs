using UseCase.Cross.Right;

namespace DxLibUI.Controllers
{
    internal class RightController
    {

        private IRightUseCase _useCase;

        public RightController(IRightUseCase useCase)
        {
            _useCase = useCase;
        }

        public void Push()
        {
            _useCase.Handle();

        }

    }
}
