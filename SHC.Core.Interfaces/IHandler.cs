
namespace SHC.Core.Interfaces
{
    public interface IHandler<TCommand, TResult>
    {
        Task<TResult> Handle(TCommand command);
    }
}
