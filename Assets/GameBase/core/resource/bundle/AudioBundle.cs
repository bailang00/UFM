
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace resource
{
    public class AudioBundle : BaseBundle
    {

        DownloadItem item;


        //
        public AudioBundle(string config, bool is_fastest)
            : base(config, is_fastest, false)
        {
        }


        protected override bool CollectAllItems()
        {
            item = AddDownload(_config);
            return true;
        }

        protected override Object OnBuild()
        {
            Profiler.BeginSample("AudioBundle.OnBuild " + _config);
            var obj = new GameObject();//item.MainObj;
            Profiler.EndSample();
            return obj;
        }

        public AudioClip Clip
        {
            get
            {
                return OnBuild() as AudioClip;
            }
        }
    }
}
