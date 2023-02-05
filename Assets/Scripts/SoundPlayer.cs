using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
  [SerializeField]
  [Range(0.0f, 1.0f)]
  float volume = 1.0f;

  [SerializeField]
  List<AudioClip> clips;

  AudioSource track;

  // Start is called before the first frame update
  void Start()
  {
    track = GetComponent<AudioSource>();
  }

  public void Play()
  {
    track.clip = clips[Random.Range(0, clips.Count)];
    track.volume = volume;
    track.Play();
  }

  public void Pause()
  {
    track.Pause();
  }

  public void Stop()
  {
    track.Stop();
  }
}
