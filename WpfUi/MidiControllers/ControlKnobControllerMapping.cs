using Microsoft.VisualBasic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WpfUi.MidiControllers;

public class ControlKnobControllerMapping {
    public int MidiControllerID { get; set; } = 0;
    public int ControlKnobID { get; set; } = 0;

    public static void Save(List<ControlKnobControllerMapping> mapping) {
        var json = JsonSerializer.Serialize<List<ControlKnobControllerMapping>>(mapping);
        System.IO.File.WriteAllText($"{WpfUi.Utils.Constants.SAVE_LOCATION}{WpfUi.Utils.Constants.MIDI_TO_KNOB_MAPPING_FILE}", json);
    }

    public static List<ControlKnobControllerMapping> Load() {
        if (!System.IO.File.Exists($"{WpfUi.Utils.Constants.SAVE_LOCATION}{WpfUi.Utils.Constants.MIDI_TO_KNOB_MAPPING_FILE}"))
            return new();

        var json = System.IO.File.ReadAllText($"{WpfUi.Utils.Constants.SAVE_LOCATION}{WpfUi.Utils.Constants.MIDI_TO_KNOB_MAPPING_FILE}");
        return JsonSerializer.Deserialize<List<ControlKnobControllerMapping>>(json) ?? new();
    }
}




