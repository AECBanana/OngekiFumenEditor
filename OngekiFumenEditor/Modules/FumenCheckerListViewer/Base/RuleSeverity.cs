﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngekiFumenEditor.Modules.FumenCheckerListViewer.Base
{
    public enum RuleSeverity
    {
        //无所谓的建议
        Suggest,
        //可以不管的一般问题
        Problem,
        //必须处理的严重
        Error
    }
}
