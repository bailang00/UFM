
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace resource
{
    /// <summary>
    /// 3D对象
    /// </summary>
    public class PrefabBundle : BaseBundle
    {

        protected DownloadItem _item;
        protected Dictionary<string, string> _deps;
        protected Dictionary<string, DownloadItem> _dep_items;
        public PrefabBundle(string config, bool is_fastest) : base(config, is_fastest, true) { }
        protected override IEnumerator CollectSubItems()
        {
            //_item = AddDownload(_config);
            //while (!_item.IsDone) yield return null;
            //if (_item.HasError) yield break;

            //var ta = _item.LoadObj("deps") as TextAsset;
            //if (ta != null)
            //{
            //    var text = ta.text;
            //    if (!string.IsNullOrEmpty(text))
            //    {
            //        _dep_str = text;
            //        _deps = TextParser.ParseIni(text);
            //        _dep_items = new Dictionary<string, DownloadItem>();
            //        ParseDepends();
            //    }
            //}
            yield break;
        }


        // 解析依赖项
        protected virtual void ParseDepends()
        {
            //AddDependItem(kv.Key, value);
        }
        protected override void OnLoadDone()
        {
            base.OnLoadDone();
        }
        // 创建对象
        protected override Object OnBuild()
        {
            //InitGO();
            //BuildDepends();
            return null;
        }


        // 还原依赖项
        void BuildDepends(GameObject go)
        {
            /*
            // 获取 go 上面的所有资源
            var objs = go.GetDependAssetsList();

            try
            {
                //var debug = _config == "go_4059";
                //if (debug) Log.LogError("debug config:{0}, go_name:{1}", _config, go.name);

                // 遍历依赖项
                foreach (var kv in _deps)
                {
                    // 依赖信息为 ini 结构, 其中包含了 "name.type=value" 的格式, 具体参考 打包脚本中的 BuildUtils.BuildGameObject
                    var arr = kv.Key.Split('.');
                    var name = arr[0];
                    var type = (arr.Length > 1 ? arr[1] : "tex");        // 以前的版本中, mainTexture 的格式为 "mat_name=value", 不包含 '.' 号分隔符, 因此兼容老版本
                    var value = kv.Value;
                    var asset = (_dep_items.ContainsKey(kv.Key) ? _dep_items[kv.Key].MainObj : null);

                    //if (debug) Log.LogError("set, key:{0}, value:{1}, name:{2}, type:{3}, asset:{4}", kv.Key, kv.Value, name, type, asset);

                    // 根据 type 把 依赖项设置到 obj 中
                    switch (type)
                    {
                        // Animation
                        case "clip":
                            {
                                SetClip(objs.Find<Animation>(name, false), asset as AnimationClip);
                            }
                            break;

                        // Material
                        case "shader":
                            {
                                // shader 特殊处理, 不从外部加载, 而是通过管理器
                                //(obj as Material).shader = asset as Shader;
                                var sdr = ShaderManager.Find(kv.Value);
                                if (sdr == null)
                                {
                                    Log.LogError("Cant find shader, key:{0}, sdr_name:{1}, config:{2}", kv.Key, kv.Value, _config);
                                }
                                var mat = objs.Find<Material>(name, false);
                                if (mat != null) mat.shader = sdr;
                            }
                            break;

                        case "tex":
                            {
                                var mat = objs.Find<Material>(name, false);
                                if (mat != null) mat.mainTexture = asset as Texture2D;
                            }
                            break;

                        case "tex2":
                            {
                                var tex2_name = arr[2];
                                var mat = objs.Find<Material>(name, false);
                                if (mat != null)
                                {
                                    // 如果 asset 为空, 可能是共享贴图
                                    if (asset == null)
                                    {
                                        if (value.EndsWith(".tex"))
                                        {
                                            var tex_name = value.Substring(0, value.Length - 4);
                                            asset = BundleManager.GetShareTexture(tex_name);
                                        }
                                    }

                                    //
                                    mat.SetTexture(tex2_name, asset as Texture2D);
                                }
                            }
                            break;

                        // MeshFilter
                        case "mesh":
                            {
                                var mf = objs.Find<MeshFilter>(name, false);
                                if (mf != null) mf.sharedMesh = asset as Mesh;
                            }
                            break;

                        // SkinnedMeshRenderer
                        case "smesh":
                            {
                                var smr = objs.Find<SkinnedMeshRenderer>(name, false);
                                if (smr != null) smr.sharedMesh = asset as Mesh;
                            }
                            break;

                        // AudioSource
                        case "sound":
                            {
                                SetSound(objs.Find<AudioSource>(name, false), asset as AudioClip, true);
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                var obj_str = string.Join(", ", Array.ConvertAll(objs.ToArray(), o => { return string.Format("{0}({1})", o.name, o.GetType().Name); }));
                var dep_str = (_item.LoadObj("deps") as TextAsset).text;
                Log.LogError("BuildDepends, config:{0}, error:{1}, objs:{2}, deps:{3}", _config, e.Message, obj_str, dep_str);
            }
             * */
        }
    }
}
