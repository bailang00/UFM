using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ObjHpInfo { }

class ObjData : IBinaryReader
{
    public const int HpBarIn = 1;
    public const int HpBarMid = 2;
    public const int HpBarOut = 3;

    public bool gui_object = false;

    public int aoi_id;
    public byte aoid_type;
    public int master_aoi_id;
    public int mtime;
    public Vector3 pos;
    public short direction;
    public int zhengyin;
    public byte now_state;
    public int base_id;
    public float speed;

    //public ObjHeaderData header;
    //public ObjWaysData ways;
    //public ObjAppearData appear;

    Dictionary<int, long> _buffers = new Dictionary<int, long>();

    public int phase_id;
    public int sceneaoi_id;

    //public WanfaAoiObjData wanfa_data;
    //public FightAttrib fight_attrib;
    //public ObjSkillData _cache_skill_data;
    public void ReadBinary() { }
}

public class ObjHeaderData : IBinaryReader
{
    public int red_name;        // 红名状态, 参见 Statics.RED_NAME_STATUS_NORMAL
    public int kill_num; 
    public void ReadBinary() { }
}

public abstract class ObjAppearData : IBinaryReader
{
    public byte aoi_type;
    public int aoi_id;
    public int ctime;               // 外观创建时间
    public int res_id;
    public string nick;
    public int base_id;

    public virtual void ReadBinary() { }

    public virtual void SetTempAppear() { }
    public virtual void UpdateTempAppear() { }
    public virtual void SetBaseId() { }

    public static ObjAppearData ReadAppearData() { return null; }

    public static ObjAppearData CreateAppearData(byte aoi_type) { return null; }
}

public class TrapAppearData : ObjAppearData
{ 
    
}

public class ItemAppearData : ObjAppearData { }

public class FighterAppearData : ObjAppearData { }
public class GuardNpcAppearData : FighterAppearData { }
public class MonsterAppearData : FighterAppearData { }
public class HorseAppearData : FighterAppearData { }
public class HorseSkinAppearData : FighterAppearData { }
public class DragonAppearData : FighterAppearData { }
public class SpititAppearData : FighterAppearData { }
public class NvshenAppearData : FighterAppearData { }
public class NpcAppearData : FighterAppearData { }
public class BeastAppearData : FighterAppearData { }
public class RoleAppearData : FighterAppearData { }
public class PetAppearData : FighterAppearData { }


//角色技能
public class CharacterSkill {}
public class ObjSkillData
{
    //技能id=>技能level
    public Dictionary<int, int> skills = new Dictionary<int, int>();
}
