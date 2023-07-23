﻿namespace Synth.Keyboard.Controllers;

public class ModWheel : iModule {
    private readonly Midi midi = Midi.Instance;

    public event EventHandler<MidiWheelEventArgs>? ModWheelChanged;

    public ModWheel() {
        // ModWheel 0 - 127
        midi.ModWheelChanged += (o, e)
            => {
                if (_midichannel == null || _midichannel == e.MidiChannel) { 
                    Value = e.Value / 127f;
                    ModWheelChanged?.Invoke(this, e);
                }
            };
    }

    private int? _midichannel = 1;
    public int? MidiChannel {
        get { return _midichannel; }
        set {
            if (value < 1 || value > 16)
                throw new ArgumentOutOfRangeException("Midi Channel must be 1-16, or left as NULL");
            _midichannel = value;
        }
    }

    public double Value { get; set; }

    public void Tick(double TimeIncrement) {
        // No need to do anything. Value gets set by Midi event
    }
}

