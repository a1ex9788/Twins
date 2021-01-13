using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ChallengeMode : MonoBehaviour
{
    [SerializeField] Button challenge1Button;
    [SerializeField] Button challenge2Button;
    [SerializeField] Button challenge3Button;
    [SerializeField] Text scoreChallengeText1;
    [SerializeField] Text scoreChallengeText2;
    [SerializeField] Text scoreChallengeText3;
    [SerializeField] Image medalChallenge1;
    [SerializeField] Image medalChallenge2;
    [SerializeField] Image medalChallenge3;
    [SerializeField] Text bestCategoryText;


    private void Start()
    {
        Observable lastScoreObservable = new LastScore();
        Observer medalObserver0 = new Medals(lastScoreObservable, 0);
        Observer medalObserver1 = new Medals(lastScoreObservable, 1);
        Observer medalObserver2 = new Medals(lastScoreObservable, 2);
        Observer scoreObserver0 = new MaximScore(lastScoreObservable, 0);
        Observer scoreObserver1 = new MaximScore(lastScoreObservable, 1);
        Observer scoreObserver2 = new MaximScore(lastScoreObservable, 2);
        Observer bestCategoryObserver = new BestCategory(lastScoreObservable);

        lastScoreObservable.addObserver(medalObserver0);
        lastScoreObservable.addObserver(medalObserver1);
        lastScoreObservable.addObserver(medalObserver2);
        lastScoreObservable.addObserver(scoreObserver0);
        lastScoreObservable.addObserver(scoreObserver1);
        lastScoreObservable.addObserver(scoreObserver2);
        lastScoreObservable.addObserver(bestCategoryObserver);

        lastScoreObservable.notifyObservers();
    
    }

    public void SetScoreText(int id)
    {
        switch (id)
        {
            case 0: scoreChallengeText1.text = PersistenceManager.GetChallengeMaxScore(id).ToString(); break;
            case 1: scoreChallengeText2.text = PersistenceManager.GetChallengeMaxScore(id).ToString(); break;
            case 2: scoreChallengeText3.text = PersistenceManager.GetChallengeMaxScore(id).ToString(); break;
        }
    }

    public void SetMedal(int id)
    {
        
            Sprite[] loadedMedals = Resources.LoadAll<Sprite>("Images&Sprites/medallas");
            List<Image> medals = new List<Image> {
            medalChallenge1,
            medalChallenge2,
            medalChallenge3
        };
            for (int i = 0; i < medals.Count; i++)
            {
                int maxScore = PersistenceManager.GetChallengeMaxScore(id);
            if (maxScore >= 200) medals[id].sprite = loadedMedals[0];
            else if (maxScore >= 150) medals[id].sprite = loadedMedals[1];
            else if (maxScore >= 100) medals[id].sprite = loadedMedals[2];
            else medals[id].sprite = Resources.LoadAll<Sprite>("Images&Sprites/medallaNegra")[0];
            }
        
    }
    public void SetBestCategory()
    {
        string category = "Mejor Categoría : ";
        string theme = "-----";
        if (PersistenceManager.GetChallengeMaxScore(0) == 0 && PersistenceManager.GetChallengeMaxScore(1) == 0 && PersistenceManager.GetChallengeMaxScore(2) == 0)
        {
            bestCategoryText.text = category + theme;
        }
        else
        {
            theme = "FÚTBOL";
            int maxScore = PersistenceManager.GetChallengeMaxScore(0);
            if (PersistenceManager.GetChallengeMaxScore(1) > maxScore)
            {
                theme = "ANIMALES";
                maxScore = PersistenceManager.GetChallengeMaxScore(1);
            }
            if (PersistenceManager.GetChallengeMaxScore(2) > maxScore)
            {
               theme = "ALIMENTOS";
            }           
            bestCategoryText.text = category + theme;
        }
    }

    public void Challenge1()
    {
        PersistenceManager.currentChallengeScore = 0;
        PersistenceManager.totalTimeChallenge = 0;
        GameConfiguration.SetChallengeGameConfiguration(1,1);
        SceneManager.LoadScene("Game");
    }

    public void Challenge2()
    {
        PersistenceManager.currentChallengeScore = 0;
        PersistenceManager.totalTimeChallenge = 0;
        GameConfiguration.SetChallengeGameConfiguration(2, 1);
        SceneManager.LoadScene("Game");
    }

    public void Challenge3()
    {
        PersistenceManager.currentChallengeScore = 0;
        PersistenceManager.totalTimeChallenge = 0;
        GameConfiguration.SetChallengeGameConfiguration(3, 1);
        SceneManager.LoadScene("Game");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("StartMenu");
    }

    void medallas()
    {
        Sprite[] loadedMedals = Resources.LoadAll<Sprite>("Images&Sprites/medallas");
        List<Image> medals = new List<Image> {
            medalChallenge1,
            medalChallenge2,
            medalChallenge3
        };
       for (int i = 0; i < medals.Count; i++)
        {
            int maxScore = PersistenceManager.GetChallengeMaxScore(i);
            if (maxScore > 100) medals[i].sprite = loadedMedals[0];
            else if (maxScore > 75) medals[i].sprite = loadedMedals[1];
            else if (maxScore > 50) medals[i].sprite = loadedMedals[2];
            else medals[i].sprite = Resources.Load<Sprite>("Images&Sprites/poop");
        }
    }
}

public interface Observable
{
    void addObserver(Observer o);
    void removeObserver(Observer o);
    void notifyObservers();
}

public interface Observer
{
    void update();
}

public class LastScore : Observable
{
    private List<Observer> observersList = new List<Observer>();

    void Observable.addObserver(Observer o)
    {
        observersList.Add(o);
    }
    void Observable.removeObserver(Observer o)
    {
        observersList.Remove(o);
    }
    void Observable.notifyObservers()
    {
        foreach (Observer observer in observersList)
        {
            observer.update();
        }
    }
}

public class Medals : Observer
{
    ChallengeMode challengeMode = GameObject.Find("GAME_MANAGER").GetComponent<ChallengeMode>();
    private Observable observable = null;
    private int id;
    public Medals(Observable observable, int id)
    {
        this.observable = observable;
        this.id = id;
    }
    void Observer.update()
    {
        challengeMode.SetMedal(id);
    }

} 

public class MaximScore : Observer
{
    ChallengeMode challengeMode = GameObject.Find("GAME_MANAGER").GetComponent<ChallengeMode>();
    private Observable observable = null;
    private int id;
    public MaximScore(Observable observable, int id)
    {
        this.observable = observable;
        this.id = id;
    }

    void Observer.update()
    {
        challengeMode.SetScoreText(id);
    }  
}

public class BestCategory : Observer
{
    ChallengeMode challengeMode = GameObject.Find("GAME_MANAGER").GetComponent<ChallengeMode>();
    private Observable observable = null;
    public BestCategory(Observable observable)
    {

    }
    void Observer.update()
    {
        challengeMode.SetBestCategory();
    }
}