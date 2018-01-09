using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace obj
{
    /// <summary>
    /// 对象外观基类
    ///     . 外观隐藏后, 会调用 ClearAppear 清空外观, 节省内存开销; 外观显示后, 自动恢复外观
    ///     . 外观隐藏, 不影响动画播放
    /// </summary>
    abstract class ObjAppear
    {
        GameObject _root;
        bool _visible;

        //protected ObjData _obj_data;
        //protected ObjAppearData _appear_data;
        //protected ObjBehaviour _master;s
        public int appear_type = 0;

        protected abstract void OnInit();
        protected abstract void OnNotice();
        protected virtual void OnSetData() { }

        public void Init(GameObject go, bool visible)
        {
            _root = go;
            _visible = visible;
            InitAppear(go);
            OnInit();
        }
        public void SetData()
        {
            OnSetData();
            if (_visible) UpdateAppear();
        }
        
        public void PlayCustomAnim() { this.Notice(); }
        public void StopCustomAnim() { this.Notice(); }
        public void Destroy(bool destroyRoot){  }
        public void Notice() { }//各种子类之间传递接收消息


        //模型放大  用协程来放大

        #region 临时外观

        /**
         *   用一个节点来缓存临时用到的外观对象
         *          . 添加
         *          . 删除
         ****/

        #endregion

        #region 外观管理

        protected ObjPartRoot _part_root;
        List<ObjPart> _part_list;

        #endregion

        protected abstract void OnUpdateAppear();
        protected virtual void OnAppearLoaded(ObjPart part) { }

        // 初始化
        void InitAppear(GameObject go)
        {
            _part_root = new ObjPartRoot(go);
            _part_root.AppearLoadedEvent += OnAppearLoaded;

            _part_list = new List<ObjPart>();
            _part_list.Add(_part_root);
        }

        protected void UpdateAppear()
        { 
            //子类更新
            OnUpdateAppear();
            //刷新动画
        }

        // 添加部位
        protected ObjPart AddPart(string name)
        {
            return null;
        }

        void ClearAppear()
        {
            foreach (var part in _part_list)
            {
                part.ClearAppear();
            }
        }

        #region  动画

        protected abstract void OnPlayAnim();//子类动画播放
        public abstract string GetAnimFileName();
        public virtual bool IsAnimLoaded() { return true; }
        public virtual AnimationInfo GetAnimLength() { return new AnimationInfo(); }
        protected void PlayAnim() { /*动画相关设置*/ OnPlayAnim(); }
        protected void StopAnim(ObjPart part) { part.StopAnim(); }
        protected void StopAll() { }

        #endregion

        public virtual Transform GetBone(string name) { return null; }
        public virtual Transform GetHorseBone(string name) { return null; }
    }
}
