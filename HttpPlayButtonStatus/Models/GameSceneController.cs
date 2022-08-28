using HttpSiraStatus.Interfaces;
using Zenject;

namespace HttpPlayButtonStatus.Models
{
    public class GameSceneController : IInitializable
    {
        private IStatusManager _statusManager;
        [Inject]
        public GameSceneController(IStatusManager statusManager)
        {
            this._statusManager = statusManager;
        }
        public void Initialize()
        {
            this._statusManager.OtherJSON.Remove("HttpPlayButtonStatus");
        }
    }
}
