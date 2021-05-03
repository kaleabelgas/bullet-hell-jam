using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup SFXMixerGroup;

	public AudioMixerGroup musicMixerGroup;

	public Sound[] sounds;

	public Sound[] tracks;

	private bool stopAudio = false;
	private bool isplaying;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = SFXMixerGroup;
		}

		foreach(Sound m in tracks)
        {
			m.source = gameObject.AddComponent<AudioSource>();
			m.source.clip = m.clip;
			m.source.loop = m.loop;

			m.source.outputAudioMixerGroup = musicMixerGroup;
        }
	}

    public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}
	public void Stop(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Stop();
	}

	public IEnumerator StartPlaylist()
    {
		stopAudio = false;
		Sound[] _tracks = tracks;
		for (int i = 0; i < _tracks.Length; i++)
		{
			_tracks[i].source.Play();
			while (_tracks[i].source.isPlaying)
			{
				isplaying = _tracks[i].source.isPlaying;
				//Debug.Log("playing");
				if (stopAudio)
				{
					Debug.Log("Stopped");
					isplaying = false;
					_tracks[i].source.Stop();
					yield break;
				}
				yield return null;
			}
		}
	}

	public void PlayMusic()
    {
		StartCoroutine(StartPlaylist());
    }

	public void StopMusic()
    {
		stopAudio = true;
    }

}
