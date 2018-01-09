
using UnityEngine;

abstract class ObjPart
{
    protected abstract void _ClearAndStop();
    protected abstract void _InitAppear(int res_id, GameObject go);
    protected abstract GameObject _GetAppear();
    protected abstract Transform _GetBone(string name);

    protected abstract void _PlayAnim();
    protected abstract void _StopAnim();
    protected abstract void _StopAll();

    public virtual void SetAniCullingType(AnimationCullingType type) { }
    public virtual Animation GetAnimation() { return null; }


    //protected static GameObjectCache _part_cache;
    //各个对象部件的操作，增加，查找等。都保存在ObjPart对象下

    #region 顶点动画/骨骼动画
    #endregion

    #region 外观

    /***
     * 根据res_id加载对象
     * 先查找缓存对象GameObjectCache中是否有对其缓存
     * 有则：设置外观
     * 否则：根据res_id查找对应的BaseBundle信息
     *      没找到，直接返回 表明没有相应地模型资源
     *      找到：下载该bundle，在下载完成后，bundle自己构建对象到内存， 并设置外观
     *      
     * 设置外观：
     *      设置层和碰撞体，材质，及其子类的外观设置
     *      
     * */

    protected virtual void OnAppearLoaded(ObjPart target) { }

    public const string PartPrefix = "Part";
    int _res_id;
    int _want_res_id;

    public void LoadAppear(int res_id)
    {
        if (res_id == 0)
        {
            //ClearAppear();
            return;
        }

        if (_res_id == res_id) return;
        if (_want_res_id == res_id) return;

        // 从cache中获取res_id
        _want_res_id = res_id;
        //var go = GetFormCache(res_id);
        //if(go != null)
        //{
        //    OnLoadAppear2(res_id, go);
        //    return;
        //}

        // 重新加载
        var _is_fastest = false;// model_quality == ObjModelQuality.High;
        //var b = ObjUtils.GetAnimBundle(res_id, _is_fastest);
        //if (b == null) return;//没找到对应的资源id

        //if (FileManager.DumpUnDone && !b.IsDone) Log.LogError("LoadAppear, 未完成, res_id:{0}!", res_id);

        // 等待加载
        var arr = new object[] { res_id };
        //DownloadHelper.WaitBundle(b, OnLoadAppear, arr);
    }

    void OnloadAppear(/*BaseBundle b, */object param)
    {
        var arr = param as object[];
        var res_id = (int)arr[0];

        // 避免多余
        if (_res_id == res_id) return;   // 已经加载
        if (_want_res_id != res_id) return;  // 不是期望的资源

        // 创建 go 并初始化
        /*var go = b.Build() as GameObject;
        if(go)
        {
            go.name = GetPartName(res_id);
        }
        else
        {
            Log.LogError("ObjPart.OnLoadAppear, build failed! part_name:{0}, res_id:{1}", Name, res_id);
#if UNITY_EDITOR
            go = new GameObject("EMPTY_GO " + Name + " " + res_id);
#endif
        }*/

        // 设置外观
        OnLoadAppear2(res_id, null);//go);
    }

    void OnLoadAppear2(int res_id, GameObject go)
    {
        // 清空外观
        ClearAppear();

        // 保存资源ID
        _res_id = res_id;
        _want_res_id = 0;

        if(go)
        {
            go.SetActive(true);

            // 设置层次/碰撞体
            //SetLayerAndCollider(go);

            // 设置材质
            //var mat_qualit = (model_quality == ObjModelQuality.High ? MaterialQuality.High : MaterialQuality.Low);
            //AnimatBundle.SetMaterial(go, mat_qualit);

            _InitAppear(res_id, go);//子类

            // 恢复挂接, 恢复至部位的显示
            //_node.LocalVisible = true;

            // 如果不可见, 立即隐藏 
            //if (!_node.Visible)
            //{
            //    BreakAppear();
            //}

            // 通知
            //(_node.RootOwner as ObjPart).OnAppearLoaded(this);
        }
    }


    // 清空外观, 停止动画
    public void ClearAppear()
    {
        // 无外观, 忽略
        if (_res_id == 0)
        {
            _want_res_id = 0;
            return;
        }

        // 取消挂接, 使得子部位被隐藏
        //_node.LocalVisible = false;

        // 清空外观
        _res_id = 0;
        _want_res_id = 0;

        //ClearFace();

        // 子类清空
        _ClearAndStop();
    }

    #endregion


    #region 动画播放

    /// <summary>
    /// 对Anim已知信息预处理，例如，是否为循环播放
    /// 通过BundlManger获取对应动画bundle， 
    ///     动画bundle是加载完成的，直接播放动画
    ///     没有加载完，等待加载   DownloadHelper.WaitBunle(bundle, OnLoadAni, info); 设置加载完的回调函数
    /// </summary>
    public void PlayAnim()
    {
        //DownloadHelper.WaitBunle(bundle, OnLoadAni, info);
    }

    /// <summary>
    /// 加载完毕  
    /// 预处理，并播放
    /// </summary>
    void OnLoadAnim()
    {
        PlayAnim(new object());
    }

    void PlayAnim(object param)
    {
        _PlayAnim();//子类播放
    }

    public void StopAnim()
    {
        _StopAnim();
    }

    public void StopAll()
    {
        _StopAll();
    }


    #endregion

    #region 节点挂接

    /*
     *  处理obj中各个部件的挂载，包括特效，武器
     *  也可以删除部分挂载点
     * */

    // 设置为根节点
    protected void SetAsRoot()
    {
    }
    void JoinAppear() { }
    void BreakAppear() { }

    #endregion

    #region ITreeNodeOwner 接口

    public void OnBeRemoved() { }
    public void OnBeAdded() { }
    public void OnAddedChild() { }
    public void OnRemovedChild() { }
    public void OnShow() { JoinAppear(); }
    public void OnHide() { BreakAppear(); }
    #endregion


    #region 层体/碰撞体
    #endregion

    #region 表情支持

    int _face_id = -1;
    Material _face_mat;
    Texture _face_tex_bak;

    public void SetFace(int face_id)
    {
        if (_face_id == face_id) return;

        // 取消设置
        ClearFace();

        //FaceConfig

        //设置表情

    }

    void ClearFace()
    {
        if (_face_id >= 0)
        {
            _face_id = -1;
            if (_face_mat != null)
            {
                _face_mat.mainTexture = _face_tex_bak;
                _face_mat = null;
            }
            _face_tex_bak = null;
        }
    }

    #endregion

    #region 输出

    #endregion
}












