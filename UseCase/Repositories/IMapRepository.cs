using Domain.Models;

namespace UseCase.Repositories
{
    public interface IMapRepository
    {

        Map FindById(int mapId);

    }
}
