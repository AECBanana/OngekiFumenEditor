﻿using Caliburn.Micro;
using OngekiFumenEditor.Base.Collections;
using OngekiFumenEditor.Base.Collections.Base;
using OngekiFumenEditor.Base.EditorObjects.LaneCurve;
using OngekiFumenEditor.Base.EditorObjects.Svg;
using OngekiFumenEditor.Base.OngekiObjects;
using OngekiFumenEditor.Base.OngekiObjects.Beam;
using OngekiFumenEditor.Base.OngekiObjects.ConnectableObject;
using OngekiFumenEditor.Base.OngekiObjects.Lane.Base;
using OngekiFumenEditor.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OngekiFumenEditor.Base
{
    public class OngekiFumen : PropertyChangedBase
    {
        private FumenMetaInfo metaInfo = new FumenMetaInfo();
        public FumenMetaInfo MetaInfo
        {
            get => metaInfo;
            set
            {
                this.RegisterOrUnregisterPropertyChangeEvent(metaInfo, value, OnFumenMetaInfoPropChanged);
                Set(ref metaInfo, value);
            }
        }

        public event Action<OngekiObjectBase, PropertyChangedEventArgs> ObjectModifiedChanged;

        public BulletPalleteList BulletPalleteList { get; } = new();
        public BpmList BpmList { get; } = new();
        public LaneList Lanes { get; } = new();
        public TGridSortList<Bell> Bells { get; } = new();
        public TGridSortList<Flick> Flicks { get; } = new();
        public TGridSortList<Bullet> Bullets { get; } = new();
        public TGridSortList<ClickSE> ClickSEs { get; } = new();
        public MeterChangeList MeterChanges { get; } = new();
        public TGridSortList<EnemySet> EnemySets { get; } = new();
        public BeamList Beams { get; } = new();
        public List<SvgPrefabBase> SvgPrefabs { get; } = new();
        public TGridSortList<Soflan> Soflans { get; } = new();
        public LaneBlockAreaList LaneBlocks { get; } = new();
        public TGridSortList<Tap> Taps { get; } = new();
        public HoldList Holds { get; } = new();

        public OngekiFumen()
        {
            Setup();
        }

        #region Overload Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddObjects(IEnumerable<OngekiObjectBase> objs)
        {
            foreach (var item in objs)
            {
                AddObject(item);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveObjects(IEnumerable<OngekiObjectBase> objs)
        {
            foreach (var item in objs)
            {
                RemoveObject(item);
            }
        }
        #endregion

        public void Setup()
        {
            MetaInfo = MetaInfo;

            //setup firstBPM from fumen metainfo
            var firstBpm = new BPMChange()
            {
                TGrid = new TGrid()
                {
                    Grid = 0,
                    Unit = 0
                },
                BPM = MetaInfo.BpmDefinition.First,
            };
            var unusedBpm = BpmList.FirstOrDefault(x => x.BPM == firstBpm.BPM && x.TGrid == TGrid.Zero);
            if (unusedBpm is not null && unusedBpm != BpmList.FirstBpm)
                BpmList.Remove(unusedBpm);
            BpmList.SetFirstBpm(firstBpm);

            //setup firstMeter from fumen metainfo
            var firstMeter = new MeterChange()
            {
                TGrid = new TGrid()
                {
                    Grid = 0,
                    Unit = 0
                },
                Bunbo = MetaInfo.MeterDefinition.Bunbo,
                BunShi = MetaInfo.MeterDefinition.Bunshi,
            };
            var unusedMeter = MeterChanges.FirstOrDefault(x => x.Bunbo == firstMeter.Bunbo && x.BunShi == firstMeter.BunShi && x.TGrid == TGrid.Zero);
            if (unusedMeter is not null && MeterChanges.FirstMeter != unusedMeter)
                MeterChanges.Remove(unusedMeter);
            MeterChanges.SetFirstBpm(firstMeter);
        }

        public void AddObject(OngekiObjectBase obj)
        {
            if (obj is Bell bel)
            {
                Bells.Add(bel);
            }
            else if (obj is BPMChange bpm)
            {
                BpmList.Add(bpm);
            }
            else if (obj is MeterChange met)
            {
                MeterChanges.Add(met);
            }
            else if (obj is EnemySet est)
            {
                EnemySets.Add(est);
            }
            else if (obj is BulletPallete bpl)
            {
                BulletPalleteList.AddPallete(bpl);
            }
            else if (obj is Flick flick)
            {
                Flicks.Add(flick);
            }
            else if (obj is IBeamObject beam)
            {
                Beams.Add(beam);
            }
            else if (obj is Soflan soflan)
            {
                Soflans.Add(soflan);
            }
            else if (obj is Bullet bullet)
            {
                Bullets.Add(bullet);
            }
            else if (obj is ClickSE clickSE)
            {
                ClickSEs.Add(clickSE);
            }
            else if (obj is Tap tap)
            {
                Taps.Add(tap);
            }
            else if (obj is Hold hold)
            {
                Holds.Add(hold);
            }
            else if (obj is HoldEnd holdEnd)
            {
                Holds.Add(holdEnd);
            }
            else if (obj is LaneBlockArea laneBlock)
            {
                LaneBlocks.Add(laneBlock);
            }
            else if (obj is SvgPrefabBase prefab)
            {
                SvgPrefabs.Add(prefab);
            }
            else if (obj switch
            {
                LaneStartBase or LaneEndBase or LaneNextBase => obj,
                _ => null
            } is ConnectableObjectBase lane)
            {
                Lanes.Add(lane);
            }
            else
            {
                Log.LogWarn($"add-in list target not found, object type : {obj?.GetType()?.GetTypeName()}");
                return;
            }

            if (obj is ConnectableStartObject start)
                start.ConnectableObjectsPropertyChanged += OnOngekiObjectModify;
            else
                obj.PropertyChanged += OnOngekiObjectModify;
        }

        public void RemoveObject(OngekiObjectBase obj)
        {
            if (obj is Bell bel)
            {
                Bells.Remove(bel);
            }
            else if (obj is BPMChange bpm)
            {
                BpmList.Remove(bpm);
            }
            else if (obj is MeterChange met)
            {
                MeterChanges.Remove(met);
            }
            else if (obj is EnemySet est)
            {
                EnemySets.Remove(est);
            }
            else if (obj is Soflan soflan)
            {
                Soflans.Remove(soflan);
            }
            else if (obj is BulletPallete bpl)
            {
                BulletPalleteList.RemovePallete(bpl);
            }
            else if (obj is Flick flick)
            {
                Flicks.Remove(flick);
            }
            else if (obj is IBeamObject beam)
            {
                Beams.Remove(beam);
            }
            else if (obj is Bullet bullet)
            {
                Bullets.Remove(bullet);
            }
            else if (obj is ClickSE clickSE)
            {
                ClickSEs.Remove(clickSE);
            }
            else if (obj is Tap tap)
            {
                Taps.Remove(tap);
            }
            else if (obj is Hold hold)
            {
                Holds.Remove(hold);
            }
            else if (obj is HoldEnd holdEnd)
            {
                Holds.Remove(holdEnd);
            }
            else if (obj is SvgPrefabBase prefab)
            {
                SvgPrefabs.Remove(prefab);
            }
            else if (obj is LaneCurvePathControlObject pathControl)
            {
                pathControl.RefCurveObject.RemoveControlObject(pathControl);
            }
            else if (obj is LaneBlockArea laneBlock)
            {
                LaneBlocks.Remove(laneBlock);
            }
            else if (obj switch
            {
                LaneStartBase or LaneEndBase or LaneNextBase => obj,
                _ => null
            } is ConnectableObjectBase lane)
            {
                Lanes.Remove(lane);
            }
            else
            {
                Log.LogWarn($"delete list target not found, object type : {obj?.GetType()?.GetTypeName()}");
                return;
            }

            if (obj is ConnectableStartObject start)
                start.ConnectableObjectsPropertyChanged -= OnOngekiObjectModify;
            else
                obj.PropertyChanged -= OnOngekiObjectModify;
        }

        private void OnOngekiObjectModify(object sender, PropertyChangedEventArgs e)
        {
            if (sender is OngekiObjectBase obj)
            {
                //Log.LogDebug($"Modified property name: {e.PropertyName} , Obj : {obj}");
                ObjectModifiedChanged?.Invoke(obj, e);
            }
        }

        public IEnumerable<IDisplayableObject> GetAllDisplayableObjects()
        {
            return GetAllDisplayableObjects(TGrid.MinValue, TGrid.MaxValue);
        }

        public IEnumerable<IDisplayableObject> GetAllDisplayableObjects(TGrid min, TGrid max)
        {
            var first = Enumerable.Empty<IDisplayableObject>()
                   .Concat(Bells.BinaryFindRange(min, max))
                   .Concat(Flicks.BinaryFindRange(min, max))
                   .Concat(MeterChanges.Skip(1)) //not show first meter
                   .Concat(BpmList.Skip(1)) //not show first bpm
                   .Concat(ClickSEs.BinaryFindRange(min, max))
                   .Concat(LaneBlocks.GetVisibleStartObjects(min, max))
                   .Concat(Soflans)
                   .Concat(EnemySets.BinaryFindRange(min, max))
                   .Concat(Bullets.BinaryFindRange(min, max))
                   .Concat(Lanes.GetVisibleStartObjects(min, max))
                   .Concat(Taps.BinaryFindRange(min, max))
                   .Concat(Holds.GetVisibleStartObjects(min, max))
                   .Concat(SvgPrefabs)
                   .Concat(Beams)
                   .Distinct();

            return first.SelectMany(x => x.GetDisplayableObjects());
        }

        public bool Contains(OngekiObjectBase obj) => obj switch
        {
            Bell f => Bells.FastContains(f),
            Flick f => Flicks.FastContains(f),
            ClickSE f => ClickSEs.FastContains(f),
            EnemySet f => EnemySets.FastContains(f),
            Tap f => Taps.FastContains(f),
            Bullet f => Bullets.FastContains(f),
            BPMChange f => BpmList.Contains(f),
            MeterChange f => MeterChanges.Contains(f),
            LaneBlockArea f => LaneBlocks.Contains(f),
            LaneBlockArea.LaneBlockAreaEndIndicator f => LaneBlocks.Any(x => x.EndIndicator == f),
            Hold f => Holds.Contains(f),
            HoldEnd f => Holds.Any(x => x.HoldEnd == f),
            SvgPrefabBase f => SvgPrefabs.Contains(f),
            Soflan f => Soflans.Contains(f),
            Soflan.SoflanEndIndicator f => Soflans.Any(x => x.EndIndicator == f),
            LaneStartBase f => Lanes.Contains(f),
            (LaneNextBase or LaneEndBase) => Lanes.SelectMany(x => x.Children).Any(x => x == obj),
            BeamStart f => Beams.Contains(f),
            (BeamEnd or BeamNext) => Beams.SelectMany(x => x.Children).Any(x => x == obj),
            LaneCurvePathControlObject f => Beams.SelectMany(x => x.Children).SelectMany(x => x.PathControls).Contains(f),
            _ => false
        };

        private void OnFumenMetaInfoPropChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FumenMetaInfo.BpmDefinition))
            {
                BpmList.FirstBpm.BPM = MetaInfo.BpmDefinition.First;
                Log.LogDebug($"Apply metainfo.firstBpm to bpmList.firstBpm : {BpmList.FirstBpm.BPM}");
            }
            if (e.PropertyName == nameof(FumenMetaInfo.MeterDefinition))
            {
                MeterChanges.FirstMeter.Bunbo = MetaInfo.MeterDefinition.Bunbo;
                MeterChanges.FirstMeter.BunShi = MetaInfo.MeterDefinition.Bunshi;
                Log.LogDebug($"Apply metainfo.firstMeter to bpmList.firstMeter : {MeterChanges.FirstMeter.BunShi}/{MeterChanges.FirstMeter.Bunbo}");
            }
        }
    }
}
