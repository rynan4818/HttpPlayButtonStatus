using HttpPlayButtonStatus.Installers;
using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;

namespace HttpPlayButtonStatus
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        private Harmony _harmony;
        public const string HARMONY_ID = "com.github.rynan4818.HttpPlayButtonStatus";
        [Init]
        /// <summary>
        /// IPAによってプラグインが最初にロードされたときに呼び出されます。
        /// （ゲームが開始されたとき、またはプラグインが無効の状態で開始された場合は有効化されたときのいずれか）
        /// [Init]はコンストラクタのメソッド、InitWithConfig のような通常のメソッドの前に呼び出されます。
        /// [Init]は１つのコンストラクタのみを使用して下さい。
        /// </summary>
        public void Init(IPALogger logger, Config conf, Zenjector zenjector)
        {
            Instance = this;
            Log = logger;
            Log.Debug("Initialized.");
            this._harmony = new Harmony(HARMONY_ID);
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            zenjector.Install<PlayButtonAppInstaller>(Location.App);
            zenjector.Install<PlayButtonMenuInstaller>(Location.Menu);
            zenjector.Install<PlayButtonGameInstaller>(Location.Player);
        }

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            this._harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            this._harmony.UnpatchSelf();
            Log.Debug("OnApplicationQuit");
        }
    }
}
