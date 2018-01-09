using System;
using System.Collections;

public class BaseLoaderControl
{
    public void StartBase()
    {
        Task.Run(_StartBase());
    }

    IEnumerator _StartBase()
    {

        //Init Base config

        yield return 0;


        try
        {
            var plugin_type_name = "GamePlugins.PluginMain";
            var plugin_type = Type.GetType(plugin_type_name);
            var plugin = Activator.CreateInstance(plugin_type) as IGamePlugin;
            plugin.Start();
        }
        catch
        {
            yield break;
        }

    }
}