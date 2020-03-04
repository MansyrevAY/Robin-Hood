using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCoinsEarned : MonoBehaviour
{
    public ParticleSystem coinSystem;
    public LevelStateSO levelState;
    public int coinNumber;
    public float pauseBetweenCoins;

    private bool played = false;

    private void Awake()
    {
        if (coinSystem == null) { Debug.LogError("Coin System must be not null"); }
        if (levelState == null) { Debug.LogError("Level State must be not null"); }
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(levelState.State.Equals(typeof(WinState)) && !played)
        {
            StartCoroutine(SpawnCoins());
            played = true;
        }
    }

    protected IEnumerator SpawnCoins()
    {
        for (int i = 0; i < coinNumber; i++)
        {
            coinSystem.Play();
            yield return new WaitForSeconds(pauseBetweenCoins);
        }
    }
}
