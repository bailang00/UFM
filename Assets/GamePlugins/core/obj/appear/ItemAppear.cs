using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace obj
{
    class ItemAppear : ObjAppear
    {
        ObjPart _body;
        ObjPart _partical;

        protected override void OnInit()
        {
            //_body = AddPart("body");
            //_body.AttachTo(_part_root);
        }

        protected override void OnNotice()
        {
            throw new NotImplementedException();
        }

        protected override void OnUpdateAppear()
        {
            throw new NotImplementedException();
        }

        protected override void OnPlayAnim()
        {
            throw new NotImplementedException();
        }

        public override string GetAnimFileName()
        {
            throw new NotImplementedException();
        }
    }
}
