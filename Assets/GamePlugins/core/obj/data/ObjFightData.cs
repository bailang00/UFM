using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


//  战斗包
class ObjFightData : IBinaryReader
{

    public int fight_number_id;
    public int scene_ins_id;
    public float usTarget_x;
    public float usTarget_y;
    public int attacker_aoi_id;
    public int target_aoi_id;
    public int skill_id;
    public int skill_level;
    public byte attacker_is_Update_up = 0;
    public long attacker_hp3_now_hp;
    public long attacker_hp2_now_hp;
    public long attacker_hp_now_hp;
    public byte attacker_update_num;
    //public HitInfo[] attacker_change_hp_info;
    public float attacker_new_x;
    public float attacker_new_y;
    public long skill_fight_etime;
    //public ObjFightTargetInfo[] targets_infos;

    //战斗开始前赋值的字段
    //public SkillBase skillbase;
    //public ObjBehaviour attacker;
    public Vector3 attacker_begin_pos;
    public Vector3 attacker_end_pos;
    public float attacker_flay_distance;
    public Vector3 attack_forward;
    //public ObjBehaviour target;
    public Vector3 target_begin_pos;
    public Vector3 target_end_pos;
    public float target_fly_distance;
    public Vector3 target_pos;
    public long last_hit_time;
    public bool has_harms_info = false;
    //public SkillConfig skill_config;

    //战斗演示过程中赋值的字段
    //public List<SimpleEffect> effect_list = new List<SimpleEffect>();
    public int curent_hit = 0;
    public long start_time;
    public long e_time;
    public bool can_interrupt;
    public bool has_effect_jitui = false;
    public long revert_attack_speed_time = 0;
    public bool is_end = false;

    //填充战斗前数据
    public void FillPreFightData()
    { 
        //this.skillbase = DB.SkillBaseMap[this.skill_id];
        //var attacker = PluginMM.obj.GetObject(this.attacker_aoi_id, false);
        ////填充攻击者数据
        //if (attacker != null)
        //{
        //    this.attacker_begin_pos = attacker.Data.pos;
        //}
    }

    //正常战斗包读取
    public void ReadBinary()
    {

    }

    //简单战斗结果， 比如加血技能，召唤技能返回的战斗包格式
    public void ReadBinary2() { }
}

//战斗受击者
public class ObjFightTargetInfo
{
 
}

public class HitInfo
{
    public byte hp_index;				// 血条索引(1：最里面的；2：内幻兽血条；3：外幻兽血条)
    public int change_hp;			// hp3的变化值
}


//单次伤害
public class ObjHarm : IBinaryReader
{
    public int aoi_id;
    public int change_hp;
    public byte hp_index;
    public long now_hp;
    public byte action_id;
    public void ReadBinary()
    {
    }
}

public class ObjZibaoHarm : IBinaryReader
{
    public int attack_aoi_id;
    public int target_aoi_id;
    public float target_x;
    public float target_y;
    public short hit_num;
    public ObjHarm[] harms;

    public void ReadBinary() { }
}

//陷阱，旋风斩等伤害
public class ObjFightHarm : IBinaryReader 
{
    public int attacker_aoi_id;
    public byte attacker_aoi_type;
    public ushort hit_num;  //受击者个数
    public ObjHarm[] harms;
    public void ReadBinary() { }
}

public class ObjFightDelay : IBinaryReader
{
    public void ReadBinary() { }
}

public class ObjFightTaoTuo : IBinaryReader
{
    public void ReadBinary() { }
}

public class ObjFightError : IBinaryReader
{
    public void ReadBinary() { }
}

public static class FightAttriFunc
{ 
    //战斗属性   角色的，召唤兽的， 机器人的，怪物的，竞技场的
}