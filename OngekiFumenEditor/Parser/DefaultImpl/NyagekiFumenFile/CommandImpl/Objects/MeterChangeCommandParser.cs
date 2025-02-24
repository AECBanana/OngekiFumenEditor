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
    public class MeterChangeCommandParser : INyagekiCommandParser
    {
        public string CommandName => "MeterChange";

        public void ParseAndApply(OngekiFumen fumen, string[] seg)
        {
            //$"MeterChange:{met.BunShi}/{met.Bunbo}:T[{met.TGrid.Unit},{met.TGrid.Grid}]"
            var met = new MeterChange();
            var data = seg[1].Split(":");

            var metData = data[0].Split("/");
            met.BunShi = int.Parse(metData[0]);
            met.Bunbo = int.Parse(metData[1]);

            met.TGrid = data[1].ParseToTGrid();

            fumen.AddObject(met);
        }
    }
}
