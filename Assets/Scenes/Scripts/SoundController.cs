using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioSource music;

    private int prefAudio;
    private int prefMusic;

    [SerializeField] GameObject iconAudioOn;
    [SerializeField] GameObject iconAudioOff;

    [SerializeField] GameObject iconMusicOn;
    [SerializeField] GameObject iconMusicOff;

    private void Awake()
    {
        prefAudio = PlayerPrefs.GetInt("Audio");
        prefMusic = PlayerPrefs.GetInt("Music");

       // Debug.Log(prefAudio + " " + prefMusic);

    }

    private void Start()
    {

        if (prefMusic == 0)
        {
            Debug.Log("Остановочка");
            music.Play();
            music.mute = false;
            iconMusicOff.SetActive(true);
        }
        else {

            iconMusicOn.SetActive(true);
            music.mute = true;
        }

        if (prefAudio == 0)
        {
            audio.mute = false;
            iconAudioOff.SetActive(true);
        }
        else {
            iconAudioOn.SetActive(true);
            audio.mute = true;
        }
    }



    public void SwitchAudio() {
        prefAudio = PlayerPrefs.GetInt("Audio");
        if (prefAudio == 0)
        {

            audio.mute = false;
        } else {
            audio.Stop();
            audio.mute = true;
        }
    }

    public void SwitchMusic() {
        prefMusic = PlayerPrefs.GetInt("Music");
        if (prefMusic == 0)
        {
            music.Play();
            music.mute = false;
        } else {
            music.Stop();
            music.mute = true;
        }
    }

    public void EnableMusic() {
        iconMusicOn.SetActive(false);
        iconMusicOff.SetActive(true);
        PlayerPrefs.SetInt("Music", 0);
        SwitchMusic();
    }    
    
    public void DisableMusic() {
        iconMusicOff.SetActive(false);
        iconMusicOn.SetActive(true);
        PlayerPrefs.SetInt("Music", 1);
        SwitchMusic();
    }

    public void EnableAudio() {
        iconAudioOn.SetActive(false);
        iconAudioOff.SetActive(true);
        PlayerPrefs.SetInt("Audio", 0);
        SwitchAudio();
    }    
    
    public void DisableAudio() {
        iconAudioOff.SetActive(false);
        iconAudioOn.SetActive(true);
        PlayerPrefs.SetInt("Audio", 1);
        SwitchAudio();
    }

/*    private void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("Music"));
    }*/

}
