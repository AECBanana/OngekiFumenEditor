﻿using Caliburn.Micro;
using OngekiFumenEditor.Base.OngekiObjects.ConnectableObject;
using OngekiFumenEditor.Modules.FumenVisualEditor.ViewModels.OngekiObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OngekiFumenEditor.Base.OngekiObjects.Beam
{
    public class BeamEnd : ConnectableEndObject,IBeamObject
    {
        public override Type ModelViewType => typeof(BeamEndViewModel);

        public override string IDShortName => "BME"; 
        
        private int widthId = 2;
        public int WidthId
        {
            get => widthId;
            set => Set(ref widthId, value);
        }
    }
}
