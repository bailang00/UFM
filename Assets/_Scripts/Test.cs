using UnityEngine;
using System.Collections;
using System;

public class Test : MonoBehaviour
{
    string url = "http://pl-aezj3dcn.game.kunlun.com/servinfo.php?os_name=android&ly_name=kl&version=1.81.18.050500&kl_unition_id=&dev_loc_id=SG,%20ip:";

    int posx, posy, w, h;
    void Start()
    {
        StartCoroutine(Download());
    }

    void OnGUI()
    {
        if(GUILayout.Button("Next"))
        {
            Application.LoadLevel(1);
        }
        GUILayout.TextArea("text");
        GUI.Label(new Rect(100,100,100,100),"123");
    }




    IEnumerator Download()
    {
        WWW www = new WWW(url);
        yield return www;
        string str = www.text;
        www.Dispose();

        var arr = MiniJSON.JsonDecode(str) as ArrayList;
        foreach (Hashtable ht in arr)
        {
            Ent et = new Ent();
            FromHashtable(ht, et);
            Debug.Log(et);
        }
    }
    
    public static void FromHashtable(Hashtable src, object dst)
    {
        var type = dst.GetType();
        foreach (DictionaryEntry e in src)
        {
            var fi = type.GetField(e.Key.ToString());
            if (fi == null) continue;

            var ftype = fi.FieldType;
            if (ftype.IsPrimitive || ftype == typeof(string))
            {
                var value = Convert.ChangeType(e.Value, fi.FieldType);
                fi.SetValue(dst, value);
            }
        }
    }

    class Ent
    {
        public string name;
        public string res_url;
        public string servlist_url;
        public string servlist_ip;
        public string report_url;
        public string oldest_version;
        public string default_version;

        public override string ToString()
        {
            return string.Format(
                "name = {0}\n"+
                "res_url = {1}\n"+
                "servlist_url = {2}\n"+
                "servlist_ip = {3}\n"+
                "report_url = {4}\n"+
                "oldest = {5}\n"+
                "default = {6}", name, res_url, servlist_url, servlist_ip, report_url, oldest_version, default_version);
        }
    }
    
}

