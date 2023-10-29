using System.Linq;
using System.Windows.Controls;
using Synth.Keyboard.Utils;
using WpfUi.MidiControllers;

namespace WpfUi;

public partial class Controllers : Window {

    const int MAXROWS = 13;
    readonly List<ControlChangeParameter> _cc = ControlChangeParameter.GetList();
    readonly List<ControlKnob> _knobs = ControlKnob.GetList();

    readonly List<ControlKnobControllerMapping> _controllermapping;

    public Controllers(List<ControlKnobControllerMapping> controllermapping) {
        _controllermapping = controllermapping;
        InitializeComponent();
        LoadComboBoxes();

        btnSave.Click += (o, e) => Save();
        btnCancel.Click += (o, e) => this.Close();

        btnAdd.Visibility = Visibility.Visible;
    }

    void LoadComboBoxes() {
        foreach (var m in _controllermapping) {
            AddComboBoxRow(m.MidiControllerID, m.ControlKnobID);
        }
        if(_controllermapping.Count == 0)
            AddComboBoxRow();           // Add blank

    }

    void Save() {
        switch(Validate()) {
            case ValidateResult.ValueNotSelected:
                MessageBox.Show("Please select the mapping parameters from the dropdowns", "Validate", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            case ValidateResult.DuplicateControllerId:
                MessageBox.Show("You have selected duplicate Midi Controllers", "Validate", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            case ValidateResult.DuplicateKnobId:
                MessageBox.Show("You have selected duplicate Synth Controllers", "Validate", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            default: break;
        }

        // ** Populate __controllermapping
        _controllermapping.Clear();
        foreach (StackPanel row in stackPanel.Children.OfType<StackPanel>()) {
            var c = row.Children.OfType<ComboBox>().ToList();
            _controllermapping.Add(new ControlKnobControllerMapping() { MidiControllerID = (int)c[0].SelectedValue, ControlKnobID = (int)c[1].SelectedValue });
        }

        // ** save _controllermapping
        ControlKnobControllerMapping.Save(_controllermapping);
        this.Close();
    }


    enum ValidateResult { 
        Validated=0,
        ValueNotSelected,
        DuplicateControllerId,
        DuplicateKnobId
    }

    ValidateResult Validate() {
        List<int> controllerids = new();
        List<int> knobids = new();


        foreach (StackPanel row in stackPanel.Children.OfType<StackPanel>()) {
            int i = 0;
            foreach (ComboBox comboBox in row.Children.OfType<ComboBox>()) {
                if (comboBox.SelectedIndex == 0)
                    return ValidateResult.ValueNotSelected;
                
                // Esnure IDs are only selected once
                if (i == 0) {
                    // Controller Combo
                    if (controllerids.Contains((int)comboBox.SelectedValue))
                        return ValidateResult.DuplicateControllerId;
                    controllerids.Add((int)comboBox.SelectedValue);
                } else {
                    // Knob Combo
                    if (knobids.Contains((int)comboBox.SelectedValue))
                        return ValidateResult.DuplicateKnobId;
                    knobids.Add((int)comboBox.SelectedValue);
                }
                i++;
            }
        }

        return ValidateResult.Validated;

    }


    private void AddRow_Click(object sender, RoutedEventArgs e) {
        AddComboBoxRow();
    }

    private void RemoveRow_Click(object sender, RoutedEventArgs e) {
        if (stackPanel.Children.Count <= 1)
            return;
        
        Button removeButton = (Button)sender;
        int index = (int)removeButton.Tag;

        // Remove the row
        stackPanel.Children.RemoveAt(index);

        // Update the remaining row indices
        for (int i = 0; i < stackPanel.Children.Count; i++) {
            if (stackPanel.Children[i] is StackPanel row) {
                Button rowRemoveButton = (Button)row.Children[2];
                rowRemoveButton.Tag = i;
            }
        }

        if (stackPanel.Children.Count <= MAXROWS-1)
            btnAdd.IsEnabled = true;
    }

    private void AddComboBoxRow(int? ControllerID = null, int? KnobID = null) {
        if (stackPanel.Children.Count == MAXROWS-1)
            return;

        StackPanel row = new() { Orientation = Orientation.Horizontal };

        ComboBox comboBox1 = new() { Width = 196 , Margin = new Thickness(10, 2, 0, 0), ItemsSource=_cc, DisplayMemberPath="Display", SelectedValuePath="ControllerId", SelectedIndex=0};
        if (ControllerID != null)
            comboBox1.SelectedValue = ControllerID;
        ComboBox comboBox2 = new() { Width = 196, Margin = new Thickness(2, 2, 0, 0), ItemsSource=_knobs, DisplayMemberPath="Description", SelectedValuePath="ID", SelectedIndex=0 };
        if(KnobID != null)
            comboBox2.SelectedValue = KnobID;
        
            
        Button removeButton = new() { Content = "X" , Width=20, Margin = new Thickness(15, 2, 0, 0) };

        removeButton.Click += RemoveRow_Click;
        removeButton.Tag = stackPanel.Children.Count; // Set the index

        row.Children.Add(comboBox1);
        row.Children.Add(comboBox2);
        row.Children.Add(removeButton);

        stackPanel.Children.Insert(stackPanel.Children.Count, row);

        if (stackPanel.Children.Count >= MAXROWS - 1)
            btnAdd.IsEnabled = false;
    }
}

