using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfUi.Modules;
public partial class VCO : UserControl {
    // Need events for Knob Value Changed




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

    public double Frequency {
        get { return knbFrequency.Value; }
        set { knbFrequency.Value = value;}
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


    public VCO() {
        InitializeComponent();
    }



}
