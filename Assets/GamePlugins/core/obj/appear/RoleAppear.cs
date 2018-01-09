using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obj
{
    //主角外观
    class RoleAppear : ObjAppear
    {
        // 部位
        ObjPart _horse;
        ObjPart _body;
        ObjPart _weapon1;
        ObjPart _weapon2;
        ObjPart _wing;

        ObjPart _body_effect;
        ObjPart _horse_effect;
        ObjPart _weapon1_effect;
        ObjPart _weapon2_effect;

        ObjPart _shenbing; //神兵
        ObjPart _shenbing_effect;
        public int body_layer;

        protected override void OnInit()
        {
            //_horse = AddPart("horse");
            //_horse.AttachTo(_part_root);
        }

        protected override void OnSetData()
        {
            base.OnSetData();
        }

        protected override void OnNotice()
        {
            //播放各种人物的动作动画
        }

        protected override void OnUpdateAppear()
        {
            throw new NotImplementedException();
        }

        protected override void OnAppearLoaded(ObjPart part)
        {
            base.OnAppearLoaded(part);
            //设置跟随对象
        }

        protected override void OnPlayAnim()
        {
            //var data = _appear_data as RoleAppearData;
        }

        public override bool IsAnimLoaded()
        {
            return base.IsAnimLoaded();
        }

        public override UnityEngine.AnimationInfo GetAnimLength()
        {
            return base.GetAnimLength();
        }

        public override string GetAnimFileName()
        {
            throw new NotImplementedException();
        }
    }
}
