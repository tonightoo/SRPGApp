using Domain.Models;

namespace UseCase.Repositories
{
    public interface IArenaRepository
    {

        void Save(Arena arena);

        Arena InitializeById(int id);

        Arena Load();

    }
}
