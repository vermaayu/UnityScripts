using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    //[SerializeField]
    //private GameObject _tripleShotPowerupPrefab;
    //[SerializeField]
    //private GameObject _speedBoostPowerupPrefab;
    
    [SerializeField]
    private GameObject[] _powerup;

    //_tripleShotPowerupPrefab, _speedBoostPowerupPrefab

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Coroutine
    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 spawn_pos = new Vector3(Random.Range(-9f, 9f), 6, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawn_pos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 post2Spawn = new Vector3(Random.Range(-9f, 9f), 6, 0);
            Instantiate(_powerup[Random.Range(0,3)], post2Spawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(8, 15));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
