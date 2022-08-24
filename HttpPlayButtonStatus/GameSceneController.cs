using HttpSiraStatus.Interfaces;
using Zenject;

namespace HttpPlayButtonStatus
{
    public class GameSceneController : IInitializable
    {
        private IStatusManager _statusManager;
        [Inject]
        public GameSceneController(IStatusManager statusManager)
        {
            _statusManager = statusManager;
        }
        public void Initialize()
        {
            this._statusManager.OtherJSON["HttpPlayButtonStatus"].Clear();
        }
    }
}
