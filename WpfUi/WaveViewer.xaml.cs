//using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;
using NAudio.Dmo.Effect;

namespace WpfUi;

public partial class WaveViewer : Window {


    public WaveViewer(Synth.SynthEngine SynthEngine) {
        InitializeComponent();

        SynthEngine.GraphUpdated += (o, e) => {
            Draw(e);
        };



    }



    Polyline lineWave;
    Polyline lineSpectrum;

    private void Draw(List<double> data) {


        ClearContent();

        DrawGrid();

        if (chkWave.IsChecked == true) {
            DrawWave(data);
            cnvDisplay.Children.Add(lineWave);
        }

        if (chkSpectrum.IsChecked == true) {
            DrawSpectrum(data);
            cnvDisplay.Children.Add(lineSpectrum);
        }
    }

    void ClearContent() {
        var polylinesToRemove = cnvDisplay.Children.OfType<Polyline>().ToList();
        foreach (var polyline in polylinesToRemove) {
            cnvDisplay.Children.Remove(polyline);
        }
        var lines = cnvDisplay.Children.OfType<Line>().ToList();
        foreach (var line in lines) {
            cnvDisplay.Children.Remove(line);
        }
        var labels = cnvDisplay.Children.OfType<TextBlock>().ToList();
        foreach (var l in labels) {
            cnvDisplay.Children.Remove(l);
        }
    }

    private void DrawGrid() {
        double height = cnvDisplay.ActualHeight;
        double width = cnvDisplay.ActualWidth;


        const int COLUMNS = 9;
        const int ROWS = 6;

        double cellWidth = (width - width/10) / COLUMNS;


        for (int i = 0; i <= COLUMNS; i++) {
            Line verticalLine = new Line {
                X1 = i * cellWidth,
                Y1 = 0,
                X2 = i * cellWidth,
                Y2 = height,
                Stroke = Brushes.LightGreen,
                StrokeThickness = .4
            };
            cnvDisplay.Children.Add(verticalLine);

            if (chkSpectrum.IsChecked == true) {
                var t = new TextBlock {
                    Text = $"{i * 500}Hz",
                    Foreground = Brushes.DarkOrange,
                    FontSize = 12
                };
                Canvas.SetLeft(t, i * cellWidth + 5);
                Canvas.SetTop(t, 5);
                cnvDisplay.Children.Add(t);
            }
        }

        double cellHeight = height / ROWS;
        for (int i = 1; i < ROWS; i++) {
            Line horizontalLine = new Line {
                X1 = 0,
                Y1 = i * cellHeight,
                X2 = width,
                Y2 = i * cellHeight,
                Stroke = Brushes.LightGreen,
                StrokeThickness = .4
            };
            cnvDisplay.Children.Add(horizontalLine);
        }
    }


    private void DrawWave(List<double> data) {
        double canvasWidth = cnvDisplay.ActualWidth;
        double canvasHeight = cnvDisplay.ActualHeight;

        if (canvasWidth == 0 || canvasHeight == 0)
            return;

        lineWave = new Polyline {
            Stroke = Brushes.Lime,
            StrokeThickness = 2
        };

        for (int i = 0; i < data.Count; i++) {
            double x = i * canvasWidth / data.Count * 2;
            double y = (data[i] * canvasHeight * 0.4 + canvasHeight / 2);

            lineWave.Points.Add(new Point(x, y));
        }
}

    private void DrawSpectrum(List<double> data) {
        double canvasWidth = cnvDisplay.ActualWidth;
        double canvasHeight = cnvDisplay.ActualHeight;

        var s = GetSpectrum(data.ToArray());


        double maxCoeff = s.MaxBy(x => x);
        if (maxCoeff < .01)
            maxCoeff = 0.01;

        lineSpectrum = new Polyline {
            Stroke = Brushes.DarkOrange,
            StrokeThickness = 2
        };

        for (int i = 0; i < s.Length; i++)
            lineSpectrum.Points.Add(new Point(i * 7, canvasHeight- (int)((s[i] * canvasHeight * .95 / maxCoeff) - 1)));
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