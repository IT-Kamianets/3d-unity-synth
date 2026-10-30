# 3d-unity-synth

Procedural sound for a game on the IT Kamianets 3D Engine, so it ships with no audio files.

### Logic

- `Tone.Build(name, seconds, wave)`: a mono 22.05 kHz clip from a function of time, clamped to -1..1. `Tone.Envelope(t, length, attack)` is the usual attack and decay shape; `Tone.Noise(random)` is white noise in -1..1.
- `SoundBoard`: a component that plays an ambience on loop and one-shot clips by integer id. `SetVolumes(master, sfx)` keeps the two volumes (the ambience follows the master one, scaled by `ambientGain`). `Play(id, volume)` does nothing while muted, so a muted game behaves exactly like an unmuted one apart from the sound.

A game keeps its own list of sounds (an enum) and its own recipes; this package only has the parts they all repeat.

```csharp
board.Init(Tone.Build("ambience", 6f, t => ...), clips, ambientGain: 0.3f);
board.Play((int)Sfx.Click, 0.5f);
```

### Status

v0.1. Used by Temnorid Escape, Detective and Master.
