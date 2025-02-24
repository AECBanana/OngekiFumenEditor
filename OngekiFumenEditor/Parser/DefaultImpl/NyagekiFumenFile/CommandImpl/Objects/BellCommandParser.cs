﻿using OngekiFumenEditor.Base;
using OngekiFumenEditor.Base.OngekiObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngekiFumenEditor.Parser.DefaultImpl.NyagekiFumenFile.CommandImpl.Objects
{
    [Export(typeof(INyagekiCommandParser))]
    public class BellCommandParser : INyagekiCommandParser
    {
        public string CommandName => "Bell";

        public void ParseAndApply(OngekiFumen fumen, string[] seg)
        {
            //$"Bell:{bell.ReferenceBulletPallete?.StrID}:X[{bell.XGrid.Unit},{bell.XGrid.Grid}],T[{bell.TGrid.Unit},{bell.TGrid.Grid}]"
            var bell = new Bell();
            var data = seg[1].Split(":");

            var strId = data[0].Trim();
            bell.ReferenceBulletPallete = fumen.BulletPalleteList.FirstOrDefault(x => x.StrID == strId);

            using var d = data[1].GetValuesMapWithDisposable(out var map);
            bell.TGrid = map["T"].ParseToTGrid();
            bell.XGrid = map["X"].ParseToXGrid();

            fumen.AddObject(bell);
        }
    }
}
