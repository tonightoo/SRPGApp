
namespace Domain.Models.Commands
{
    public interface ICommand
    {
        void Execute(Arena arena);

        void Redo(Arena arena);
    }
}
