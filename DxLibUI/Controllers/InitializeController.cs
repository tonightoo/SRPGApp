using UseCase.Initialize;

namespace DxLibUI.Controllers
{
    internal class InitializeController
    {

        private IInitializeUseCase InitializeUseCase;

        public InitializeController(IInitializeUseCase initializeUseCase)
        {
            this.InitializeUseCase = initializeUseCase;
        }

        public void Initialize(int id)
        {
            InitializeInputData inputData = new InitializeInputData(id);
            InitializeUseCase.Handle(inputData);
        }

    }
}
