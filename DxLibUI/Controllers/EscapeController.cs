using UseCase.Escape;

namespace DxLibUI.Controllers
{
    internal class EscapeController
    {

        private IEscapeUseCase escapeUseCase;

        public EscapeController(IEscapeUseCase escapeUseCase)
        {
            this.escapeUseCase = escapeUseCase;
        }

        public void Push()
        {
            escapeUseCase.Handle();
        }

    }
}
