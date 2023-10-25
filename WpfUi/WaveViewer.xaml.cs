//using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;

namespace WpfUi;

public partial class WaveViewer : Window {


    public WaveViewer(Synth.SynthEngine SynthEngine) {
        InitializeComponent();

        SynthEngine.GraphUpdated += (o, e) => {
            Draw(e);
        };



    }

    private void Draw(List<double> data) {
        cnvDisplay.Children.Clear();

        if (chkWave.IsChecked == true) {
            var polylineWave = DrawWave(data);
            cnvDisplay.Children.Add(polylineWave);
        }

    //    if (chkSpectrum.IsChecked == true) {
            var polylineSpectrum = DrawSpectrum(data);
            cnvDisplay.Children.Add(polylineSpectrum);
      //  }

    }

    private Polyline? DrawWave(List<double> data) {
        double canvasWidth = cnvDisplay.ActualWidth;
        double canvasHeight = cnvDisplay.ActualHeight;

        if (canvasWidth == 0 || canvasHeight == 0)
            return null;

        var polyline = new Polyline {
            Stroke = Brushes.Lime,
            StrokeThickness = 2
        };

        for (int i = 0; i < data.Count; i++) {
            double x = i * canvasWidth / data.Count * 2;
            double y = (data[i] * canvasHeight * 0.6 + canvasHeight / 2);

            polyline.Points.Add(new Point(x, y));
        }
        return polyline;
    }

    private Polyline? DrawSpectrum(List<double> data) {
        double canvasWidth = cnvDisplay.ActualWidth;
        double canvasHeight = cnvDisplay.ActualHeight;

        var s = GetSpectrum(data.ToArray());


        double maxCoeff = s.MaxBy(x => x);
        if (maxCoeff < .01)
            maxCoeff = 0.01;

        var polyline = new Polyline {
            Stroke = Brushes.DarkOrange,
            StrokeThickness = 2
        };

        for (int i = 0; i < s.Length; i++)
            polyline.Points.Add(new Point(i * 7, canvasHeight- (int)((s[i] * canvasHeight * .95 / maxCoeff) - 1)));
        return polyline;
    }


    private static double[] GetSpectrum(double[] signal) {
        // Must be power of 2 samples
        var extract = signal[0..512];
        // Uses nuget package from https://github.com/swharden/FftSharp
        // Shape the signal using a Hanning window
        var window = new FftSharp.Windows.Hanning();
        window.ApplyInPlace(extract);

        // Calculate the FFT as an array of complex numbers
        // Complex[] fftRaw = FftSharp.Transform.FFT(signal);

        // or get the magnitude (units²) or power (dB) as real numbers
        double[] fftMag = FftSharp.Transform.FFTmagnitude(extract);
        return fftMag;
    }

}