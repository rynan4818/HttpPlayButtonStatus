using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace HttpPlayButtonStatus.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }
        // BSIPAが値の変更を検出し、自動的に設定を保存したい場合は、'virtual'でなければなりません。
        public virtual bool PlayButtonEnable { get; set; } = true;
        public virtual bool SceneChangeEnable { get; set; } = true;
        public virtual float PlayButtonDelay { get; set; } = 0f;
        public virtual string OptionSceneName1 { get; set; } = "Option Scene 1";
        public virtual string OptionSceneName2 { get; set; } = "Option Scene 2";
        public virtual string OptionSceneName3 { get; set; } = "Option Scene 3";

        /// <summary>
        /// これは、BSIPAが設定ファイルを読み込むたびに（ファイルの変更が検出されたときを含めて）呼び出されます。
        /// </summary>
        public virtual void OnReload()
        {
            // 設定ファイルを読み込んだ後の処理を行う。
        }

        /// <summary>
        /// これを呼び出すと、BSIPAに設定ファイルの更新を強制します。 これは、ファイルが変更されたことをBSIPAが検出した場合にも呼び出されます。
        /// </summary>
        public virtual void Changed()
        {
            // 設定が変更されたときに何かをします。
        }

        /// <summary>
        /// これを呼び出して、BSIPAに値を<paramref name ="other"/>からこの構成にコピーさせます。
        /// </summary>
        public virtual void CopyFrom(PluginConfig other)
        {
            // このインスタンスのメンバーは他から移入されました
        }
    }
}
