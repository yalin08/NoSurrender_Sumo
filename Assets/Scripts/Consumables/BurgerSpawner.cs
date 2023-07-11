using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class BurgerSpawner : Singleton<BurgerSpawner>
{

    public GameObject BurgerPrefab;

    public float SpawnSeconds;



    public Transform[] SpawnLocations;
    [HideInInspector] public List<Transform> _SpawnLocations;
    public List<Transform> BurgersOnTheScene;
    

    public IEnumerator SpawnBurgers()
    {



        yield return new WaitForSeconds(SpawnSeconds);
        if (_SpawnLocations.Count <= 0)
        {
            foreach (Transform transform in SpawnLocations)
                _SpawnLocations.Add(transform);
        }


        int i = Random.Range(0, _SpawnLocations.Count);

        GameObject burger = Instantiate(BurgerPrefab, _SpawnLocations[i].position, Quaternion.identity, _SpawnLocations[i].root);
        BurgersOnTheScene.Add(burger.transform);
        _SpawnLocations.Remove(_SpawnLocations[i]);
        StartCoroutine(SpawnBurgers());
    }

    public void StartTheGame()
    {
       
        GameObject[] BurgersInStart = GameObject.FindGameObjectsWithTag("Burger");
        foreach (GameObject go in BurgersInStart)
            BurgersOnTheScene.Add(go.transform);
        StartCoroutine(SpawnBurgers());
    }



    // Start is called before the first frame update
    void Start()
    {

    }

}
