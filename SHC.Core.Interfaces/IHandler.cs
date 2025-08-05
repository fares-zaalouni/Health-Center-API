
namespace SHC.Core.Interfaces
{
    public interface IHandler<TCommand, TResult> where TCommand : ICommand
    {
        Task<TResult> Handle(TCommand command);
    }
}
