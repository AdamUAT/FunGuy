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

    //[SerializeField]
    //List<> songs;

    // TODO - Define how to do audio mixing to transition the sound. AKA lower volume of track 1, increase volume of track 2 and then swap


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {

    }

    public void Pause()
    {

    }

    public void TransitionToSong(int songIndex, float transitionTime = 1.0f)
    {

    }
}
