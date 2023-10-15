namespace WpfUi.Modules;

public partial class ADSR : UserControl, ISelectableModule {
    #region Event declarations
    public event EventHandler<bool>? ModuleSelectLedChanged;
    // Knob value changed events
    public event EventHandler<double>? AttackChanged;
    public event EventHandler<double>? DecayChanged;
    public event EventHandler<double>? SustainChanged;
    public event EventHandler<double>? ReleaseChanged;

    #endregion

    #region Public Properties
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

    public double Attack {
        get { return knbAttack.Value; }
        set { knbAttack.Value = value;}
    }

    public double Decay {
        get { return knbDecay.Value; }
        set { knbDecay.Value = value; }
    }

    public double Sustain {
        get { return knbSustain.Value; }
        set { knbSustain.Value = value; }
    }

    public double Release {
        get { return knbRelease.Value; }
        set { knbRelease.Value = value; }
    }

    public bool ModuleSelectLedOn{
        get { return ledSelect.LedOn; }
        set { ledSelect.LedOn = value; }
    }
    #endregion

    #region Constructor
    public ADSR() {
        InitializeComponent();

        knbAttack.ValueChanged += (o, e) => AttackChanged?.Invoke(this, knbAttack.Value);
        knbDecay.ValueChanged += (o, e) => DecayChanged?.Invoke(this, knbDecay.Value);
        knbSustain.ValueChanged += (o, e) => SustainChanged?.Invoke(this, knbSustain.Value);
        knbRelease.ValueChanged += (o, e) => ReleaseChanged?.Invoke(this, knbRelease.Value);

        ledSelect.LedChanged += (o, e) => ModuleSelectLedChanged?.Invoke(this, ledSelect.LedOn);
    }
    #endregion
}
