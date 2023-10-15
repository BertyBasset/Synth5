using Synth.Properties;

namespace WpfUi.Modules;
public partial class VCO : UserControl, ISelectableModule {
    #region Event declarations
    // Knob value changed events
    public event EventHandler<double>? FrequencyChanged;
    public event EventHandler<int>? OctaveChanged;
    public event EventHandler<VCOWaveformType>? WaveformChanged;
    public event EventHandler<double>? PulseWidthChanged;
    // Module selector led clicked event
    public event EventHandler<bool>? ModuleSelectLedChanged;
    #endregion

    #region Public properties
    public string Caption {
        get { return lblCaption.Text; }
        set { lblCaption.Text = value; }
    }

    public Brush CaptionBrush {
        get { return lblCaption.Foreground; }
        set {
            lblCaption.Foreground = value;
        }
    }

    public Brush BorderColor {
        get { return brdBorder.BorderBrush; }
        set { brdBorder.BorderBrush = value; }
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Brush BorderBrush {
        get { return BorderColor; }
        set { BorderColor = value; }
    }

    public Thickness BorderWidth {
        get { return brdBorder.BorderThickness; }
        set { brdBorder.BorderThickness = value; }
    }

    public CornerRadius BorderRadius {
        get { return brdBorder.CornerRadius; }
        set { brdBorder.CornerRadius = value; }
    }

    public double Frequency {
        get { return knbFrequency.Value; }
        set { knbFrequency.Value = value; }
    }

    public double Octave {
        get { return knbOctave.Value; }
        set { knbOctave.Value = value; }
    }

    public double Waveform {
        get { return knbWaveform.Value; }
        set { knbWaveform.Value = value; }
    }

    public double PulseWidth {
        get { return knbPulseWidth.Value; }
        set { knbPulseWidth.Value = value; }
    }

    public bool ModuleSelectLedOn{
        get { return ledSelect.LedOn; }
        set { ledSelect.LedOn = value; }
    }
    #endregion

    #region Constructor
    public VCO() {
        InitializeComponent();

        knbFrequency.ValueChanged += (o, e) => FrequencyChanged?.Invoke(this, knbFrequency.Value);
        knbOctave.ValueChanged += (o, e) => OctaveChanged?.Invoke(this, (int)knbOctave.Value);
        knbWaveform.ValueChanged += (o, e) => WaveformChanged?.Invoke(this, (VCOWaveformType)knbWaveform.Value);
        knbPulseWidth.ValueChanged += (o, e) => PulseWidthChanged?.Invoke(this, knbPulseWidth.Value);

        ledSelect.LedChanged += (o, e) => ModuleSelectLedChanged?.Invoke(this, ledSelect.LedOn);
    }
    #endregion
}