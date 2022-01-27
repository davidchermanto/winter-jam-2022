using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("AudioSources")]
    [SerializeField] private AudioSource sfxPlayer;
    [SerializeField] private AudioSource ostPlayer;
    [SerializeField] private AudioSource wthPlayer;

    [Header("SFX Clips")]
    [SerializeField] private AudioClip tap;
    [SerializeField] private AudioClip chirp;
    [SerializeField] private AudioClip woosh;
    [SerializeField] private AudioClip thunder;

    [Header("OST Clips")]
    [SerializeField] private AudioClip easy;
    [SerializeField] private AudioClip medium;
    [SerializeField] private AudioClip hard;

    [Header("Weather Clips")]
    [SerializeField] private AudioClip rainy;
    [SerializeField] private AudioClip windy;

    // Others
    private float dspSongTime;

    private void Awake()
    {
        Instance = this;
    }

    public void Setup()
    {
        ostPlayer.loop = true;
        wthPlayer.loop = true;
    }

    public void PlayOneShot(string audio)
    {
        AudioClip soundtrack = null;

        switch (audio)
        {
            case "thunder":
                soundtrack = easy;
                break;
            case "NORMAL":
                soundtrack = medium;
                break;
            case "HARD":
                soundtrack = hard;
                break;
            default:
                Debug.LogError("No soundtrack found of title: " + audio);
                break;
        }


    }

    public void PlaySoundtrack(string title)
    {
        AudioClip soundtrack = null;

        switch (title)
        {
            case "EASY":
                soundtrack = easy;
                break;
            case "NORMAL":
                soundtrack = medium;
                break;
            case "HARD":
                soundtrack = hard;
                break;
            default:
                Debug.LogError("No soundtrack found of title: " + title);
                break;
        }

        dspSongTime = (float)AudioSettings.dspTime;

        ostPlayer.clip = soundtrack;
        ostPlayer.Play();

        ostPlayer.volume = Constants.maxVolume;
    }

    public float GetSoundtrackStartTime()
    {
        return dspSongTime;
    }

    public void StopSoundtrack()
    {
        StartCoroutine(LerpOSTVolume(0, 2));
    }

    public void PlayWeather(string title)
    {

    }

    public void StopWeather()
    {

    }

    public IEnumerator LerpOSTVolume(float targetVolume, float duration)
    {
        float timer = 0;

        float currentVolume = ostPlayer.volume;

        while(timer < 1)
        {
            timer += Time.deltaTime / targetVolume;

            ostPlayer.volume = Mathf.Lerp(currentVolume, targetVolume, timer);

            yield return new WaitForEndOfFrame();
        }

        if(targetVolume == 0)
        {
            ostPlayer.Stop();
        }
    }
}
