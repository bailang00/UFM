

namespace GamePlugins
{
    public class PluginMain : IGamePlugin
    {
        public void Start()
        {
            UnityEngine.Debug.Log("Enter Plugin");

            PluginApp.Init();

            PluginMM.plugin_loader.StartPlugin();
        }
    }

}