using System.Windows.Controls;
using Synth.Keyboard.Utils;

namespace WpfUi;

public partial class Controllers : Window {

    const int MAXROWS = 12;

    List<ControlChangeParameter> _cc = ControlChangeParameter.GetList();

    public Controllers() {
        InitializeComponent();

        AddComboBoxRow();

        btnSave.Click += (o, e) => Save();
        btnCancel.Click += (o, e) => this.Close();

    }


    void Save() {

        this.Close();
    }

    private void AddRow_Click(object sender, RoutedEventArgs e) {
        AddComboBoxRow();
    }

    private void RemoveRow_Click(object sender, RoutedEventArgs e) {
        if (stackPanel.Children.Count <= 2)
            return;
        
        Button removeButton = (Button)sender;
        int index = (int)removeButton.Tag;

        // Remove the row
        stackPanel.Children.RemoveAt(index);

        // Update the remaining row indices
        for (int i = index; i < stackPanel.Children.Count; i++) {
            if (stackPanel.Children[i] is StackPanel row) {
                Button rowRemoveButton = (Button)row.Children[2];
                rowRemoveButton.Tag = i;
            }
        }

        if (stackPanel.Children.Count <= MAXROWS)
            btnAdd.IsEnabled = true;
    }

    private void AddComboBoxRow() {
        StackPanel row = new StackPanel { Orientation = Orientation.Horizontal };

        ComboBox comboBox1 = new ComboBox() { Width = 200 , Margin = new Thickness(10, 2, 0, 0), ItemsSource=_cc, DisplayMemberPath="Display", SelectedValuePath="ControllerId"};
        ComboBox comboBox2 = new ComboBox() { Width = 200, Margin = new Thickness(2, 2, 0, 0) };
        Button removeButton = new Button { Content = "X" , Width=20, Margin = new Thickness(2, 2, 0, 0) };

        removeButton.Click += RemoveRow_Click;
        removeButton.Tag = stackPanel.Children.Count; // Set the index

        row.Children.Add(comboBox1);
        row.Children.Add(comboBox2);
        row.Children.Add(removeButton);

        stackPanel.Children.Insert(stackPanel.Children.Count-1, row);


        if (stackPanel.Children.Count > MAXROWS)
            btnAdd.IsEnabled = false;


    }
}

