using UnityEngine;

namespace ITKamianets.Engine.Synth
{
	/// <summary>An ambience on loop and one-shot clips by id, with a master and an effects volume.</summary>
	public sealed class SoundBoard : MonoBehaviour
	{
		AudioSource _ambient;
		AudioSource _sfx;
		AudioClip[] _clips;
		float _ambientGain = 0.3f;

		public float MasterVolume { get; private set; } = 0.8f;
		public float SfxVolume { get; private set; } = 0.9f;

		/// <param name="ambient">Played on loop; may be null.</param>
		/// <param name="clips">The one-shots, played by index.</param>
		/// <param name="ambientGain">How loud the ambience is next to the master volume.</param>
		public void Init(AudioClip ambient, AudioClip[] clips, float ambientGain = 0.3f)
		{
			_clips = clips;
			_ambientGain = ambientGain;
			if (_ambient == null)
			{
				_ambient = gameObject.AddComponent<AudioSource>();
				_ambient.loop = true;
				_ambient.spatialBlend = 0f;
				_sfx = gameObject.AddComponent<AudioSource>();
				_sfx.spatialBlend = 0f;
			}
			_ambient.clip = ambient;
			if (ambient != null) _ambient.Play();
			SetVolumes(MasterVolume, SfxVolume);
		}

		public void SetVolumes(float master, float sfx)
		{
			MasterVolume = Mathf.Clamp01(master);
			SfxVolume = Mathf.Clamp01(sfx);
			if (_ambient != null) _ambient.volume = MasterVolume * _ambientGain;
		}

		public void Play(int id, float volume = 1f)
		{
			if (_sfx == null || _clips == null || id < 0 || id >= _clips.Length || MasterVolume * SfxVolume <= 0f) return;
			_sfx.PlayOneShot(_clips[id], MasterVolume * SfxVolume * Mathf.Clamp01(volume));
		}
	}
}
