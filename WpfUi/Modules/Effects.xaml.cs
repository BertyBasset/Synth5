using Synth;
using Synth.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace WpfUi.Modules;

public partial class Effects : UserControl, ISelectableModule {
    #region Event declarations
    // Knob value changed events
    // Pass enums back here
    public event EventHandler<Enums.EffectType>? EffectTypeChanged;
    public event EventHandler<double>? GainChanged;
    public event EventHandler<double>? FrequencyChanged;
    public event EventHandler<double>? MixChanged;

    // Module selector led clicked event
    public event EventHandler<bool>? ModuleSelectLedChanged;
    #endregion

    #region Public properties
    private string _caption = "EFFECTS";
    public string Caption {
        get { return _caption; }
        set { 
            _caption = value; 
            UpdateEffectsCaption();
        }
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

    public double EffectType {
        get { return knbEffectType.Value; }
        set { knbEffectType.Value = value;}
    }

    public double Gain {
        get { return knbGain.Value; }
        set { knbGain.Value = value; }
    }

    public double Frequency {
        get { return knbFrequency.Value; }
        set { knbFrequency.Value = value; }
    }

    public double Mix {
        get { return knbMix.Value; }
        set { knbMix.Value = value; }
    }

    public bool ModuleSelectLedOn{
        get { return ledSelect.LedOn; }
        set { ledSelect.LedOn = value; }
    }
    #endregion

    #region Constructor
    public Effects() {
        InitializeComponent();

        knbEffectType.ValueChanged += (o, e) => { EffectTypeChanged?.Invoke(this, (Enums.EffectType)knbEffectType.Value); UpdateEffectsCaption(); };
        knbGain.ValueChanged += (o, e) => GainChanged?.Invoke(this, knbGain.Value);
        knbFrequency.ValueChanged += (o, e) => FrequencyChanged?.Invoke(this, knbFrequency.Value);
        knbMix.ValueChanged += (o, e) => MixChanged?.Invoke(this, knbMix.Value);

        ledSelect.LedChanged += (o, e) => ModuleSelectLedChanged?.Invoke(this, ledSelect.LedOn);
    }
    #endregion

    private void UpdateEffectsCaption() {
        lblCaption.Text = " " + _caption.Trim() + " - " + ((Enums.EffectType)knbEffectType.Value).ToString().ToCapitalizedWithSpaces() + " ";
    }


}
