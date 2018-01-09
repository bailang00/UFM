using System.Collections;

class Init : EmptyMain
{
    protected override void Start()
    {
        base.Start();

        MM.gameloader.StartGame();
    }
}
