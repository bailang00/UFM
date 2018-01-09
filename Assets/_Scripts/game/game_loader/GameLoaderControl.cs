using System;
using System.Collections;

public partial class GameLoaderControl
{
    Task task = new Task(true);

    public void StartGame()
    {
        task.Start(_InitConfig());
    }

    IEnumerator _StartGameBase(Type type)
    {
        yield return 0;

        try
        {
            var gameBase = Activator.CreateInstance(type) as IGamePlugin;
            gameBase.Start();
        }
        catch(Exception e)
        {
            throw e;
        }
    }
}
