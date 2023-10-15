//#define test          // Uncomment to show TestWindow


using System.Windows;

namespace WpfUi {
    public partial class App : Application {

        private void Application_Startup(object sender, StartupEventArgs e) {

#if test            
            TestWindow wnd = new() {
                Title = "My App"
            };
            wnd.Show();
#else

            SynthUI wnd = new() {
                Title = "Synth"
            };
            wnd.Show();
#endif
        }
    }
}
