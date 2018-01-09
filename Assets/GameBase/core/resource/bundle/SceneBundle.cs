
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace resource
{
    /// <summary>
    /// 场景
    /// </summary>
    public partial class SceneBundle : BaseBundle
    {
        public SceneBundle(string config, bool is_fastest)
            : base(config, is_fastest, false)
        {
        }
        
        protected override Object OnBuild()
        {
            //将场景中所有对象生成一个场景对象返回
            return null;
        }

        void Visible()
        {
            //todo
            /***
             * 初始化光照贴图
             * 小地图
             * 动态合批
             * 渲染环境设置
             * 灯光设置
             * 照相机设置
             * */

        }

    }
}
