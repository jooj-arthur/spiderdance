using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSourceMusicaDeFundo;
    public AudioSource audioSourceSFX;
    public AudioClip musicaDeFundo;
    // Start is called before the first frame update
    void Start()
    {
        audioSourceMusicaDeFundo.clip = musicaDeFundo;
        audioSourceMusicaDeFundo.Play();
    }

    public void ToqueSFX(AudioClip clip){
        audioSourceSFX.clip = clip;
        audioSourceSFX.Play();
        audioSourceMusicaDeFundo.Stop();

    }
}
