using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//对象的移动路径信息
class ObjWaysData : IBinaryReader
{
    public int pos_idx;
    public float[] pos_arr;
    public int[] trype_arr;

    //public ObjSpeedData speed;

    public void ReadBinary()
    { }
}

public class ObjSpeedData : IBinaryReader
{
    public float speed;
    public long su_expire;
    public float su_speed;      //米/毫秒
    public float su_stop_x;     //如果su_speed = 0, 则表示停止精确的坐标
    public float su_stop_y;


    public void ReadBinary() { }
}

// 对象移动信息
public class ObjMoveData : IBinaryReader
{
    public int scene_ins_id;
    public int aoi_id;
    public byte ssid;       // 序列号

    // move
    public int move_type;
    public float[] pos_arr;

    public void ReadBinary() { }

}


/// <summary>
/// 对象停止
/// </summary>
public class ObjStopData : IBinaryReader
{
    public int scene_ins_id;
    public int aoi_id;
    public float x;
    public float y;

    public void ReadBinary() { }
}
