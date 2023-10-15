using Synth.Core;

namespace Synth;

public class Patch {
    #region Events
    public event EventHandler<bool>? Lfo1StateChanged;
    public event EventHandler<bool>? Lfo2StateChanged;
    public event EventHandler<EventArgs<(int, bool)>>? KeyChanged;          // Return tuple - key number, state

    
    public event EventHandler<MidiControllerEventArgs>? MidiControllerChanged;
    #endregion

    #region Module Declaration
    readonly Synth.SynthEngine engine = new ();
    const int NUM_VOICES = 5;
    readonly PolyKeyboard polyKbd = new(NUM_VOICES);

    // Voice Level Modules
    readonly List<Voice>      voices   = Enumerable.Range(0, NUM_VOICES).Select(i => new Voice()).ToList();        //
    readonly List<VCO>        vco1     = Enumerable.Range(0, NUM_VOICES).Select(i => new VCO()).ToList();        // { WaveFormType = VCOWaveformType.SuperSaw }).ToList();
    readonly List<VCO>        vco2     = Enumerable.Range(0, NUM_VOICES).Select(i => new VCO()).ToList();        // { WaveFormType = VCOWaveformType.Square}).ToList();
    readonly List<VCO>        vco3     = Enumerable.Range(0, NUM_VOICES).Select(i => new VCO()).ToList();        // { WaveFormType = VCOWaveformType.Saw }).ToList();
    readonly List<Noise>      noise    = Enumerable.Range(0, NUM_VOICES).Select(i => new Noise()).ToList();
    readonly List<Mixer>      vcoMixer = Enumerable.Range(0, NUM_VOICES).Select(i => new Mixer()).ToList();
    readonly List<BitCrusher> crusher  = Enumerable.Range(0, NUM_VOICES).Select(i => new BitCrusher()).ToList();
    readonly List<EnvGen>     envVcf   = Enumerable.Range(0, NUM_VOICES).Select(i => new EnvGen() ).ToList();
    readonly List<VCF>        vcf      = Enumerable.Range(0, NUM_VOICES).Select(i => new VCF() ).ToList();
    readonly List<EnvGen>     envVca   = Enumerable.Range(0, NUM_VOICES).Select(i => new EnvGen() ).ToList();
    readonly List<VCA>        vca      = Enumerable.Range(0, NUM_VOICES).Select(i => new VCA()).ToList();
    readonly List<AudioOut>   voiceOut = Enumerable.Range(0, NUM_VOICES).Select(i => new AudioOut()).ToList();
    // These 2 not patched to anything but will use mod matrix
    readonly List<EnvGen>     env1     = Enumerable.Range(0, NUM_VOICES).Select(i => new EnvGen()).ToList();
    readonly List<EnvGen>     env2     = Enumerable.Range(0, NUM_VOICES).Select(i => new EnvGen()).ToList();


    // Synth Level Modules
    readonly MidiControllers mc = new();
    readonly Mixer           voiceMixer = new ();
    readonly Effects         effects = new () { EffectType = Enums.EffectType.FeedbackComb, Mix = -.7, Param1 = .6, Param2 = .6 };
    readonly AudioOut        audioOut = new();
    readonly LFO             lfo1 = new ();          // Lfos are module level because we want same waveform for all voices
    readonly LFO             lfo2 = new();
    #endregion

    #region Initiallse 2 - Patch modules together and add them either to a separate voice, or to the Synth Engine
    public Patch() {
        Init();
    }

