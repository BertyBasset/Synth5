namespace WpfUi.MidiControllers;

public class ControlKnob {
    public int ID { get; set; } = 0;
    public string Description { get; set; } = "";
    public string ModuleName { get; set; } = "";
    public string PropertyName { get; set; } = "";
    public double Min { get; set; }
    public double Max { get; set; }
    public bool Integral { get; set; } = false;


    public static List<ControlKnob> GetList() {
        var list = new List<ControlKnob> {
            new ControlKnob() { ID = 0, Description = "[select]" },
            new ControlKnob() { ID = 1, Description = "VCO1 Frequency", ModuleName = "modVCO1", PropertyName = "Frequency", Min = -.083, Max = .083 },
            new ControlKnob() { ID = 2, Description = "VCO1 Octave", ModuleName = "modVCO1", PropertyName = "Octave", Min = 0, Max = 3, Integral = true },
            new ControlKnob() { ID = 3, Description = "VCO1 Waveform", ModuleName = "modVCO1", PropertyName = "Waveform", Min = 0, Max = 5, Integral = true },
            new ControlKnob() { ID = 4, Description = "VCO1 Pulse Width", ModuleName = "modVCO1", PropertyName = "PulseWidth", Min = -.9, Max = .9 },

            new ControlKnob() { ID = 5, Description = "VCO2 Frequency", ModuleName = "modVCO2", PropertyName = "Frequency", Min = -.083, Max = .083 },
            new ControlKnob() { ID = 6, Description = "VCO2 Octave", ModuleName = "modVCO2", PropertyName = "Octave", Min = 0, Max = 3, Integral = true },
            new ControlKnob() { ID = 7, Description = "VCO2 Waveform", ModuleName = "modVCO2", PropertyName = "Waveform", Min = 0, Max = 5, Integral = true },
            new ControlKnob() { ID = 8, Description = "VCO2 Pulse Width", ModuleName = "modVCO2", PropertyName = "PulseWidth", Min = -.9, Max = .9 },

            new ControlKnob() { ID = 9, Description = "VCO2 Frequency", ModuleName = "modVCO3", PropertyName = "Frequency", Min = -.083, Max = .083 },
            new ControlKnob() { ID = 10, Description = "VCO3 Octave", ModuleName = "modVCO3", PropertyName = "Octave", Min = 0, Max = 3, Integral = true },
            new ControlKnob() { ID = 11, Description = "VCO3 Waveform", ModuleName = "modVCO3", PropertyName = "Waveform", Min = 0, Max = 5, Integral = true },
            new ControlKnob() { ID = 12, Description = "VCO3 Pulse Width", ModuleName = "modVCO3", PropertyName = "PulseWidth", Min = -.9, Max = .9 },

            new ControlKnob() { ID = 13, Description = "Mixer Osc1", ModuleName = "modMixer", PropertyName = "Osc1", Min = 0, Max = 1 },
            new ControlKnob() { ID = 14, Description = "Mixer Osc2", ModuleName = "modMixer", PropertyName = "Osc2", Min = 0, Max = 1 },
            new ControlKnob() { ID = 15, Description = "Mixer Osc3", ModuleName = "modMixer", PropertyName = "Osc3", Min = 0, Max = 1 },
            new ControlKnob() { ID = 16, Description = "Mixer Noise", ModuleName = "modMixer", PropertyName = "Noise", Min = 0, Max = 1 },

            new ControlKnob() { ID = 17, Description = "Env1 Attack", ModuleName = "modEnv1", PropertyName = "Attack", Min = 0, Max = 5 },
            new ControlKnob() { ID = 18, Description = "Env1 Decay", ModuleName = "modEnv1", PropertyName = "Decay", Min = 0, Max = 5 },
            new ControlKnob() { ID = 19, Description = "Env1 Sustain", ModuleName = "modEnv1", PropertyName = "Sustain", Min = 0, Max = 1 },
            new ControlKnob() { ID = 20, Description = "Env1 Release", ModuleName = "modEnv1", PropertyName = "Release", Min = 0, Max = 5 },

            new ControlKnob() { ID = 21, Description = "Env2 Attack", ModuleName = "modEnv2", PropertyName = "Attack", Min = 0, Max = 5 },
            new ControlKnob() { ID = 22, Description = "Env2 Decay", ModuleName = "modEnv2", PropertyName = "Decay", Min = 0, Max = 5 },
            new ControlKnob() { ID = 23, Description = "Env2 Sustain", ModuleName = "modEnv2", PropertyName = "Sustain", Min = 0, Max = 1 },
            new ControlKnob() { ID = 24, Description = "Env2 Release", ModuleName = "modEnv2", PropertyName = "Release", Min = 0, Max = 5 },

            new ControlKnob() { ID = 25, Description = "LFO 1 Rate", ModuleName = "modDualLFO", PropertyName = "Rate1", Min = 0, Max = 1 },
            new ControlKnob() { ID = 26, Description = "LFO 1 Shape", ModuleName = "modDualLFO", PropertyName = "Shape1", Min = 0, Max = 5, Integral = true },
            new ControlKnob() { ID = 27, Description = "LFO 1 Delay", ModuleName = "modDualLFO", PropertyName = "Delay1", Min = 0, Max = 5 },

            new ControlKnob() { ID = 28, Description = "LFO 2 Rate", ModuleName = "modDualLFO", PropertyName = "Rate2", Min = 0, Max = 1 },
            new ControlKnob() { ID = 29, Description = "LFO 2 Shape", ModuleName = "modDualLFO", PropertyName = "Shape2", Min = 0, Max = 5, Integral = true },
            new ControlKnob() { ID = 30, Description = "LFO 2 Delay", ModuleName = "modDualLFO", PropertyName = "Delay2", Min = 0, Max = 5 },

            new ControlKnob() { ID = 31, Description = "Keyboard Glide", ModuleName = "modKeyboard", PropertyName = "Glide", Min = 0, Max = 500 },

            new ControlKnob() { ID = 32, Description = "Bitcrush Sample Rate", ModuleName = "modBitcrush", PropertyName = "SampleRate", Min = 0, Max = 1 },
            new ControlKnob() { ID = 33, Description = "Bitcrush Resolution", ModuleName = "modBitcrush", PropertyName = "Resolution", Min = 0, Max = 1 },

            new ControlKnob() { ID = 34, Description = "VCF Type", ModuleName = "modVCF", PropertyName = "FilterType", Min = 0, Max = 5, Integral = true },
            new ControlKnob() { ID = 35, Description = "VCF Cutoff", ModuleName = "modVCF", PropertyName = "Cutoff", Min = 0, Max = 1 },
            new ControlKnob() { ID = 36, Description = "VCF Resonance", ModuleName = "modVCF", PropertyName = "Resonance", Min = 0, Max = 1 },
            new ControlKnob() { ID = 37, Description = "VCF Envelope Amount", ModuleName = "modVCF", PropertyName = "EnvelopeAmount", Min = 0, Max = 1 },

            new ControlKnob() { ID = 38, Description = "VCF Attack", ModuleName = "modEnvVCF", PropertyName = "Attack", Min = 0, Max = 5 },
            new ControlKnob() { ID = 39, Description = "VCF Decay", ModuleName = "modEnvVCF", PropertyName = "Decay", Min = 0, Max = 5 },
            new ControlKnob() { ID = 40, Description = "VCF Sustain", ModuleName = "modEnvVCF", PropertyName = "Sustain", Min = 0, Max = 1 },
            new ControlKnob() { ID = 41, Description = "VCF Release", ModuleName = "modEnvVCF", PropertyName = "Release", Min = 0, Max = 5 },

            new ControlKnob() { ID = 42, Description = "VCA Attack", ModuleName = "modEnvVCA", PropertyName = "Attack", Min = 0, Max = 5 },
            new ControlKnob() { ID = 43, Description = "VCA Decay", ModuleName = "modEnvVCA", PropertyName = "Decay", Min = 0, Max = 5 },
            new ControlKnob() { ID = 44, Description = "VCA Sustain", ModuleName = "modEnvVCA", PropertyName = "Sustain", Min = 0, Max = 1 },
            new ControlKnob() { ID = 45, Description = "VCA Release", ModuleName = "modEnvVCA", PropertyName = "Release", Min = 0, Max = 5 },

            new ControlKnob() { ID = 46, Description = "Effects Type", ModuleName = "modEffects", PropertyName = "EffectType", Min = 0, Max = 5, Integral=true },
            new ControlKnob() { ID = 47, Description = "Effects Gain", ModuleName = "modEffects", PropertyName = "Gain", Min = 0, Max = 1 },
            new ControlKnob() { ID = 48, Description = "Effects Frequency", ModuleName = "modEffects", PropertyName = "Frequency", Min = 0, Max = 1 },
            new ControlKnob() { ID = 49, Description = "Effects Mix", ModuleName = "modEffects", PropertyName = "Mix", Min = -1, Max = 1 }
        };

        return list;
    }
}

