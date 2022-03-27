﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngekiFumenEditor.Base.EditorObjects.Lane
{
    public class LaneCurvePathControlObject : OngekiMovableObjectBase
    {
        private bool isSelecting;
        public bool IsSelecting
        {
            get => isSelecting;
            set => Set(ref isSelecting, value);
        }

        private LaneCurveObject refCurveObject;
        public LaneCurveObject RefCurveObject
        {
            get => refCurveObject;
            set => Set(ref refCurveObject, value);
        }

        public override Type ModelViewType => throw new NotImplementedException();

        public override string IDShortName => "[LCO_CTRL]";
    }
}
