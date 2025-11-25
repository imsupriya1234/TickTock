using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioClip wrongAudio; 
    [SerializeField] AudioClip correctAudio; 
    [SerializeField] AudioClip looseAudio;   
    [SerializeField] AudioClip winAudio;  
    [SerializeField] AudioClip buttonClickAudio;    
    
    [SerializeField] AudioSource audioPlayer;

    public static AudioController Instance; 

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null) // Creating Singleton
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
          
    }
   
    // Audio play Func by passing Audio Clip
    private IEnumerator PlayAudioClip(AudioClip clip) 
    {
        if (clip != null)
        {
            audioPlayer.clip = clip;
            audioPlayer.Play();
            yield return new WaitForSeconds(clip.length + 1f);
        }
    }

    public void PlayWrongSound() //  wrong sound
    {
        StartCoroutine(PlayAudioClip(wrongAudio));
    }

    public void PlayCorrectSound() // Correct sound
    {
        StartCoroutine(PlayAudioClip(correctAudio));
    }

    public void PlayLoosePopUpSound() // Loose Panel sound 
    {
        StartCoroutine(PlayAudioClip(looseAudio));
    }

    public void PlayWinPopUpSound() // Win Panel sound
    {
        StartCoroutine(PlayAudioClip(winAudio));
    }

    public void PlayButtonClickSound() // Button click sound
    {
        StartCoroutine(PlayAudioClip(buttonClickAudio));
    }
}
