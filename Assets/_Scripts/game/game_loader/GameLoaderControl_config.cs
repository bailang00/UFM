using System;
using System.Collections;

partial class GameLoaderControl
{
    IEnumerator _InitConfig()
    {
        //初始化启动程序的相关配置文件的加载


        var base_type_name = "BaseMain";
        var base_type = Type.GetType(base_type_name);
        yield return Task.Call(_StartGameBase(base_type));
    }
}