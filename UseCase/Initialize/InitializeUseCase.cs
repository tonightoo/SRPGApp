using UseCase.Repositories;
using Domain.Models;

namespace UseCase.Initialize
{
    public class InitializeUseCase
        : IInitializeUseCase
    {

        private IArenaRepository repository;

        private IInitializePresenter presenter;

        public InitializeUseCase(IArenaRepository repository, IInitializePresenter presenter)
        {
            this.repository = repository;
            this.presenter = presenter;
        }


        public void Handle(InitializeInputData inputData)
        {
            Arena arena = repository.InitializeById(inputData.id);

            presenter.Complete(arena);

        }
    }
}
