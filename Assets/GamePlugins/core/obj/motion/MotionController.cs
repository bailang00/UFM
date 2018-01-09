using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class MotionController
{
    public const int CHONGCI_MOVETYPE = 5;
    public const float MOVE_ERROR = 10;

    int _aoi_id;
    bool _is_client;

    float[] _pos_arr;
    int[] _type_arr;

    int _pos_idx_prev;
    int _pos_idx;
    int _move_type;
    float _cur_dist;
    float _cur_moved;
    float _cur_x, _cur_y;
    float _cur_deg;

    float _speed_rate;
    float _speed;
    long _su_expire;
    float _su_speed;
    float _su_stop_x, _su_stop_y;


    // 设置速度信息
    public void ChangeSpeed(ObjSpeedData pi, bool need_speed)
    {
        this._speed = pi.speed;
        this._su_expire = pi.su_expire;
        this._su_speed = pi.su_speed;
        this._su_stop_x = pi.su_stop_x;
        this._su_stop_y = pi.su_stop_y;

        if (pi.speed <= 0 && need_speed)
        {
            //Log.LogError("ChangeSpeed, aoi_id:{0}, speed:{1}", _aoi_id, pi.speed);
        }
    }



    SendMoveBuff _send_buff;

    // 开始移动
    public void StartMoveByClient(int move_type, float[] pos_arr, float speed)
    {
        //PathUtils.Assert(_is_client, "is client");
        //PathUtils.Assert(pos_arr.Length >= 4, "pos_arr error");
        //PathUtils.Assert(speed > 0, "speed zero");
        ////		ObjAI_Debug.AddDebug(_aoi_id, string.Format("StartMoveByClient speed : {0}", speed));
        //// 发移动包
        //if (_aoi_id > 0 && !PathUtils.dont_send_packet)
        //{
        //    float time_delay = 0.5f;
        //    if (PluginMM.scene.model.scene_base.safe_mode == entity.SceneDefs.SCENE_SAFE_MODE_PVP)
        //    {
        //        time_delay = 0.2f;
        //    }
        //    if (_send_buff == null) _send_buff = new SendMoveBuff(_aoi_id, time_delay);
        //    _send_buff.SendStartMove(move_type, pos_arr);
        //}

        //// 开始移动
        //_speed = speed;
        //_start(move_type, pos_arr);
    }


    // 恢复移动, 当对象进入视野, 并且它已经处于移动状态时, 需要恢复它的当前移动
    public void ResumeMove(ObjWaysData data)
    {
        //PathUtils.Assert(this._move_type == 0, "move_type");
        //PathUtils.Assert(!this._is_client, "is_client");

        //// 保存速度
        //ChangeSpeed(data.speed, true);

        //// 获取路径信息
        //var type_arr = data.type_arr;
        //var pos_arr = data.pos_arr;

        //var pos_idx = data.pos_idx - 1; // 1-based => 0-based
        //if (pos_idx != 0)
        //{
        //    var pos_num = type_arr.Length;
        //    PathUtils.Assert(pos_idx < pos_num - 1, "resume move, is stop!");
        //    var skip = pos_idx;
        //    type_arr = ArrayUtils.SubArray(type_arr, skip, type_arr.Length - skip);
        //    skip = pos_idx * 2;
        //    pos_arr = ArrayUtils.SubArray(pos_arr, skip, pos_arr.Length - skip);
        //    pos_idx = 0;
        //}

        //// 路径信息
        //_pos_arr = pos_arr;
        //_type_arr = type_arr;
        //// 进度
        //_pos_idx_prev = -1;
        //_pos_idx = pos_idx;
        //_move_type = type_arr[pos_idx];
        //_cur_dist = PathUtils.GetDistance(pos_arr, pos_idx);
        //_cur_moved = MathUtils.Distance(_cur_x, _cur_y, pos_arr[pos_idx * 2], pos_arr[pos_idx * 2 + 1]);
    }


    // 停止移动
    public void StopMoveByClient()
    {
        //PathUtils.Assert(this._is_client, "is_client");
        //if (_move_type == 0) return;

        // 开始移动
        //var pos_arr = new float[] { _cur_x, _cur_y, _cur_x, _cur_y };
        //this.StartMoveByClient(_move_type, pos_arr, _speed);

        // 立刻发送网络包
        //if (_send_buff != null)
        //{
        //    _send_buff.Flush();
        //}
    }

    // 更新帧, 返回是否移动中
    public void Update()
    {
        //if (_move_type == 0) return;
        //// 获取当前速度
        //var speed = _speed; // 米/毫秒
        //if (_time_now < this._su_expire)    // 加速期间
        //{
        //    speed = this._su_speed;
        //    if (speed == 0)
        //    {
        //        if (_su_stop_x != this._cur_x || _su_stop_y != this._cur_y)
        //        {
        //            if (Vector2.Distance(new Vector2(_cur_x, _cur_y), new Vector2(_su_stop_x, _su_stop_y)) < MOVE_ERROR)
        //            {
        //                _su_stop_x = this._cur_x;
        //                _su_stop_y = this._cur_y;
        //            }
        //            else
        //            {
        //                this._cur_x = _su_stop_x;
        //                this._cur_y = _su_stop_y;
        //            }
        //        }
        //        return;
        //    }
        //}
        //speed *= _speed_rate;   // 速度加成

        //// 移动 _time_pass 个时间量
        //var time_value = _time_pass;
        //PathUtils.MoveByTime(time_value, speed, _pos_arr, _type_arr, ref _pos_idx, ref _cur_moved, ref _cur_dist);
        //// 结束判断
        //if (_pos_idx >= _type_arr.Length - 1)
        //{
        //    _move_type = 0;
        //}
        //else
        //{
        //    _move_type = _type_arr[_pos_idx];
        //}

        //// 计算坐标
        //PathUtils.GetPosNow(_pos_arr, _pos_idx, _cur_moved, _cur_dist, out _cur_x, out _cur_y);

        //// 方向
        //if (_pos_idx_prev != _pos_idx)
        //{
        //    _pos_idx_prev = _pos_idx;
        //    PathUtils.GetDegree(_pos_arr, _pos_idx, ref _cur_deg);
        //}
    }

    /// <summary>
    /// 获取未来某个时刻的坐标近似值
    ///     返回坐标, 是根据当前基础速度 BaseSpeed 来计算的, 因此是近似值
    /// </summary>
    //public void GetFuturePosition(long time_offset, out float x, out float y)
    //{
        // 移动中
        //if (_move_type != 0)
        //{
        //    var time_value = time_offset;
        //    var pos_idx = _pos_idx;
        //    var cur_moved = _cur_moved;
        //    var cur_dist = _cur_dist;

        //    PathUtils.MoveByTime(time_value, _speed, _pos_arr, _type_arr, ref pos_idx, ref cur_moved, ref cur_dist);
        //    PathUtils.GetPosNow(_pos_arr, pos_idx, cur_moved, cur_dist, out x, out y);
        //}
        //else
        //{
        //    x = _cur_x;
        //    y = _cur_y;
        //}
    //}
}