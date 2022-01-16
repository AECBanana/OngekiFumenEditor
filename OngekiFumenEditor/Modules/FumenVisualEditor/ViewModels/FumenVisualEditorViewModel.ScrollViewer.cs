using Caliburn.Micro;
using Gemini.Framework;
using OngekiFumenEditor.Modules.FumenVisualEditor.Base;
using OngekiFumenEditor.UI.Controls;
using OngekiFumenEditor.Utils;
using System;
using System.Security.Cryptography;
using System.Windows.Controls;

namespace OngekiFumenEditor.Modules.FumenVisualEditor.ViewModels
{
    public partial class FumenVisualEditorViewModel : PersistedDocument
    {
        public double MinVisibleCanvasY => ScrollViewerVerticalOffset;
        public double MaxVisibleCanvasY => ScrollViewerVerticalOffset + CanvasHeight;

        private double totalDurationHeight;
        public double TotalDurationHeight
        {
            get => totalDurationHeight;
            set
            {
                value = Math.Max(value, CanvasHeight);
                //Log.LogDebug($"TotalDurationHeight {TotalDurationHeight} -> {value}");
                Set(ref totalDurationHeight, value);
            }
        }

        private double scrollViewerVerticalOffset;
        public double ScrollViewerVerticalOffset
        {
            get => scrollViewerVerticalOffset;
            set
            {
                Set(ref scrollViewerVerticalOffset, value);
                NotifyOfPropertyChange(() => MaxVisibleCanvasY);
                NotifyOfPropertyChange(() => MinVisibleCanvasY);
                Redraw(RedrawTarget.TGridUnitLines);
                //Log.LogDebug($"current:{ScrollViewerVerticalOffset:F2}  min:{MinVisibleCanvasY:F2}  max:{MaxVisibleCanvasY:F2}");
            }
        }

        private void RecalculateScrollBar()
        {
            //重新计算理论高度
        }

        public void ScrollViewer_OnScrollChanged(ActionExecutionContext e)
        {
            var arg = e.EventArgs as ScrollChangedEventArgs;
            var scrollViewer = e.Source as AnimatedScrollViewer;

            ScrollViewerVerticalOffset = scrollViewer.ScrollableHeight - arg.VerticalOffset;
            //Log.LogDebug($"ScrollViewerVerticalOffset = {ScrollViewerVerticalOffset}");
        }
    }
}
