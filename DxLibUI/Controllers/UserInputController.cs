using UseCase.UserInput;

namespace DxLibUI.Controllers
{
    internal class UserInputController
    {

        private IUserInputUseCase inputUseCase;

        public UserInputController(IUserInputUseCase inputUseCase)
        {
            this.inputUseCase = inputUseCase;
        }

        public void Up()
        {
            inputUseCase.Up();
        }

        public void Down()
        {
            inputUseCase.Down();
        }

        public void Left()
        {
            inputUseCase.Left();
        }

        public void Right()
        {
            inputUseCase.Right();
        }

        public void Enter()
        {
            inputUseCase.Enter();
        }
        
        public void Escape()
        {
            inputUseCase.Escape();
        }


    }
}
