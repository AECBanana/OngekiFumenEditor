﻿using OngekiFumenEditor.Base;
using OngekiFumenEditor.Modules.FumenObjectPropertyBrowser.UIGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngekiFumenEditor.Modules.FumenObjectPropertyBrowser.ViewModels
{
    public class RangeValueTypeUIViewModel : CommonUIViewModelBase<RangeValue>
    {
        public float CurrentValue
        {
            get => TypedProxyValue.CurrentValue;
            set
            {
                if (PropertyInfo is UndoablePropertyInfoWrapper undoable)
                    undoable.ExecuteSubPropertySetAction(nameof(RangeValue.CurrentValue), (val) => TypedProxyValue.CurrentValue = val, CurrentValue, value);
                else
                    TypedProxyValue.CurrentValue = value;

                NotifyOfPropertyChange(() => CurrentValue);
            }
        }

        public float MinValue
        {
            get => TypedProxyValue.MinValue;
            set
            {
                if (PropertyInfo is UndoablePropertyInfoWrapper undoable)
                    undoable.ExecuteSubPropertySetAction(nameof(RangeValue.MinValue), (val) => TypedProxyValue.MinValue = val, MinValue, value);
                else
                    TypedProxyValue.MinValue = value;

                NotifyOfPropertyChange(() => MinValue);
            }
        }

        public float MaxValue
        {
            get => TypedProxyValue.MaxValue;
            set
            {
                if (PropertyInfo is UndoablePropertyInfoWrapper undoable)
                    undoable.ExecuteSubPropertySetAction(nameof(RangeValue.MaxValue), (val) => TypedProxyValue.MaxValue = val, MaxValue, value);
                else
                    TypedProxyValue.MaxValue = value;

                NotifyOfPropertyChange(() => MaxValue);
            }
        }

        public RangeValueTypeUIViewModel(PropertyInfoWrapper wrapper) : base(wrapper)
        {

        }
    }
}
