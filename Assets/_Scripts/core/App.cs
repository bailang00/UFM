using System.Collections;
using System;
using UnityEngine;


public static partial class App
{
    internal static void Init()
    {
        //初始化程序启动的一些相关内容

        InitSelfEventHandler();
    }

    public static event Action UpdateEvent;
    public static event Action QuitEvent;

    internal static void FireUpdate()
    {
        try
        {
            if (UpdateEvent != null) UpdateEvent();
        }
        catch (Exception e)
        {

        }
    }

    internal static void FireQuit()
    {
        try
        {
            if (QuitEvent != null) QuitEvent();
        }
        catch (Exception e)
        {

        }
    }

    static void InitSelfEventHandler()
    {
        UpdateEvent += OnUpdate;
    }

    static void OnUpdate()
    {
        //do something in self Update

        Profiler.BeginSample("22222222222222");
        Task.UpdateAll((int)(Time.time * 1000));
        Profiler.EndSample();
    }

}