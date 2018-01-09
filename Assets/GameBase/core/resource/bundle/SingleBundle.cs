
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace resource
{
    /// <summary>
    /// 动画
    ///     config: 骨架/模型/材质
    /// </summary>
    public class SingleBundle : BaseBundle
    {
        public SingleBundle(string config, bool is_fastest)
            : base(config, is_fastest, false)
        {
        }
        
        protected override Object OnBuild()
        {
            return null;
        }

    }
}
