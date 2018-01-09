using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obj
{
    /// <summary>
    /// 战斗者 外观, 怪物/侠客/NPC
    /// </summary>
    class FighterAppear : ObjAppear
    {
        ObjPart _body;
        ObjPart _body_effect;
        ObjPart _weapon1;
        ObjPart _wing;
        ObjPart _partical;

        protected override void OnInit()
        {
            _body = AddPart("body");
            //_body.AttachTo(_part_root);
        }

        protected override void OnSetData()
        {
            base.OnSetData();
        }

        //播放不同技能状态或者其他状态的动画
        protected override void OnNotice()
        {
            throw new NotImplementedException();
        }

        protected override void OnUpdateAppear()
        {
            //if (_body.ResId == res_id)
            //    UpdateBodyEffect();
            //else
            //    _body.LoadAppear(res_id);

            //UpdateWeapon();
            //UpdateWing();
            //FighterAppearData ia = _appear_data as FighterAppearData;
            //if (ia != null && ia.partical_id > 0)
            //{
            //    _partical.LoadAppear(ia.partical_id);
            //}
        }


        void UpdateWing()
        {
            //var data = _appear_data as PetAppearData;
            //if (data == null || data.wing_res_id <= 0)
            //{
            //    _wing.ClearAppear();
            //    return;
            //}
            //else
            //{
            //    var res_id = data.wing_res_id;
            //    血族的翅膀需要往里面一点
            //    Vector3 wing_pos = Vector3.zero;
            //    _wing.AttachTo(_body, ObjBonesInfo.BONE_WING, wing_pos);
            //    _wing.LoadAppear(res_id);
            //}
        }

        protected override void OnAppearLoaded(ObjPart part)
        {
            base.OnAppearLoaded(part);
        }

        void UpdateBodyEffect() { }

        public override bool IsAnimLoaded()
        {
            return base.IsAnimLoaded();
        }

        public override string GetAnimFileName()
        {
            throw new NotImplementedException();
        }

        protected override void OnPlayAnim()
        {
            throw new NotImplementedException();
        }

        public override UnityEngine.Transform GetBone(string name)
        {
            return base.GetBone(name);
        }

        public override UnityEngine.Transform GetHorseBone(string name)
        {
            return base.GetHorseBone(name);
        }
    }
}
