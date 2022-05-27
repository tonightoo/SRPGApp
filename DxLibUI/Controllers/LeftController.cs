using UseCase.Cross.Left;

namespace DxLibUI.Controllers
{
    internal class LeftController
    {

        private ILeftUseCase _useCase;

        public LeftController(ILeftUseCase useCase)
        {
            _useCase = useCase;
        }

        public void Push()
        {
            _useCase.Handle();
        }

    }
}
