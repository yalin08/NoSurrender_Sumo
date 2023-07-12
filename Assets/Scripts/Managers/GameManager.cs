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

    private void Awake()
    {
        FindCharacters();
    }
    public void StartGame()
    {
     
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
    void FindCharacters() //Adds all characters to a list
    {
        Characters.Clear();
        GameObject[] cstemp = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject cs in cstemp)
        {
            Characters.Add(cs.GetComponent<CharacterStats>());
        }
    }
    void TimeOnScreen() //Show tine on UI
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

        EndGame();
    }
    public void Lose()
    {
        Debug.Log("Lose");
        UIManager.Instance.lose.SetActive(true);

        EndGame();
    }
    public void EndGame()
    {
        UIManager.Instance.UITimer.enabled = false;
        UIManager.Instance.DefeatedEnemies.transform.parent.gameObject.SetActive(true);
        BurgerSpawner.Instance.StopAllCoroutines();
        CameraFollow.Instance.OnGameFinish();


        if (Characters.Count > 0)
            CameraFollow.Instance.followObject = Characters[0].transform;


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
    IEnumerator StartCountdown() //Countdown when game starts
    {
        UIManager.Instance.UICountdown.text = "" + countdown;
        yield return new WaitForSeconds(1);
        UIManager.Instance.UICountdown.transform.DOScale(1.3f, 0.3f).OnComplete(ResetTween);
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
         x => -x.SizePoints   // List gets arranged by player points, [0] is the winner.
        ).ToList();

        foreach (CharacterStats cs in Characters)
        {
            cs.StickmanAnimator.crown.SetActive(false);
        }
        if(Characters.Count>0)
        Characters[0].StickmanAnimator.crown.SetActive(true); //Put crown on the winner


        if (timer <= 0) //When timer runs out
        {
            if (Characters[0].gameObject.layer == 3) //if player is the biggest character
            {
                Win();
            }
            else
            {
                Lose();
            }
        }

        if (Characters.Count == 1) //If there is only one character left
        {
            if (Characters[0].gameObject.layer == 3)//if player is the biggest character
            {
                Win();
            }
            else
            {
                Lose();
            }
        }
        if (Characters.Count == 0) //If everyone is dead
        {
            Lose();
        }
    }


}
