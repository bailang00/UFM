public class BaseMain : IGamePlugin
{
    public void Start()
    {
        UnityEngine.Debug.Log("Enter BaseMain");

        BaseApp.Init();

        BaseMM.base_loader.StartBase();
    }
}