    void Init() {
        polyKbd.DebugEvent += (o, e) => System.Console.WriteLine(e.Value);
        lfo1.ClockTick += (o, e) => Lfo1StateChanged?.Invoke(o, e);
        lfo2.ClockTick += (o, e) => Lfo2StateChanged?.Invoke(o, e);
        polyKbd.KeyChanged += (o, e) => KeyChanged?.Invoke(this, e);
        mc.ControllerValueChanged += (o, e) => MidiControllerChanged?.Invoke(this, e);

        // Patch Voice level modules together
        for (int i = 0; i < NUM_VOICES; i++) {
            var voice = new Voice();
            voices.Add(voice);

            voice.Modules.AddRange(new iModule[] {
                polyKbd.keys[i].MonoKeyboard,
                vco1[i],
                vco2[i],
                vco3[i],
                noise[i],
                vcoMixer[i],
                crusher[i],
                envVcf[i],
                vcf[i],
                envVca[i],
                vca[i],
                voiceOut[i],
                env1[i],
                env2[i]
            });

            engine.Modules.Add(voice);

            // Hook modules together - Source and Modulator to iModules
            vco1[i].Frequency.Keyboard = polyKbd.keys[i].MonoKeyboard;
            vco2[i].Frequency.Keyboard = polyKbd.keys[i].MonoKeyboard;
            vco2[i].Frequency.FineTune = -.01;
            vco3[i].Frequency.Keyboard = polyKbd.keys[i].MonoKeyboard;
            vco3[i].Frequency.FineTune = .01;
            vco3[i].Frequency.Octave = -1;

            vcoMixer[i].Sources.Add(vco1[i]);
            vcoMixer[i].Sources.Add(vco2[i]);
            vcoMixer[i].Sources.Add(vco3[i]);
            vcoMixer[i].Sources.Add(noise[i]);
            vcoMixer[i].Levels[0] = 1;   // <<<<<<<<<<<<<<<<
            vcoMixer[i].Levels[1] = 1;
            vcoMixer[i].Levels[2] = 1;
            crusher[i].Source = vcoMixer[i];
            envVcf[i].Keyboard = polyKbd.keys[i].MonoKeyboard;
            vcf[i].Modulator = envVcf[i];
            vcf[i].Source = crusher[i];
            envVca[i].Keyboard = polyKbd.keys[i].MonoKeyboard;
            vca[i].Source = vcf[i];
            vca[i].Modulator = envVca[i];
            voiceOut[i].Source = vca[i];
            voiceMixer.Sources.Add(voiceOut[i]);
            voiceMixer.Levels[i] = 1;
        }

        // Patch Synth level modules together
        effects.Source = voiceMixer;
        audioOut.Source = effects;

        engine.Modules.Add(mc);
        engine.Modules.Add(voiceMixer);
        engine.Modules.Add(effects);
        engine.Modules.Add(audioOut);
        engine.Modules.Add(lfo1);
        engine.Modules.Add(lfo2);


    }
    #endregion

    #region Module Properties
    #region Oscillators
    public int Vco1Octave {
        set => vco1.ForEach(vco => vco.Frequency.Octave = value);
        get => vco1.FirstOrDefault()!.Frequency.Octave;
    }
    public double Vco1FineTune { 
        set => vco1.ForEach (vco => vco.Frequency.FineTune = value);
        get => vco1.FirstOrDefault()!.Frequency.FineTune;
    }
    public double Vco1PulseWidth {
        set => vco1.ForEach(vco => vco.Duty.Value = value);
        get => vco1.FirstOrDefault()!.Duty.Value;
    }
    public VCOWaveformType Vco1WaveFormType {
        set => vco1.ForEach(vco => vco.WaveFormType = value);
        get => vco1.FirstOrDefault()!.WaveFormType;
    }
    public int Vco2Octave {
        set => vco2.ForEach(vco => vco.Frequency.Octave = value);
        get => vco2.FirstOrDefault()!.Frequency.Octave;
    }
    public double Vco2FineTune {
        set => vco2.ForEach(vco => vco.Frequency.FineTune = value);
        get => vco2.FirstOrDefault()!.Frequency.FineTune;
    }
    public double Vco2PulseWidth {
        set => vco2.ForEach(vco => vco.Duty.Value = value);
        get => vco2.FirstOrDefault()!.Duty.Value;
    }
    public VCOWaveformType Vco2WaveFormType {
        set => vco2.ForEach(vco => vco.WaveFormType = value);
        get => vco2.FirstOrDefault()!.WaveFormType;
    }
    public int Vco3Octave {
        set => vco3.ForEach(vco => vco.Frequency.Octave = value);
        get => vco3.FirstOrDefault()!.Frequency.Octave;
    }
    public double Vco3FineTune {
        set => vco3.ForEach(vco => vco.Frequency.FineTune = value);
        get => vco3.FirstOrDefault()!.Frequency.FineTune;
    }
    public double Vco3PulseWidth {
        set => vco3.ForEach(vco => vco.Duty.Value = value);
        get => vco3.FirstOrDefault()!.Duty.Value;
    }
    public VCOWaveformType Vco3WaveFormType {
        set => vco3.ForEach(vco => vco.WaveFormType = value);
        get => vco3.FirstOrDefault()!.WaveFormType;
    }
    #endregion

