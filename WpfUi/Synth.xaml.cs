using WpfUi.Modules;

// To Do
// 1.  Border colour stuck to white ?
// 2.  Module Caption background should be same as canvas
// 3.  Add Lfo LED + wire up
// 4.  Get synth working
// 5.  Add Midi Channel Selector
// 6.  Add Polygate viewer
// 7.  Wave viewer
// 8.  Cntrollers setup
// 9.  Patch Save/Load/Init - and categories?
// 10. Modulation Section



namespace WpfUi;

public partial class SynthUI : Window {
    readonly Synth.Patch patch = new();

    #region Constructor
    public SynthUI() {
        InitializeComponent();

        AddModuleSelectedEventHandlers();
        AddModuleControlsEventHandlers();
    }
    #endregion

    #region Event Handlers
    #region ModuleSelectedEventHandlers
    private void AddModuleSelectedEventHandlers() {
        modVCO1.ModuleSelectLedChanged      += (o, e) => ModuleSelect(o, e);
        modVCO2.ModuleSelectLedChanged      += (o, e) => ModuleSelect(o, e);
        modVCO3.ModuleSelectLedChanged      += (o, e) => ModuleSelect(o, e);
        modEnv1.ModuleSelectLedChanged      += (o, e) => ModuleSelect(o, e);
        modEnv2.ModuleSelectLedChanged      += (o, e) => ModuleSelect(o, e);
        modEnvVCF.ModuleSelectLedChanged    += (o, e) => ModuleSelect(o, e);
        modEnvVCA.ModuleSelectLedChanged    += (o, e) => ModuleSelect(o, e);
        modVCF.ModuleSelectLedChanged       += (o, e) => ModuleSelect(o, e);
        modEffects.ModuleSelectLedChanged   += (o, e) => ModuleSelect(o, e);
        modBitcrush.ModuleSelectLedChanged  += (o, e) => ModuleSelect(o, e);
        modEffects.ModuleSelectLedChanged   += (o, e) => ModuleSelect(o, e);
        modDualLFO.ModuleSelectLedChanged   += (o, e) => ModuleSelect(o, e);
        modMixer.ModuleSelectLedChanged     += (o, e) => ModuleSelect(o, e);
    }
    #endregion

