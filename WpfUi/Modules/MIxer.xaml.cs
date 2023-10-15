namespace WpfUi.Modules;
public partial class Mixer : UserControl, ISelectableModule {
    #region Event declarations
    // Knob value changed events
    public event EventHandler<double>? Osc1LevelChanged;
    public event EventHandler<double>? Osc2LevelChanged;
    public event EventHandler<double>? Osc3LevelChanged;
    public event EventHandler<double>? NoiseLevelChanged;
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

    public Thickness BorderWidth{ 
        get { return brdBorder.BorderThickness; }
        set  {brdBorder.BorderThickness = value; }
    }

    public CornerRadius BorderRadius {
        get { return brdBorder.CornerRadius; }
        set { brdBorder.CornerRadius = value; }
    }

    public double Osc1 {
        get { return knbOsc1.Value; }
        set { knbOsc1.Value = value;}
    }

    public double Osc2{
        get { return knbOsc2.Value; }
        set { knbOsc2.Value = value; }
    }

    public double Osc3 {
        get { return knbOsc3.Value; }
        set { knbOsc3.Value = value; }
    }

    public double Noise {
        get { return knbNoise.Value; }
        set { knbNoise.Value = value; }
    }

    public bool ModuleSelectLedOn{
        get { return ledSelect.LedOn; }
        set { ledSelect.LedOn = value; }
    }
    #endregion

    #region Constructor
    public Mixer() {
        InitializeComponent();

        knbOsc1.ValueChanged += (o, e) => Osc1LevelChanged?.Invoke(this, knbOsc1.Value);
        knbOsc2.ValueChanged += (o, e) => Osc2LevelChanged?.Invoke(this, knbOsc2.Value);
        knbOsc3.ValueChanged += (o, e) => Osc3LevelChanged?.Invoke(this, knbOsc3.Value);
        knbNoise.ValueChanged += (o, e) => NoiseLevelChanged?.Invoke(this, knbNoise.Value);

        ledSelect.LedChanged += (o, e) => ModuleSelectLedChanged?.Invoke(this, ledSelect.LedOn);
    }
    #endregion
}
