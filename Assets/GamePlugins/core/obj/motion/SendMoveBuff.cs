using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


/// <summary>
/// 移动发包 BUFF. 避免 频繁点击/摇杆操作 时频繁发包
/// </summary>
class SendMoveBuff
{

    class PathInfo
    {
        public float time_start;
        public int move_type;
        public float[] pos_arr;
    }

    float TIME_DELAY = 0.5f;

    //
    int _aoi_id;        // 自己的 AOI_ID
    List<PathInfo> _path_list;      // 延迟发送的 路径数组
    PathInfo _path_last;        // 上次发送的路径
    Task _task;     // 定时检测

    int _ssid;
    float[] _send_time_buff = new float[256];

    // 提交发送包
    void _CommitSendPacks()
    {
        var count = _path_list.Count;
        if (count == 0) return;
        var last = _path_list[count - 1];
        var move_type = last.move_type;
        var pos_arr = last.pos_arr;
        //string log = string.Format("_CommitSendPacks, aoi_id:{0}, move_type:{1}, pos_arr:{2}", _aoi_id, move_type, StringUtils.FormatArray(pos_arr));
        //		ObjAI_Debug.AddDebug(_aoi_id, log);
        //Log.AddNetLog(log);

        //Log.LogInfo("_CommitSendPacks, aoi_id:{0}, move_type:{1}, time_now:{2}, time_start{3}, pos_arr:{4}", _aoi_id, move_type, Time.time, last.time_start, StringUtils.FormatArray(pos_arr));
        //PluginApp.network.SendBinary(OpCodes_C2S.C2M_SCENE_START_MOVE_TO_DEST, (bw) =>
        //{
        //    _ssid = (_ssid + 1) % 256;
        //    _send_time_buff[_ssid] = Time.realtimeSinceStartup;

        //    //
        //    bw.WriteUInt32((UInt32)PluginMM.scene.model.scene_ins_id);
        //    bw.WriteUInt32((UInt32)_aoi_id);
        //    //bw.Write((byte)_ssid);                  // TODO: 网络延迟
        //    bw.WriteByte((byte)move_type);
        //    bw.WritePath(pos_arr);
        //});

        _path_list.Clear();
        _path_last = last;
    }
}