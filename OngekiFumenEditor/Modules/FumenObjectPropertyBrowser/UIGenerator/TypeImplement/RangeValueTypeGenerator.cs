﻿using OngekiFumenEditor.Base;
using OngekiFumenEditor.Modules.FumenObjectPropertyBrowser.ViewModels;
using OngekiFumenEditor.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OngekiFumenEditor.Modules.FumenObjectPropertyBrowser.UIGenerator.TypeImplement
{
    [Export(typeof(ITypeUIGenerator))]
    public class RangeValueTypeGenerator : ITypeUIGenerator
    {
        public IEnumerable<Type> SupportTypes { get; } = new[] {
            typeof(RangeValue)
        };

        public UIElement Generate(PropertyInfoWrapper wrapper)=> ViewHelper.CreateViewByViewModelType(() => new RangeValueTypeUIViewModel(wrapper));
    }
}
