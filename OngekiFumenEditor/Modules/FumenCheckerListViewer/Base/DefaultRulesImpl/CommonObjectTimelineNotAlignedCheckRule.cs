﻿using OngekiFumenEditor.Base;
using OngekiFumenEditor.Base.OngekiObjects;
using OngekiFumenEditor.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngekiFumenEditor.Modules.FumenCheckerListViewer.Base.DefaultRulesImpl
{
    [Export(typeof(IFumenCheckRule))]
    internal class CommonObjectTimelineNotAlignedCheckRule : IFumenCheckRule
    {
        public IEnumerable<ICheckResult> CheckRule(OngekiFumen fumen, object fumenHostViewModel)
        {
            IEnumerable<ICheckResult> CheckList(IEnumerable<OngekiTimelineObjectBase> objs)
            {
                const string RuleName = "ObjectTimelineNotAligned";
                var beats = fumen.MeterChanges.GetCachedAllTimeSignatureUniformPositionList(240, fumen.BpmList);

                var currentIndex = 0;

                var lengthPerBeat = 0d;
                var currentStartTGrid = default(TGrid);
                var currentMeter = default(MeterChange);
                var nextStartTGrid = default(TGrid);
                void UpdateStatus()
                {
                    (_, currentStartTGrid, currentMeter, _) = beats[currentIndex];
                    nextStartTGrid = beats.ElementAtOrDefault(currentIndex + 1).startTGrid;
                    //计算每一拍的(grid)长度
                    var resT = currentStartTGrid.ResT;
                    var beatCount = currentMeter.BunShi;
                    lengthPerBeat = resT * 1.0d / beatCount;
                }

                UpdateStatus();

                foreach (var obj in objs.OrderBy(x => x.TGrid))
                {
                    //确保obj属于currentStartTGrid里面的
                    while (nextStartTGrid != null && obj.TGrid >= nextStartTGrid)
                    {
                        currentIndex++;
                        UpdateStatus();
                    }

                    var diff = obj.TGrid - currentStartTGrid;
                    var totalGrid = diff.TotalGrid(obj.TGrid.ResT);
                    var div = totalGrid / lengthPerBeat;
                    var trck = div - Math.Truncate(div);
                    var beat = trck != 0 ? (1 / trck) : 0;
                    var revBeat = trck != 0 ? (1 / (1 - trck)) : 0;

                    if ((!(revBeat == (int)revBeat))&&(!(beat == (int)beat)))
                    {
                        yield return new CommonCheckResult()
                        {
                            Severity = RuleSeverity.Problem,
                            Description = $"物件{obj.IDShortName}没对上节奏",
                            LocationDescription = $"{obj}",
                            NavigateTGridLocation = obj.TGrid,
                            RuleName = RuleName,
                        };
                    }
                }
            }

            var objs = Enumerable.Empty<OngekiTimelineObjectBase>()
                .Concat(fumen.Taps);

            foreach (var result in CheckList(objs))
                yield return result;
        }
    }
}
