﻿using Caliburn.Micro;
using Gemini.Modules.Settings;
using Microsoft.Win32;
using OngekiFumenEditor.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OngekiFumenEditor.Kernel.SettingPages.Audio.ViewModels
{
    [Export(typeof(ISettingsEditor))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AudioSettingViewModel : PropertyChangedBase, ISettingsEditor
    {
        public Properties.AudioSetting Setting => Properties.AudioSetting.Default;

        public AudioSettingViewModel()
        {
            Setting.PropertyChanged += SettingPropertyChanged;
        }

        private void SettingPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Log.LogDebug($"audio setting property changed : {e.PropertyName}");
        }

        public string SettingsPageName => "音频";

        public string SettingsPagePath => "音效";

        public void ApplyChanges()
        {
            Setting.Save();
        }

        public void OnSoundFolderPathButtonClick()
        {
            using var openFolderDialog = new FolderBrowserDialog();
            openFolderDialog.ShowNewFolderButton = true;
            openFolderDialog.SelectedPath = Path.GetFullPath(Setting.SoundFolderPath);
            if (openFolderDialog.ShowDialog() == DialogResult.OK)
            {
                var folderPath = openFolderDialog.SelectedPath;
                if (!Directory.Exists(folderPath))
                {
                    MessageBox.Show("选择的文件夹为空,请重新选择");
                    OnSoundFolderPathButtonClick();
                    return;
                }
                Setting.SoundFolderPath = folderPath;
                ApplyChanges();
            }
        }
    }
}
