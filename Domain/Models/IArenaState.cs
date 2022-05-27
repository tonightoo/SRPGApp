
namespace Domain.Models
{
    public interface IArenaState
    {

        void Up(Arena arena);

        void Down(Arena arena);

        void Left(Arena arena);

        void Right(Arena arena);

        void Enter(Arena arena);

        void Escape(Arena arena);

    }
}
