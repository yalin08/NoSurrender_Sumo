using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using Pixelplacement;
public class GameManager : Singleton<GameManager>
{


    public List<CharacterStats> Characters;
    public float NeededTime;
    public float timer;

    public TextMeshProUGUI UITimer;

    public void StartGame()
    {
        foreach (CharacterStats cs in Characters)
        {
            CharacterMove cm = cs.GetComponent<CharacterMove>();
            cm.canMove = true;
            cm.ac.Run(cs.Speed);
        }
        timer = NeededTime; TimeOnScreen();
        StartCoroutine(StartTimer());
    }
    void TimeOnScreen()
    {
        string minutes = Mathf.Floor(timer / 60).ToString();
        int secondsNumber = (int)(timer % 60);
        string seconds = secondsNumber.ToString();
        if (secondsNumber < 10)
            seconds = "0" + secondsNumber;
        UITimer.text = minutes + ":" + seconds;
    }
    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1);
        timer--;
        TimeOnScreen();
        
        StartCoroutine(StartTimer());
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

    }


}
