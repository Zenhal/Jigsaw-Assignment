using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip placeSound;
    [SerializeField] private AudioClip failedPlacement;
    [SerializeField] private AudioClip winSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    
    public void PlaySound(string clipName)
    {
        switch (clipName)
        {
            case "placed" : PlaySound(placeSound);
                break;
            case "failed" : PlaySound(failedPlacement);
                break;
            case "win" : PlaySound(winSound);
                break;
        }
    }
}