    #region Oscillator/Noise Mixer
    private enum MixerSource { 
        Vco1,
        Vco2,
        Vco3,
        Noise
    }
    public double MixerVco1Level { 
        set => vcoMixer.ForEach(mixer => mixer.Levels[(int)MixerSource.Vco1] = value);
        get => vcoMixer.FirstOrDefault()!.Levels[(int)MixerSource.Vco1];
    }
    public double MixerVco2Level {
        set => vcoMixer.ForEach(mixer => mixer.Levels[(int)MixerSource.Vco2] = value);
        get => vcoMixer.FirstOrDefault()!.Levels[(int)MixerSource.Vco2];

    }
    public double MixerVco3Level {
        set => vcoMixer.ForEach(mixer => mixer.Levels[(int)MixerSource.Vco3] = value);
        get => vcoMixer.FirstOrDefault()!.Levels[(int)MixerSource.Vco3];
    }
    public double MixerNoiseLevel {
        set => vcoMixer.ForEach(mixer => mixer.Levels[(int)MixerSource.Noise] = value);
        get => vcoMixer.FirstOrDefault()!.Levels[(int)MixerSource.Noise];
    }
    #endregion

    #region BitCrush + Glide
    public double BitCrushSampleRate {
        set => crusher.ForEach(crush => crush.SampleRate = value);
        get => crusher.FirstOrDefault()!.SampleRate;
    }

    public double BitCrushResolution {
        set => crusher.ForEach(crush => crush.Resolution = value);
        get => crusher.FirstOrDefault()!.Resolution;
    }

    public double Glide {
        set => polyKbd.keys.ForEach(key => key.MonoKeyboard.Glide = value);
        get => polyKbd.keys.FirstOrDefault()!.MonoKeyboard.Glide;
    }

    #endregion

    #region VCF + VCF Env
    public Synth.Enums.FilterType VcfFilterType {
        set => vcf.ForEach(vcf => vcf.FilterType = value);
        get => vcf.FirstOrDefault()!.FilterType;
    }

    public double VcfCutoff {
        set => vcf.ForEach(vcf => vcf.Cutoff = value);
        get => vcf.FirstOrDefault()!.Cutoff;
    }

    public double VcfEnvelopeAmount {
        set => vcf.ForEach(vcf => vcf.ModAmount = value);
        get => vcf.FirstOrDefault()!.ModAmount;
    }

    public double VcfRippleFactor {
        set => vcf.ForEach(vcf => vcf.RippleFactor = value);
        get => vcf.FirstOrDefault()!.RippleFactor;
    }

    public double VcfResonance {
        set => vcf.ForEach(vcf => vcf.Resonance = value);
        get => vcf.FirstOrDefault()!.Resonance;
    }

    public double VcfBandwidth {
        set => vcf.ForEach(vcf => vcf.Bandwidth = value);
        get => vcf.FirstOrDefault()!.Bandwidth;
    }

    // General Envelope Env1
    public double Env1Attack {
        set => env1.ForEach(vcf => vcf.Attack = value);
        get => env1.FirstOrDefault()!.Attack;
    }

