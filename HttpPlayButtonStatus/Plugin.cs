using HarmonyLib;
using HttpPlayButtonStatus.Installers;
using IPA;
using SiraUtil.Zenject;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;

namespace HttpPlayButtonStatus
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        private Harmony _harmony;
        public const string HARMONY_ID = "com.github.rynan4818.HttpPlayButtonStatus";
        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger, Zenjector zenjector)
        {
            Instance = this;
            Log = logger;
            Log.Debug("Logger initialized.");
            this._harmony = new Harmony(HARMONY_ID);
            zenjector.Install<PlayButtonAppInstaller>(Location.App);
            zenjector.Install<PlayButtonGameInstaller>(Location.Player);
        }

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");
        }

        [OnEnable]
        public void OnEnabled()
        {
            this._harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [OnDisable]
        public void OnDisabled()
        {
            this._harmony.UnpatchSelf();
        }
    }
}
