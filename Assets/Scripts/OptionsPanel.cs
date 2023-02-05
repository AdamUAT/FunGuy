using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField]
    LabeledSlider MasterVolumeSlider;
    
    [SerializeField]
    LabeledSlider MusicVolumeSlider;

    [SerializeField]
    LabeledSlider SfxVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        CheckValues();
    }

    public void CheckValues()
    {
        MasterVolumeSlider.SetValue(VolumeManager.Instance.master_volume);
        MusicVolumeSlider.SetValue(VolumeManager.Instance.music_volume);
        SfxVolumeSlider.SetValue(VolumeManager.Instance.sfx_volume);
    }
}
