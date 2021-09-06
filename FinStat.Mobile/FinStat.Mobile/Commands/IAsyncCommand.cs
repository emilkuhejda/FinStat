using System.Threading.Tasks;
using System.Windows.Input;

namespace FinStat.Mobile.Commands
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();

        void ChangeCanExecute();
    }

    public interface IAsyncCommand<T> : ICommand
    {
        Task ExecuteAsync(T parameter);

        void ChangeCanExecute();
    }
}
