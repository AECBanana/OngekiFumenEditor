﻿using OngekiFumenEditor.Base;
using OngekiFumenEditor.Base.OngekiObjects.ConnectableObject;
using OngekiFumenEditor.Modules.FumenVisualEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OngekiFumenEditor.Modules.FumenVisualEditor.Base.DropActions
{
    public abstract class EditorAddObjectDropAction : IEditorDropHandler
    {
        protected abstract DisplayObjectViewModelBase GetDisplayObject();

        public void Drop(FumenVisualEditorViewModel editor, Point mousePosition)
        {
            var displayObject = GetDisplayObject();
            var isFirst = true;

            editor.UndoRedoManager.ExecuteAction(LambdaUndoAction.Create("添加物件", () =>
            {
                editor.AddObject(displayObject);
                displayObject.MoveCanvas(mousePosition);
                editor.Redraw(RedrawTarget.OngekiObjects);

                if (isFirst)
                {
                    editor.NotifyObjectClicked(displayObject);
                    isFirst = false;
                }
            }, () =>
            {
                editor.RemoveObject(displayObject);
                editor.Redraw(RedrawTarget.OngekiObjects);
            }));
        }
    }
}
