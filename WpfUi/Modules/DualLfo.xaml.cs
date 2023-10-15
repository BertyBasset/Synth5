using Synth.Properties;

namespace WpfUi.Modules;

public partial class DualLfo : UserControl, ISelectableModule {
    #region Event declarations    
    // Knob value changed events
    public event EventHandler<double>? Rate1Changed;
    public event EventHandler<LFOWaveformType>? Shape1Changed;
    public event EventHandler<double>? Delay1Changed;
    public event EventHandler<double>? Rate2Changed;
    public event EventHandler<LFOWaveformType>? Shape2Changed;
    public event EventHandler<double>? Delay2Changed;

    // Module selector led clicked event
    public event EventHandler<bool>? ModuleSelectLedChanged;
    #endregion

    #region Public properties
    public string Caption1 {
        get { return lblCaption1.Text; }
        set { lblCaption1.Text = value; }
    }

    public string Caption2 {
        get { return lblCaption2.Text; }
        set { lblCaption2.Text = value; }
    }

    public Brush CaptionBrush {
        get { return lblCaption1.Foreground; }
        set { 
            lblCaption1.Foreground = value;
            lblCaption2.Foreground = value;
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

    public double Rate1 {
        get { return knbRate1.Value; }
        set { knbRate1.Value = value;}
    }

    public double Shape1 {
        get { return knbShape1.Value; }
        set { knbShape1.Value = value; }
    }

    public double Delay1 {
        get { return knbDelay1.Value; }
        set { knbDelay1.Value = value; }
    }

    public double Rate2 {
        get { return knbRate2.Value; }
        set { knbRate2.Value = value; }
    }

    public double Shape2 {
        get { return knbShape2.Value; }
        set { knbShape2.Value = value; }
    }

    public double Delay2 {
        get { return knbDelay2.Value; }
        set { knbDelay2.Value = value; }
    }

    public bool ModuleSelectLedOn{
        get { return ledSelect.LedOn; }
        set { ledSelect.LedOn = value; }
    }
    #endregion

    #region Constructor
    public DualLfo() {
        InitializeComponent();

        knbRate1.ValueChanged += (o, e) => Rate1Changed?.Invoke(this, knbRate1.Value);
        knbShape1.ValueChanged += (o, e) => Shape1Changed?.Invoke(this, (LFOWaveformType)knbShape1.Value);
        knbDelay1.ValueChanged += (o, e) => Delay1Changed?.Invoke(this, knbDelay1.Value);
        knbRate2.ValueChanged += (o, e) => Rate2Changed?.Invoke(this, knbRate2.Value);
        knbShape2.ValueChanged += (o, e) => Shape2Changed?.Invoke(this, (LFOWaveformType)knbShape2.Value);
        knbDelay2.ValueChanged += (o, e) => Delay2Changed?.Invoke(this, knbDelay2.Value);

        ledSelect.LedChanged += (o, e) => ModuleSelectLedChanged?.Invoke(this, ledSelect.LedOn);
    }
    #endregion
}
