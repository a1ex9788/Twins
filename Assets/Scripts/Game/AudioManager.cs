using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{

    [SerializeField] Image iconNoMusic;
    [SerializeField] Image iconNoEffects;
    [SerializeField] Image iconNoNotal;
    [SerializeField] Image iconMusic;
    [SerializeField] Image iconEffects;
    [SerializeField] Image iconTotal;
    [SerializeField] BackgroundMusicController backgroundMusicController;
    Vector3 iconTotalBasePos;
    Vector3 iconMusicBasePos;
    Vector3 iconEffectsBasePos;
    private static bool isMutedEffect = false;
    private static bool isMutedTotal = false;
    private static bool isMutedMusic = false;

    private void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Equals("StartMenu"))
        {
            iconMusic.transform.localPosition = iconMusicBasePos;
            iconEffects.transform.localPosition = iconEffectsBasePos;
            iconTotal.transform.localPosition = new Vector3(1000, 1000, 1000);
        }
        else
        {
            iconMusic.transform.localPosition = new Vector3(1000, 1000, 1000);
            iconEffects.transform.localPosition = new Vector3(1000, 1000, 1000);
            iconTotal.transform.localPosition = iconTotalBasePos;
        }

        if (isMutedMusic && isMutedEffect) { isMutedTotal = true; }
        else { isMutedTotal = false; }
    }

    void Awake()
    {
        iconTotalBasePos = iconTotal.transform.localPosition;
        iconMusicBasePos = iconMusic.transform.localPosition;
        iconEffectsBasePos = iconEffects.transform.localPosition;

        if (FindObjectsOfType<AudioManager>().Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);
        this.gameObject.GetComponent<AudioManager>().backgroundMusicController = GameObject.Find("BackgroundMusic").GetComponent<BackgroundMusicController>();
    }

    public static bool getIsMutedEffect() { return isMutedEffect; }
    public static void setIsMutedEffect(bool ismutedeffect) { isMutedEffect = ismutedeffect; }

    public static bool getIsMutedTotal() { return isMutedTotal; }
    public static void setIsMutedTotal(bool ismutedtotal) { isMutedTotal = ismutedtotal; }

    public static bool getIsMutedMusic() { return isMutedMusic; }
    public static void setIsMutedMusic(bool ismutedmusic) { isMutedMusic = ismutedmusic; }

    public void TotalMuted()
    {
        isMutedTotal = !isMutedTotal;
        iconNoNotal.gameObject.SetActive(getIsMutedTotal());

        if (isMutedTotal != AudioManager.getIsMutedMusic())
        {
            BaseEventData music = new BaseEventData(EventSystem.current);
            ExecuteEvents.Execute(GameObject.Find("MuteBackgroundMusicButton").gameObject, music, ExecuteEvents.submitHandler);
        }

        if (isMutedTotal != AudioManager.getIsMutedEffect())
        {
            BaseEventData effects = new BaseEventData(EventSystem.current);
            ExecuteEvents.Execute(GameObject.Find("MuteMusicEffectsButton").gameObject, effects, ExecuteEvents.submitHandler);
        }

        if (isMutedTotal)
        {
            iconNoMusic.gameObject.SetActive(false);
            iconNoEffects.gameObject.SetActive(false);
            if (isMutedTotal) iconNoMusic.gameObject.SetActive(true);
            if (isMutedTotal) iconNoEffects.gameObject.SetActive(true);
        }
    }

    public void MuteBackGroundMusic()
    {
        GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().mute = !GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().mute;
        AudioManager.setIsMutedMusic(GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().mute);
        iconNoMusic.gameObject.SetActive(AudioManager.getIsMutedMusic());
        if (AudioManager.getIsMutedMusic() & AudioManager.getIsMutedEffect())
        {
            iconNoNotal.gameObject.SetActive(true);
            isMutedTotal = true;
        }
        else { iconNoNotal.gameObject.SetActive(false); }
    }

    public void MuteEffects()
    {
        setIsMutedEffect(!AudioManager.getIsMutedEffect());
        iconNoEffects.gameObject.SetActive(AudioManager.getIsMutedEffect());

        if (AudioManager.getIsMutedMusic() & AudioManager.getIsMutedEffect())
        {
            iconNoNotal.gameObject.SetActive(true);
            isMutedTotal = true;
        }
        else { iconNoNotal.gameObject.SetActive(false); }
    }
}
