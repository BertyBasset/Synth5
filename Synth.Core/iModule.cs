namespace Synth.Core;

// The reason for Synth.Core is so that collections inherting from iModule can live in their own project
// At the moment this measn Synth.Keyboard and Synth.Modules

public interface iModule {
    // Each module needs to provide a Value - nominally between -1 and +1
    public double Value { get; }
    // When Tick occurs, each module must update its Value to reflect what i will be in TimeIncrement
    public void Tick(double TimeIncrement);
}

