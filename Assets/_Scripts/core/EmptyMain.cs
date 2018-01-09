using UnityEngine;
using System.Collections;

abstract class EmptyMain : MonoBehaviour
{
    protected virtual void Start()
    {
        App.Init();
    }

    protected virtual void Update()
    {
        Profiler.BeginSample("sample FireUpdate");
        App.FireUpdate();
        Profiler.EndSample();
    }

    protected virtual void Quit()
    {
        App.FireQuit();
    }
}
