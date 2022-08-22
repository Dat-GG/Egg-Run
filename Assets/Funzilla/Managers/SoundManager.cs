using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEditor;
using Random = UnityEngine.Random;

namespace Funzilla
{
	internal class SoundManager : MonoBehaviour
	{
		private AudioSource _sfxPlayer;
		private AudioSource _musicPlayer;
		private AudioSource _fireExtinguisher;
		private string _currentMusicKey = string.Empty;

		[SerializeField] private List<AudioClip> clips;

		private readonly LinkedList<AudioSource> _pitchedSfxPlayers = new LinkedList<AudioSource>();
		private readonly Dictionary<string, AudioClip> _dict = new Dictionary<string, AudioClip>();

		private static SoundManager _instance;
		internal static SoundManager Instance {
			get
			{
				if (_instance) return _instance;
#if UNITY_EDITOR
				var prefab = AssetDatabase.LoadAssetAtPath<SoundManager>(
					"Assets/Funzilla/Managers/SoundManager.prefab");
				_instance = Instantiate(prefab);
				DontDestroyOnLoad(_instance.gameObject);
				return _instance;
#else
				return null;
#endif
			}
		}

		AudioClip LoadAudioClip(string key)
		{
			try
			{
				var clip = Resources.Load<AudioClip>("Sound/" + key);
				if (clip != null)
				{
					_dict.Add(key, clip);
				}
				return clip;
			}
			catch
			{
				return null;
			}
		}
		
		private AudioClip GetAudioClip(string key)
		{
			AudioClip clip;
			if (_dict.TryGetValue(key, out clip))
			{
				return clip;
			}

			clip = LoadAudioClip(key);
			return clip;
		}

		internal void PlaySfx(string key)
		{
			if (!Preference.Instance.SfxOn)
			{
				return;
			}
			var clip = GetAudioClip(key);
			if (clip)
			{
				_sfxPlayer.PlayOneShot(clip);
			}
		}

		internal void PlaySfx(string key, float pitch)
		{
			if (!Preference.Instance.SfxOn)
			{
				return;
			}
			var clip = GetAudioClip(key);
			if (clip == null)
			{
				return;
			}
			AudioSource player;
			if (_pitchedSfxPlayers.First == null)
			{
				player = gameObject.AddComponent<AudioSource>();
			}
			else
			{
				player = _pitchedSfxPlayers.First.Value;
				_pitchedSfxPlayers.RemoveFirst();
			}
			player.pitch = pitch;
			player.PlayOneShot(clip);
			DOVirtual.DelayedCall(clip.length, () => { _pitchedSfxPlayers.AddLast(player); });
		}

		internal void PlayMusic(string key, bool loop = false, float delay = 0)
		{
			if (!Preference.Instance.MusicOn)
			{
				return;
			}

			if (_currentMusicKey.Equals(key))
			{
				return;
			}

			var clip = GetAudioClip(key);
			if (!clip)
			{
				return;
			}
			_currentMusicKey = key;

			// Play music logic here
			_musicPlayer.Stop();
			_musicPlayer.clip = clip;
			_musicPlayer.PlayDelayed(delay);
			_musicPlayer.loop = loop;
			_musicPlayer.Play();
		}
		
		internal void PlayFireExtinguisher(string key, bool loop = false, float delay = 0)
		{
			if (!Preference.Instance.MusicOn)
			{
				return;
			}
			
			var clip = GetAudioClip(key);
			if (!clip)
			{
				return;
			}

			// Play music logic here
			_fireExtinguisher.Stop();
			_fireExtinguisher.clip = clip;
			_fireExtinguisher.PlayDelayed(delay);
			_fireExtinguisher.loop = loop;
			_fireExtinguisher.Play();
		}
		internal void StopFireExtinguisher()
		{
			_fireExtinguisher.Stop();
		}

		private bool MusicPlaying => !string.IsNullOrEmpty(_currentMusicKey);

		internal bool IsMusicPlaying(string music)
		{
			return MusicPlaying && music == _currentMusicKey;
		}

		internal void StopMusic()
		{
			_currentMusicKey = string.Empty;
			_musicPlayer.Stop();
		}

		internal void ResumeMusic()
		{
			if (!Preference.Instance.MusicOn ||
				string.IsNullOrEmpty(_currentMusicKey) ||
				_musicPlayer.isPlaying)
			{
				return;
			}
			_musicPlayer.Play();
		}

		internal void SetLoopMusic(bool value)
		{
			_musicPlayer.loop = value;
		}

		private float timeWalking = 0.45f;
		readonly string[] walkingSounds = new string[2] { "walk_1", "walk_2"};
		internal void PlayWalking()
		{
			if (notWalking) return;
			if(Time.realtimeSinceStartup - timeWalking > 0.45f)
			{
				PlaySfx(walkingSounds[Random.Range(0, walkingSounds.Length)]);
				timeWalking = Time.realtimeSinceStartup;
			}
		}

		private bool notWalking = false;

		internal void PauseWalking()
		{
			notWalking = true;
		}
		internal void ResumeWalking()
		{
			notWalking = false;
		}

		private void Awake()
		{
			_instance = this;
			var audioSources = GetComponents<AudioSource>();
			if (audioSources.Length < 3)
			{
				Array.Resize(ref audioSources, 3);
				for (var i = 0; i < 3; i++)
				{
					audioSources[i] = gameObject.AddComponent<AudioSource>();
				}
			}
			_sfxPlayer = audioSources[0];
			_musicPlayer = audioSources[1];
			_fireExtinguisher = audioSources[2];

			if (clips == null) return;
			foreach (var clip in clips)
			{
				_dict.Add(clip.name, clip);
			}
		}
	}
}