    public double Env1Decay {
        set => env1.ForEach(vcf => vcf.Decay = value);
        get => env1.FirstOrDefault()!.Decay;
    }

    public double Env1Sustain {
        set => env1.ForEach(vcf => vcf.Sustain = value);
        get => env1.FirstOrDefault()!.Sustain;
    }

    public double Env1Release {
        set => env1.ForEach(vcf => vcf.Release = value);
        get => env1.FirstOrDefault()!.Release;
    }

    // General Envelope Env2
    public double Env2Attack {
        set => env2.ForEach(vcf => vcf.Attack = value);
        get => env2.FirstOrDefault()!.Attack;
    }

    public double Env2Decay {
        set => env2.ForEach(vcf => vcf.Decay = value);
        get => env2.FirstOrDefault()!.Decay;
    }

    public double Env2Sustain {
        set => env2.ForEach(vcf => vcf.Sustain = value);
        get => env2.FirstOrDefault()!.Sustain;
    }

    public double Env2Release {
        set => env2.ForEach(vcf => vcf.Release = value);
        get => env2.FirstOrDefault()!.Release;
    }

    // **** VCF Envelope 
    public double VcfEnvAttack {
        set => envVcf.ForEach(vcf => vcf.Attack = value);
        get => envVcf.FirstOrDefault()!.Attack;
    }

    public double VcfEnvDecay {
        set => envVcf.ForEach(vcf => vcf.Decay = value);
        get => envVcf.FirstOrDefault()!.Decay;
    }

    public double VcfEnvSustain {
        set => envVcf.ForEach(vcf => vcf.Sustain = value);
        get => envVcf.FirstOrDefault()!.Sustain;
    }

    public double VcfEnvRelease {
        set => envVcf.ForEach(vcf => vcf.Release = value);
        get => envVcf.FirstOrDefault()!.Release;
    }
    #endregion

    #region VCA + VCA Env
    public double VcaEnvAttack {
        set => envVca.ForEach(vca => vca.Attack = value);
        get => envVca.FirstOrDefault()!.Attack;
    }

    public double VcaEnvDecay {
        set => envVca.ForEach(vca => vca.Decay = value);
        get => envVca.FirstOrDefault()!.Decay;
    }

    public double VcaEnvSustain {
        set => envVca.ForEach(vca => vca.Sustain = value);
        get => envVca.FirstOrDefault()!.Sustain;
    }

    public double VcaEnvRelease {
        set => envVca.ForEach(vca => vca.Release = value);
        get => envVca.FirstOrDefault()!.Release;
    }

    #endregion

    #region Effects - This is module not voice level, so no needs for foreach
    public Enums.EffectType EffectType {
        set => effects.EffectType = (Enums.EffectType)value;
        get => effects.EffectType;
    }

    public double EffectParam1 {
        set => effects.Param1 = value;
        get => effects.Param1;
    }

    public double EffectParam2 {
        set => effects.Param2 = value;
        get => effects.Param2;
    }

    public double EffectMix {
        set => effects.Mix = value;
        get => effects.Mix;

    }

    public int? MidiChannel {
        set { polyKbd.MidiChannel = value; }
        get => polyKbd.MidiChannel;
    }

    public SynthEngine SynthEngine { 
        get => engine;
    }

    public double Lfo1Frequency {
        set => lfo1.Frequency = value;
        get => lfo1.Frequency;
    }

    public LFOWaveformType Lfo1WaveformType { 
        set => lfo1.Shape.Type = value;
        get => lfo1.Shape.Type;
    }

    public double Lfo1Delay {
        set => lfo1.Delay = value;
        get => lfo1.Delay;
    }

    public double Lfo2Frequency {
        set => lfo2.Frequency = value;
        get => lfo2.Frequency;
    }

    public LFOWaveformType Lfo2WaveformType {
        set => lfo2.Shape.Type = value;
        get => lfo2.Shape.Type;
    }

    public double Lfo2Delay {
        set => lfo2.Delay = value;
        get => lfo2.Delay;
    }



    #endregion
    #endregion
}