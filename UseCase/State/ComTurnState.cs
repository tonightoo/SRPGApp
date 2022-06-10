using Domain.Models;
using UseCase.Com;
using System.Threading.Tasks;
using System.Threading;


namespace UseCase.State
{
    public class ComTurnState
        : IArenaState
    {

        private IArenaState _state;

        private bool _isExecuted;

        public ComTurnState()
        {
            _state = new SelectUnitState();
            _isExecuted = false;
        }


        public void Down(Arena arena)
        {
            _state.Down(arena);
        }

        /// <summary>
        /// ターンエンド時に実行
        /// ユーザーのエンター時は何もしない
        /// </summary>
        /// <param name="arena"></param>
        public void Enter(Arena arena)
        {
            if (_isExecuted) return;

            _isExecuted = true;
            Task.Run(() => {

                IComUseCase comUseCase = new ComUseCase();
                comUseCase.ComTurn(arena);

            });

        }

        public void Escape(Arena arena)
        {

        }

        public void Left(Arena arena)
        {
            _state.Left(arena);
        }

        public void Right(Arena arena)
        {
            _state.Right(arena);
        }

        public void Up(Arena arena)
        {
            _state.Up(arena);
        }
    }
}
