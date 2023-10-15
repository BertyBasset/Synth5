namespace WpfUi.Modules;

public partial class Keyboard : UserControl {
    #region Event declarations
    // Knob value changed events
    public event EventHandler<double>? GlideChanged;
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

    public double Glide {
        get { return knbGlide.Value; }
        set { knbGlide.Value = value;}
    }
    #endregion

    #region Constructor
    public Keyboard() {
        InitializeComponent();

        knbGlide.ValueChanged += (o, e) => GlideChanged?.Invoke(this, knbGlide.Value);
    }
    #endregion
}
