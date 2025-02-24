﻿using Caliburn.Micro;
using OngekiFumenEditor.Base;
using OngekiFumenEditor.Modules.FumenVisualEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OngekiFumenEditor.Modules.FumenVisualEditor.Models
{
    public class EditorProjectDataModel : PropertyChangedBase
    {
        private string audioFilePath = default;
        [JsonInclude]
        public string AudioFilePath
        {
            get => audioFilePath;
            set => Set(ref audioFilePath, value);
        }

        private TimeSpan audioDuration = default;
        [JsonInclude]
        public TimeSpan AudioDuration
        {
            get => audioDuration;
            set => Set(ref audioDuration, value);
        }

        public EditorSetting EditorSetting { get; } = new EditorSetting();

        private string fumenFilePath = default;
        [JsonInclude]
        public string FumenFilePath
        {
            get => fumenFilePath;
            set => Set(ref fumenFilePath, value);
        }

        private OngekiFumen fumen = new();
        [JsonIgnore]
        public OngekiFumen Fumen
        {
            get => fumen;
            set
            {
                Set(ref fumen, value);
                NotifyOfPropertyChange(() => BaseBPM);
            }
        }

        [JsonIgnore]
        public double BaseBPM
        {
            get => Fumen.MetaInfo.BpmDefinition.First;
            set
            {
                if (Fumen is not null)
                {
                    Fumen.MetaInfo.BpmDefinition.First = value;
                    Fumen.BpmList.FirstBpm.BPM = value;
                }
                NotifyOfPropertyChange(() => BaseBPM);
            }
        }

        public class StoreBulletPalleteEditorData
        {
            public string Name { get; set; }
            public Color AuxiliaryLineColor { get; set; }
        }

        public Dictionary<string, StoreBulletPalleteEditorData> StoreBulletPalleteEditorDatas { get; set; } = new();
    }
}
