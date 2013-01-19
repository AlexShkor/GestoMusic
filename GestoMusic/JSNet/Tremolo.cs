﻿// This effect Copyright (C) 2004 and later Cockos Incorporated
// License: GPL - http://www.gnu.org/licenses/gpl.html
// ported to .NET by Mark Heath

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace JSNet
{
    [Export(typeof(Effect))]
    public class Tremolo : Effect
    {
        public Tremolo()
        {
            AddSlider(4,0,100,1,"frequency (Hz)");
            AddSlider(-6,-60,0,1,"amount (dB)");
            AddSlider(0, 0, 1, 0.1f, "stereo separation (0..1)");
        }

        float adv;
        float sep;
        float amount;
        float sc;
        float pos;

        public override string Name
        {
            get { return "Tremolo"; }
        }

        public override void Slider()
        {
            adv=PI*2*slider1/SampleRate;
            sep=slider3*PI;
            amount=pow(2,slider2/6);
            sc=0.5f*amount; amount=1-amount;
        }

        public override void Sample(ref float spl0, ref float spl1)
        {
            spl0 = spl0 * ((cos(pos) + 1) * sc + amount);
            spl1 = spl1 * ((cos(pos + sep) + 1) * sc + amount);
            pos += adv;
        }
    }
}
