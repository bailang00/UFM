using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace resource
{
    /// <summary>
    /// 资源包基类
    /// </summary>
    public abstract class BaseBundle : IProgress
    {
        public bool NeedInitGo = true;
        public int state;

        protected string _config;
        protected bool _is_fastest;
        protected bool _always_build;
        bool _split_done;

        //List<DownloadItem> _items;
        Task _collect_sub_items_task;
        int _is_done;

        static int _somooth_last_done_fc;
        static List<BaseBundle> s_loading_bundles = new List<BaseBundle>();

        public BaseBundle(string config, bool is_fast, bool is_split)
        {
            // 如果无法收集所有 item
            if (!CollectAllItems())
            {
                // 启用携程下载
                _collect_sub_items_task = new Task(true);
                _collect_sub_items_task.Start(CollectSubItems());
            }
        }

        protected virtual bool CollectAllItems() { return false; }
        protected virtual IEnumerator CollectSubItems() { yield break; }
        protected virtual void OnLoadDone() { }
        protected virtual void OnSpeedUp() { }
        public virtual float GetLoaded() { return 0.0f; }
        public virtual float GetTotal() { return 0.0f; }

        protected abstract UnityEngine.Object OnBuild();


        protected DownloadItem AddDownload(string name)
        {
            /*
            var item = DownloadManager.GetDownload(name, _is_fastest);
            _items.Add(item);
            return item;
            **/
            return null;
        }

        public UnityEngine.Object Build()
        {
            Profiler.BeginSample("BaseBundle.Build");
            var obj = OnBuild();
            Profiler.EndSample();
            return obj;
        }

         // 初始化 GO
        protected void InitGO(GameObject go, bool remove_camera) { }

    }
}
