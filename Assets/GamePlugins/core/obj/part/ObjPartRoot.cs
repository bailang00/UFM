
using UnityEngine;
using System;

class ObjPartRoot : ObjPart
{
    public event Action<ObjPart> AppearLoadedEvent;
    GameObject _go;

    public ObjPartRoot(GameObject go)
    {
        _go = go;
        base.SetAsRoot();
    }

    protected override void OnAppearLoaded(ObjPart target)
    {
        if(AppearLoadedEvent != null)
        {
            AppearLoadedEvent(target);
        }
    }

    protected override void _InitAppear(int res_id, GameObject go)
    {
        throw new NotImplementedException();
    }

    protected override void _ClearAndStop()
    {
        throw new NotImplementedException();
    }

    protected override Transform _GetBone(string name)
    {
        return _go.transform;
    }

    protected override GameObject _GetAppear()
    {
        return _go;
    }

    protected override void _PlayAnim()
    {
        throw new NotImplementedException();
    }

    protected override void _StopAnim()
    {
        throw new NotImplementedException();
    }

    protected override void _StopAll()
    {
        throw new NotImplementedException();
    }
}