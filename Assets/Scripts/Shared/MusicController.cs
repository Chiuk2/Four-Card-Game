using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public AudioClip menuSong, blackJackSong, highLowSong, fivePokerSong, sevenPokerSong, gameSelectSong;

    private AudioSource audioS;
    private bool audioOnOff;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        audioS = GetComponent<AudioSource>();
        audioOnOff = true;

        switch (sceneName)
        {
            case "MainMenu":
                PlaySong(menuSong);
                break;
            case "BlackJack":
                PlaySong(blackJackSong);
                break;
            case "HighLow":
                PlaySong(highLowSong);
                break;
            case "FivePoker":
                PlaySong(fivePokerSong);
                break;
            case "SevenPoker":
                PlaySong(sevenPokerSong);
                break;
            case "GameSelect":
                PlaySong(gameSelectSong);
                break;
            default:
                PlaySong(menuSong);
                break;
        }

            
    }

    private void PlaySong(AudioClip song)
    {
        audioS.clip = song;
        audioS.Play();
    }

    private void OnMouseDown()
    {
        if (audioOnOff)
            audioS.Stop();
        else
            audioS.Play();
        audioOnOff = !audioOnOff;
    }
}
