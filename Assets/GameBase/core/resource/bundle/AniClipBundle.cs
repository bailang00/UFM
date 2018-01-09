
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace resource
{
    public class AniClipBundle : BaseBundle
    {

        public AniClipBundle(string config, bool is_fastest)
            : base(config, is_fastest, false)
        {
        }


        protected override Object OnBuild()
        {
            /*
            Profiler.BeginSample("AnimClipBundle.OnBuild " + _config);
            var ac = item.MainObj as AnimationClip;
            Profiler.EndSample();

            if (ac == null)
            {
                Log.LogError("no assetbundle, config:{0}", _config);
            }
            return ac;
             * */
            return null;
        }

        // 添加事件
        public void AddEvent(string name, float time)
        {
            /*
            if (_events == null) _events = new List<string>();
            if (!_events.Contains(name))
            {
                _events.Add(name);
                var clip = OnBuild() as AnimationClip;
                if (clip != null)
                {
                    var e = new AnimationEvent();
                    e.functionName = name;
                    e.time = time;
                    clip.AddEvent(e);
                }
            }
             * **/
        }
    }
}
