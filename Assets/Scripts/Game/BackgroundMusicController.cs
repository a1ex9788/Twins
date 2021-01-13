using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BackgroundMusicController : MonoBehaviour
{
  
    [SerializeField] AudioClip menusMusic;
    [SerializeField] AudioClip gameMusic;

    AudioSource audioSource;

 
    void Awake()
    {
        if (FindObjectsOfType<BackgroundMusicController>().Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);

        audioSource = GetComponent<AudioSource>();
    }

   
    void OnLevelWasLoaded(int level)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName.Equals("StartMenu") || sceneName.Equals("LevelMode"))
        {
            PlayMenusMusic();
        }

        if (sceneName.Equals("Game"))
        {
            PlayGameMusic();
        }
    }

    public void PlayMenusMusic() {
        if (audioSource.clip.Equals(gameMusic)) {
            audioSource.clip = menusMusic;
            audioSource.Play();
        }
    }

    public void PlayGameMusic() {
        if (audioSource.clip.Equals(menusMusic)) {
            audioSource.clip = gameMusic;
            audioSource.Play();
        }
    }

  
}
