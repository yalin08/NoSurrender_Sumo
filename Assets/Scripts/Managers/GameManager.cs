using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using DG.Tweening;
using Pixelplacement;
public class GameManager : Singleton<GameManager>
{


    public List<CharacterStats> Characters;
    public float NeededTime;
    public float timer;
    int countdown = 3;


    public void StartGame()
    {
        FindCharacters();
        UIManager.Instance.UIScore.enabled = true;
        UIManager.Instance.UITimer.enabled = true;
        foreach (CharacterStats cs in Characters)
        {
            CharacterMove cm = cs.GetComponent<CharacterMove>();
            cm.canMove = true;
            cm.ac.Run(cs.speed);
        }
        TimeOnScreen();
        StartCoroutine(StartTimer());
    }
    void FindCharacters()
    {
        Characters.Clear();
        GameObject[] cstemp = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject cs in cstemp)
        {
            Characters.Add(cs.GetComponent<CharacterStats>());
        }
    }
    void TimeOnScreen()
    {
        string minutes = Mathf.Floor(timer / 60).ToString();
        int secondsNumber = (int)(timer % 60);
        string seconds = secondsNumber.ToString();
        if (secondsNumber < 10)
            seconds = "0" + secondsNumber;
        UIManager.Instance.UITimer.text = minutes + ":" + seconds;
    }

    public void Win()
    {
        Debug.Log("Win");
        UIManager.Instance.win.SetActive(true);
       
        LastOneDances();
    }
    public void Lose()
    {
        Debug.Log("Lose");
        UIManager.Instance.lose.SetActive(true);

        LastOneDances();
    }
    public void LastOneDances()
    {
        UIManager.Instance.UITimer.enabled = false;
        UIManager.Instance.DefeatedEnemies.transform.parent.gameObject.SetActive(true);
        foreach (CharacterStats stat in Characters)
        {
            CharacterMove move = stat.GetComponent<CharacterMove>();
            stat.transform.rotation = Quaternion.Euler(0, 180, 0);
            move.canMove = false;
            move.rb.velocity = Vector3.zero;
            stat.GetComponent<AnimationController>().Dance();

        }

    }
    public void StartCountdownOnButton()
    {
        StartCoroutine(StartCountdown());
    }
    IEnumerator StartCountdown()
    {
        UIManager.Instance.UICountdown.text = "" + countdown;
        yield return new WaitForSeconds(1);
        UIManager.Instance.UICountdown.transform.DOScale(1.3f,0.3f).OnComplete(ResetTween);
        countdown--;
        UIManager.Instance.UICountdown.text = "" + countdown;
        if (countdown <= 0)
        {
            StartGame();
            BurgerSpawner.Instance.StartTheGame();
            UIManager.Instance.UICountdown.enabled = false;
        }
        else
        {
            StartCoroutine(StartCountdown());
        }
    }
    void ResetTween()
    {
        UIManager.Instance.UICountdown.transform.DOScale(1f, 0.7f);
    }
    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1);
        timer--;
        TimeOnScreen();

        StartCoroutine(StartTimer());
    }

    private void Start()
    {
        timer = NeededTime;
    }

    // Update is called once per frame
    void Update()
    {

        Characters = Characters.OrderBy(
         x => -x.SizePoints   //-x.gameObject.transform.position.z
        ).ToList();

        foreach (CharacterStats cs in Characters)
        {
            cs.crown.SetActive(false);
        }
        Characters[0].crown.SetActive(true);


        if (timer <= 0)
        {
            if (Characters[0].gameObject.layer == 3)
            {
                Win();
            }
            else
            {
                Lose();
            }
        }

        if (Characters.Count == 1)
        {
            if (Characters[0].gameObject.layer == 3)
            {
                Win();
            }
            else
            {
                Lose();
            }
        }
        if (Characters.Count == 0)
        {
            Lose();
        }
    }


}
