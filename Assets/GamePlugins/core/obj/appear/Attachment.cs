using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace obj
{
    class Attachment
    {
        //固定信息
        //static GameObjectCache _cache;
        GameObject _root;
        string _name;
        string _res_name;
        Vector3 _pos;
        float _scale;

        //可变信息
        bool _visible;
        GameObject _go;

        public Attachment(GameObject root, string name, string res_name, Vector3 pos, float scale) { }
        public static void AddToCache(string res_name, GameObject go) { }
        public void Destroy() { }

        void LoadAppear() { }
        void ClearAppear() { }
    }

    class ObjAttachment
    {
        GameObject _root;
        ArrayListT<Attachment> _list;
        bool _visible;
        float _time_update;

        static GameObject _static_root;

        //对Attachment的增删改查
    }
}
