using UseCase.Enter;

namespace DxLibUI.Controllers
{
    internal class EnterController
    {

        private readonly IEnterUseCase enterUseCase;

        public EnterController(IEnterUseCase enterUseCase)
        {
            this.enterUseCase = enterUseCase;
        }



        internal void Push()
        {
            enterUseCase.Handle();
        }

    }
}
