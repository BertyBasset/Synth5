using Synth;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using System.Xml;
using WpfUi.Modules;
using WpfUi.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

// To Do
// 1.  Wave viewer
//     a. Check boxes
//     b. Add grid/legend
// 2.  Controllers setup
// 3.   Maybe break for Inventory
// 4.  Patch Save/Load/Init - and categories?
// 5.  Modulation Section



namespace WpfUi;

public partial class SynthUI : Window {
    private Point origin;
    private Point start;
    private bool isPanning = false;

    readonly Synth.Patch patch = new();

    #region Constructor
    public SynthUI() {
        InitializeComponent();

        AddModuleSelectedEventHandlers();
        AddModuleControlsEventHandlers();
        AddPatchEventHandlers();
        AddPageEventHandlers();

        InitPatch();
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
        modVCF.ResonanceChanged += (f, resonance) => {
            _ = patch.VcfFilterType switch {
                Enums.FilterType.Butterworth => patch.VcfResonance = resonance,
                Enums.FilterType.Chebyshev => patch.VcfRippleFactor = resonance,
                Enums.FilterType.BandPass or Enums.FilterType.Notch => patch.VcfBandwidth = resonance,
                _ => default // Default case does nothing
            };
        };
        modVCF.EnvelopeAmountChanged  += (f, envAmount)  => patch.VcfEnvelopeAmount  = envAmount;

        modEffects.EffectTypeChanged  += (f, effecttype) => patch.EffectType         = effecttype;
        modEffects.GainChanged        += (f, gain)       => patch.EffectParam1       = gain;
        modEffects.FrequencyChanged   += (f, frequency)  => patch.EffectParam2       = frequency;
        modEffects.MixChanged         += (f, mix)        => patch.EffectMix          = mix;

        modEnvVCF.AttackChanged       += (e, attack)     => patch.VcfEnvAttack       = attack;
        modEnvVCF.DecayChanged        += (e, decay)      => patch.VcfEnvDecay        = decay;
        modEnvVCF.SustainChanged      += (e, sustain)    => patch.VcfEnvSustain      = sustain;
        modEnvVCF.ReleaseChanged      += (e, release)    => patch.VcfEnvRelease      = release;

        modEnvVCA.AttackChanged       += (e, attack)     => patch.VcaEnvAttack       = attack;
        modEnvVCA.DecayChanged        += (e, decay)      => patch.VcaEnvDecay        = decay;
        modEnvVCA.SustainChanged      += (e, sustain)    => patch.VcaEnvSustain      = sustain;
        modEnvVCA.ReleaseChanged      += (e, release)    => patch.VcaEnvRelease      = release;

    }

    private void AddPatchEventHandlers() { 
        patch.Lfo1StateChanged        += (o, state)      => modDualLFO.RateLedOn1    = state;
        patch.Lfo2StateChanged        += (o, state)      => modDualLFO.RateLedOn2    = state;
        patch.KeyChanged              += (o, keystate)   => modKeyboard.KeyState     = keystate.Value;


    }

    #endregion

    #region Page Event Handlers
    void AddPageEventHandlers() {
        canvasContent.PreviewMouseWheel += CanvasContent_PreviewMouseWheel;
        canvasContent.PreviewMouseDown += CanvasContent_PreviewMouseDown;
        canvasContent.PreviewMouseUp += CanvasContent_PreviewMouseUp;
        canvasContent.MouseMove += CanvasContent_MouseMove;
        this.MouseRightButtonDown += CanvasContent_MouseRightButtonDown;
        canvasContent.MouseRightButtonDown += CanvasContent_MouseRightButtonDown;

        //cmdSave.Click += (o, e) => Patching.SavePatch("init.json", canvasContent);
        //cmdLoad.Click += (o, e) => Patching.LoadPatch("init.json", canvasContent);

        modKeyboard.WaveViewerButtonClicked += (o, e) => {
            var c = new WaveViewer(patch.SynthEngine);
            c.Show();
        };
        modKeyboard.ControllersButtonClicked += (o, e) => {
            var c = new Controllers();
            c.Show();
        };
    }

    private void CanvasContent_MouseRightButtonDown(object sender, MouseButtonEventArgs e) {
        // Reset zoom and pan on right click
        if (e.RightButton == MouseButtonState.Pressed)
            canvasContent.RenderTransform = new MatrixTransform(Matrix.Identity);
    }

    private void CanvasContent_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
        Point position = e.GetPosition(canvasContent);
        double scale = e.Delta > 0 ? 1.1 : 0.9; // Zoom in or out based on the mouse wheel direction
        ScaleCanvas(scale, position);
    }

    private void CanvasContent_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
        if (e.ChangedButton == MouseButton.Middle) {
            canvasContent.CaptureMouse();
            isPanning = true;
            start = e.GetPosition(this);
            origin = new Point(canvasContent.RenderTransform.Value.OffsetX, canvasContent.RenderTransform.Value.OffsetY);
        }
    }

    private void CanvasContent_PreviewMouseUp(object sender, MouseButtonEventArgs e) {
        if (isPanning && e.ChangedButton == MouseButton.Middle) {
            canvasContent.ReleaseMouseCapture();
            isPanning = false;
        }
    }

    private void CanvasContent_MouseMove(object sender, MouseEventArgs e) {
        if (isPanning) {
            Point p = e.MouseDevice.GetPosition(this);
            Matrix matrix = canvasContent.RenderTransform.Value;
            matrix.OffsetX = origin.X + (p.X - start.X);
            matrix.OffsetY = origin.Y + (p.Y - start.Y);

            canvasContent.RenderTransform = new MatrixTransform(matrix);
        }
    }

    private void ScaleCanvas(double scale, Point position) {
        Matrix matrix = canvasContent.RenderTransform.Value;
        matrix.ScaleAt(scale, scale, position.X, position.Y);
        canvasContent.RenderTransform = new MatrixTransform(matrix);
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

    #region Patch
    void InitPatch() {
        Patching.LoadPatch("init.json", canvasContent);
    }
  


    #endregion

}