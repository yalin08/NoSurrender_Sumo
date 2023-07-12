using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterStats : MonoBehaviour
{
    public int SizePoints = 0;
    public float speed = 1;
    [HideInInspector] public float speedOnStart = 1;
    [HideInInspector] public CharacterAnimationEvent StickmanAnimator;
    [HideInInspector] public CharacterStats LastTouchedEnemy;

    int kills = 0;
    private void Start()
    {
        speedOnStart = speed;
        StickmanAnimator = GetComponentInChildren<CharacterAnimationEvent>();
    }

    public void UpdateStats(int Value)
    {
        SizePoints += Value;
        float f = 1 - 1 / (1 + 0.1f * (SizePoints));
        transform.DOScale(1 + f * 2, 0.5f); // Hyperbolically increases size


        if (gameObject.layer == 3) //Updates UI if this is the player
        {
            UIManager.Instance.UpdateScore(SizePoints * 100);
            CameraFollow.Instance.ChangeOffset(f);
        }
        speed = speedOnStart - f / 2;  // You get slower as you increase in size




    }
    public void Die()
    {
        if (gameObject.layer == 3) // If this is the player
            GameManager.Instance.Lose();

        if (LastTouchedEnemy != null) // Your killer gets your points
        {
            LastTouchedEnemy.UpdateStats(SizePoints);
            LastTouchedEnemy.kills++;


            if (LastTouchedEnemy.gameObject.layer == 3)
                UIManager.Instance.DefeatedEnemies.text = "Enemies Defeated:\n" + LastTouchedEnemy.kills;
        }


        GameManager.Instance.Characters.Remove(this);
        Destroy(gameObject);

    }

    private void Update()
    {
        if (transform.position.y < -1f)
            Die();
    }

}
