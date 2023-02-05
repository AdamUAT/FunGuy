using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    #region Singleton
    public static MusicManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    [SerializeField]
    AudioSource track1;

    [SerializeField]
    AudioSource track2;

    bool transition = false;
    AudioSource currentTrack;
    AudioSource previousTrack;
    float transitionStart;
    float transitionTime;

    [SerializeField]
    List<AudioClip> songs;


    // Start is called before the first frame update
    void Start()
    {
        currentTrack = track1;
        previousTrack = track2;
    }

    // Update is called once per frame
    void Update()
    {
       if(transition)
       {
            float time_passed = Time.time - transitionStart;

            if(time_passed > transitionTime)
            {
                // Finish Transition
                currentTrack.volume = 1.0f;
                previousTrack.volume = 0.0f;
                previousTrack.Stop();

                transition = false;
            } else {

                // Transition Volumes
                float transitionPercent = time_passed / transitionTime;

                currentTrack.volume = Mathf.Lerp(0.0f, 1.0f, transitionPercent);
                previousTrack.volume = Mathf.Lerp(1.0f, 0.0f, transitionPercent);
            }
       } 
    }

    public void Play()
    {
        currentTrack.Play();
    }

    public void Pause()
    {
        currentTrack.Pause();
        previousTrack.Pause();
    }

    public void TransitionToSong(int songIndex, float transitionTime = 5.0f)
    {
        transitionStart = Time.time;
        this.transitionTime = transitionTime;

        // Swap tracks
        AudioSource tempTrack = previousTrack;
        previousTrack = currentTrack;
        currentTrack = tempTrack; 

        // Get Song
        AudioClip song = songs[songIndex];

        // Play song on current track starting quiet
        currentTrack.clip = song;
        currentTrack.volume = 0.0f;
        currentTrack.Play();
        transition = true;
    }
}
