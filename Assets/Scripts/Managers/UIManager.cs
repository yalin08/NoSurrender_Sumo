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

    public GameObject win, lose;

    public void UpdateScore(float score)
    {
        UIScore.text = ""+score;
        UIScore.transform.DOScale(1.3f, 0.4f).OnComplete(ResetTween);
    }
    public void ResetTween()
    {
        UIScore.transform.DOScale(1f, 0.6f);

    }

}
