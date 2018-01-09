
using UnityEngine;



class ObjPartImp : ObjPart
{

    //骨骼动画
    GameObject _skel_go;
    Animation _skel_anim;
    AnimationCullingType _cull_type = AnimationCullingType.BasedOnRenderers;


    public override Animation GetAnimation()
    {
        return _skel_anim;
    }

    protected override Transform _GetBone(string name)
    {
        throw new System.NotImplementedException();
    }

    protected override void _ClearAndStop()
    {
        //删除的时候添加到缓存并初始化对象属性值
    }

    protected override void _InitAppear(int res_id, GameObject go)
    {
        //根据得到的bundle信息进行anim的属性初始化
    }

    protected override GameObject _GetAppear()
    {
        //获取外观   返回在子类中构建好外观的对象 obj
        return null;
    }

    protected override void _PlayAnim()
    {
        //根据传入的AnimateBundle得到相关动画信息
        //添加到动画状态及其在各种动画播放模式下的播放设置(如混合模式，渐入渐变，权值得变化)
        //将对象引用缓存
    }

    protected override void _StopAnim()
    {
        //具体清理动画的相关信息或者删除对应的播放片段
    }

    protected override void _StopAll()
    {
        throw new System.NotImplementedException();
    }
}
