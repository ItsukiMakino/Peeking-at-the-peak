using System.Threading;
using Cysharp.Threading.Tasks;

namespace MyGame.StateMachine
{
    public interface IState
    {
        UniTask Start(CancellationToken ct);
        UniTask Update(CancellationToken ct);
        UniTask Exit(CancellationToken ct);
    }
}
