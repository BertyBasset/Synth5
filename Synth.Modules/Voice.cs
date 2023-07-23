﻿using Synth.Modules;
using Synth.IO;

namespace Synth;

// Voice is simply a collection of modules. When Tick is applied to Voice, it applies Tick to all modules in the collection
public class Voice : iModule {
    public List<iModule> Modules = new();

    #region iModule Members
    public double Value { get; set; }
    public void Tick(double timeIncrement) { 
        float wave = 0; ;
        foreach (var module in Modules) {
            module.Tick(timeIncrement);
            if(module.GetType() == typeof(AudioOut))
                wave += (float)module.Value;
        }
        Value = wave;

    }
    #endregion
}
