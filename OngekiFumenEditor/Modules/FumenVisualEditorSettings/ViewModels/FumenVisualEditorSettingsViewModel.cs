﻿using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Services;
using OngekiFumenEditor.Base;
using OngekiFumenEditor.Modules.FumenVisualEditor.Kernel;
using OngekiFumenEditor.Modules.FumenVisualEditor.Models;
using OngekiFumenEditor.Modules.FumenVisualEditor.ViewModels;
using OngekiFumenEditor.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OngekiFumenEditor.Modules.FumenVisualEditor.Models.EditorSetting;

namespace OngekiFumenEditor.Modules.FumenVisualEditorSettings.ViewModels
{
    [Export(typeof(IFumenVisualEditorSettings))]
    public class FumenVisualEditorSettingsViewModel : Tool, IFumenVisualEditorSettings
    {
        public double[] UnitCloseSizeValues { get; } = new[]
        {
            1,
            1.5,
            2,
            3,
            4,
            4.5,
            6,
            8,
            9,
            12,
        };

        public string[] SupportTimeFormats { get; } = new[]
        {
            nameof(TimeFormat.TGrid),
            nameof(TimeFormat.AudioTime)
        };

        public override PaneLocation PreferredLocation => PaneLocation.Right;

        private FumenVisualEditorViewModel editor = default;
        public FumenVisualEditorViewModel Editor
        {
            get => editor;
            set
            {
                Set(ref editor, value);
                Setting = Editor?.Setting;


                if (Editor is null)
                    DisplayName = "编辑器设置";
                else
                    DisplayName = "编辑器设置 - " + Editor.FileName;
            }
        }

        private EditorSetting setting;
        public EditorSetting Setting
        {
            get => setting;
            set => Set(ref setting, value);
        }

        public FumenVisualEditorSettingsViewModel()
        {
            DisplayName = "编辑器设置";
            IoC.Get<IEditorDocumentManager>().OnActivateEditorChanged += OnActivateEditorChanged;
            Editor = IoC.Get<IEditorDocumentManager>().CurrentActivatedEditor;
        }

        private void OnActivateEditorChanged(FumenVisualEditorViewModel @new, FumenVisualEditorViewModel old)
        {
            Editor = @new;
            this.RegisterOrUnregisterPropertyChangeEvent(old, @new, OnEditorPropertyChanged);
        }

        private void OnEditorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FumenVisualEditorViewModel.Setting))
            {
                Setting = Editor?.Setting;
            }
        }

        public void OnSliderValueChanged()
        {
            Editor?.ClearDisplayingObjectCache();
            Editor?.Redraw(FumenVisualEditor.Base.RedrawTarget.All);
        }
    }
}