    #region Module Controls Event Handlers
    private void AddModuleControlsEventHandlers() {
        modVCO1.FrequencyChanged      += (v, frequency)  => patch.Vco1FineTune       = frequency;
        modVCO1.OctaveChanged         += (v, octave)     => patch.Vco1Octave         = octave;
        modVCO1.WaveformChanged       += (v, waveform)   => patch.Vco1WaveFormType   = waveform;
        modVCO1.PulseWidthChanged     += (v, pulsewidth) => patch.Vco1PulseWidth     = pulsewidth;
                                                                                         
        modVCO2.FrequencyChanged      += (v, frequency)  => patch.Vco2FineTune       = frequency;
        modVCO2.OctaveChanged         += (v, octave)     => patch.Vco2Octave         = octave;
        modVCO2.WaveformChanged       += (v, waveform)   => patch.Vco2WaveFormType   = waveform;
        modVCO2.PulseWidthChanged     += (v, pulsewidth) => patch.Vco2PulseWidth     = pulsewidth;
                                          
        modVCO3.FrequencyChanged      += (v, frequency)  => patch.Vco1FineTune       = frequency;
        modVCO3.OctaveChanged         += (v, octave)     => patch.Vco1Octave         = octave;
        modVCO3.WaveformChanged       += (v, waveform)   => patch.Vco1WaveFormType   = waveform;
        modVCO3.PulseWidthChanged     += (v, pulsewidth) => patch.Vco1PulseWidth     = pulsewidth;
                                                                                         
        modEnv1.AttackChanged         += (e, attack)     => patch.Env1Attack         = attack;
        modEnv1.DecayChanged          += (e, decay)      => patch.Env1Decay          = decay;
        modEnv1.SustainChanged        += (e, sustain)    => patch.Env1Sustain        = sustain;
        modEnv1.ReleaseChanged        += (e, release)    => patch.Env1Release        = release;
                                                                                         
        modEnv2.AttackChanged         += (e, attack)     => patch.Env2Attack         = attack;
        modEnv2.DecayChanged          += (e, decay)      => patch.Env2Decay          = decay;
        modEnv2.SustainChanged        += (e, sustain)    => patch.Env2Sustain        = sustain;
        modEnv2.ReleaseChanged        += (e, release)    => patch.Env2Release        = release;
                                                                                         
        modMixer.Osc1LevelChanged     += (m, level)      => patch.MixerVco1Level     = level;
        modMixer.Osc2LevelChanged     += (m, level)      => patch.MixerVco2Level     = level;
        modMixer.Osc3LevelChanged     += (m, level)      => patch.MixerVco3Level     = level;
        modMixer.NoiseLevelChanged    += (m, level)      => patch.MixerNoiseLevel    = level;

        modBitcrush.SampleRateChanged += (b, samplerate) => patch.BitCrushSampleRate = samplerate;
        modBitcrush.ResolutionChanged += (b, resolution) => patch.BitCrushResolution = resolution;

        modKeyboard.GlideChanged      += (k, glide)      => patch.Glide              = glide;

        modDualLFO.Rate1Changed       += (l, rate)       => patch.Lfo1Frequency      = rate;
        modDualLFO.Shape1Changed      += (l, shape)      => patch.Lfo1WaveformType   = shape;
        modDualLFO.Delay1Changed      += (l, delay)      => patch.Lfo1Delay          = delay;
        modDualLFO.Rate2Changed       += (l, rate)       => patch.Lfo2Frequency      = rate;
        modDualLFO.Shape2Changed      += (l, shape)      => patch.Lfo2WaveformType   = shape;
        modDualLFO.Delay2Changed      += (l, delay)      => patch.Lfo2Delay          = delay;

        modVCF.FilterTypeChanged      += (f, filtertype) => patch.VcfFilterType      = filtertype;
        modVCF.CutoffChanged          += (f, cutoff)     => patch.VcfCutoff          = cutoff;
        modVCF.ResonanceChanged       += (f, resonance)  => patch.VcfResonance       = resonance;
        modVCF.EnvelopeAmountChanged  += (f, envAmount)  => patch.VcfEnvelopeAmount  = envAmount;

        modEnvVCF.AttackChanged       += (e, attack)     => patch.VcfEnvAttack       = attack;
        modEnvVCF.DecayChanged        += (e, decay)      => patch.VcfEnvDecay        = decay;
        modEnvVCF.SustainChanged      += (e, sustain)    => patch.VcfEnvSustain      = sustain;
        modEnvVCF.ReleaseChanged      += (e, release)    => patch.VcfEnvRelease      = release;

        modEnvVCA.AttackChanged       += (e, attack)     => patch.VcaEnvAttack       = attack;
        modEnvVCA.DecayChanged        += (e, decay)      => patch.VcaEnvDecay        = decay;
        modEnvVCA.SustainChanged      += (e, sustain)    => patch.VcaEnvSustain      = sustain;
        modEnvVCA.ReleaseChanged      += (e, release)    => patch.VcaEnvRelease      = release;
    }
    #endregion
    #endregion

    #region Module Select using Leds
    private void ModuleSelect(object? o, bool LedState) {
        if (LedState) {
            // Switch Led on other modules off
            List<UserControl> userControls = FindUserControls(this); // 'this' refers to the current window or UserControl
            foreach (UserControl userControl in userControls) {
                // Do something with each user control
                if (userControl.Name == ((Control)o!).Name)
                    continue;

                if (userControl is ISelectableModule selectableModule)
                    selectableModule.ModuleSelectLedOn= false;
            }
            // Enable Midi controller for select module
        }
    }

    public static List<UserControl> FindUserControls(DependencyObject parent) {
        List<UserControl> userControls = new ();

        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++) {
            DependencyObject child = VisualTreeHelper.GetChild(parent, i);

            if (child is UserControl userControl) {
                userControls.Add(userControl);
            }

            // Recursively search for user controls in child elements
            userControls.AddRange(FindUserControls(child));
        }
        return userControls;
    }
    #endregion
}