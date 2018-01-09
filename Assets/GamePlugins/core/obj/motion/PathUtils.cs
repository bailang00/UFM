using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using TimeType = System.Int64;     // 时间戳 类型
using PosType = System.Single;       // 坐标 类型

class PathUtils
{
    public static void GetDegree(PosType[] pos_arr, int pos_idx, ref float degree)
    {
        var j = pos_idx * 2;
        if ((j + 3) < pos_arr.Length)
        {
            var dx = pos_arr[j + 2] - pos_arr[j + 0];
            var dy = pos_arr[j + 3] - pos_arr[j + 1];
            if (dx != 0 || dy != 0)
            {
                //degree = MathUtils.XZ2Degree(dx, dy);
            }
        }
    }

    // 获取长度
    public static float GetDistance(PosType[] pos_arr, int pos_idx)
    {
        var j = pos_idx * 2;
        var dx = pos_arr[j + 2] - pos_arr[j + 0];
        var dy = pos_arr[j + 3] - pos_arr[j + 1];
        return Mathf.Sqrt(dx * dx + dy * dy);
    }

    // 拟合路径
    public static void MergePath(float curx, float cury, int move_type, ref float[] pos_arr, ref int[] type_arr)
    { 
    }


    
    // 根据时间获取位置
    public static void MoveByTime(float time_value, float speed, float[] pos_arr, int[] type_arr, ref int pos_idx, ref float cur_moved, ref float cur_dist)
    { 
    }
}
