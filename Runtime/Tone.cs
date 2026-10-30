using System;
using UnityEngine;

namespace ITKamianets.Engine.Synth
{
	/// <summary>Builds short mono clips from a function of time, so a game needs no audio files.</summary>
	public static class Tone
	{
		public const int Rate = 22050;

		/// <summary>A clip of <paramref name="seconds"/> whose sample at time t (seconds) is <paramref name="wave"/>(t), clamped to -1..1.</summary>
		public static AudioClip Build(string name, float seconds, Func<float, float> wave)
		{
			var samples = Mathf.CeilToInt(seconds * Rate);
			var data = new float[samples];
			for (var i = 0; i < samples; i++) data[i] = Mathf.Clamp(wave(i / (float)Rate), -1f, 1f);
			var clip = AudioClip.Create(name, samples, 1, Rate, false);
			clip.SetData(data, 0);
			return clip;
		}

		/// <summary>0 to 1 and back: a fast attack, then a decay over most of the clip's length.</summary>
		public static float Envelope(float t, float length, float attack = 0.004f) => Mathf.Clamp01(t / attack) * Mathf.Clamp01((length - t) / Mathf.Max(0.02f, length * 0.7f));

		/// <summary>White noise in -1..1 from the given generator (seed it for a repeatable sound).</summary>
		public static float Noise(System.Random random) => (float)(random.NextDouble() * 2.0 - 1.0);
	}
}
