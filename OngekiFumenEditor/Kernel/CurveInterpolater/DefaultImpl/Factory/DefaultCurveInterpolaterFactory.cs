﻿using OngekiFumenEditor.Base.OngekiObjects.ConnectableObject;
using OngekiFumenEditor.Kernel.CurveInterpolater.DefaultImpl.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngekiFumenEditor.Kernel.CurveInterpolater.DefaultImpl.Factory
{
    public class DefaultCurveInterpolaterFactory : ICurveInterpolaterFactory
    {
        public static ICurveInterpolaterFactory Default { get; } = new DefaultCurveInterpolaterFactory();

        public ICurveInterpolateEnumerator CreateInterpolaterForAll(ConnectableStartObject start)
        {
            return new DefaultCurveInterpolateEnumerator(start);
        }

        public ICurveInterpolateEnumerator CreateInterpolaterForRange(ConnectableChildObjectBase start, ConnectableChildObjectBase end)
        {
            return new DefaultCurveInterpolateEnumerator(start, end);
        }
    }
}
