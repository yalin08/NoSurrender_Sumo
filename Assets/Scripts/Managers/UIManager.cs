using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using DG.Tweening;
using Pixelplacement;
public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI UICountdown;
    public TextMeshProUGUI UITimer;

    public TextMeshProUGUI UIScore;
    public TextMeshProUGUI DefeatedEnemies;

    public GameObject win, lose,pause;

    int countdown;

    public void UpdateScore(float score)
    {
        UIScore.text = ""+score;
        UIScore.transform.DOScale(1.3f, 0.4f).OnComplete(ResetTweenScore);
    }
    public void ResetTweenScore()
    {
        UIScore.transform.DOScale(1f, 0.6f);

    }

    public void PauseGameButton()
    {
        if (Time.timeScale == 1)
        {
            pause.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            countdown = 3;
            UICountdown.enabled = true;
            StartCoroutine(StartCountdown());
        }
    }

    IEnumerator StartCountdown() //Countdown when unpausing the game
    {
        UICountdown.text = "" + countdown;
        yield return new WaitForSecondsRealtime(1);
        UICountdown.transform.DOScale(1.3f, 0.3f).OnComplete(ResetTweenCountdown).SetUpdate(UpdateType.Normal, true); 
        countdown--;
        UICountdown.text = "" + countdown;
        if (countdown <= 0)
        {
            ResumeGame();
           
           UICountdown.enabled = false;
        }
        else
        {
            StartCoroutine(StartCountdown());
        }
    }  
    public void ResetTweenCountdown()
    {
        UICountdown.transform.DOScale(1f, 0.6f).SetUpdate(UpdateType.Normal, true); ;

    }
    void ResumeGame()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
    }

}
