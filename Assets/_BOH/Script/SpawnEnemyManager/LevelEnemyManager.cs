using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemyManager : MonoBehaviour, IListener
{
    public static LevelEnemyManager Instance;
    public Vector2 spawnPosition;
    public EnemyWave[] EnemyWaves;
    int currentWave = 0;

    List<GameObject> listEnemySpawned = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    int totalEnemy, currentSpawn;
    // Start is called before the first frame update
    void Start()
    {
        if (GameLevelSetup.Instance)
            EnemyWaves = GameLevelSetup.Instance.GetLevelWave();

        var hit = Physics2D.Raycast(transform.position, Vector2.down, 100, GameManager.Instance.layerGround);
        if (hit)
        {
            //Debug.LogError(hit.collider.gameObject + "pos: " + hit.point);
            spawnPosition = hit.point;
        }
        else
            Debug.LogError("NEED PLACE LEVEL SPAWN MANAGER ABOVE THE GROUND TO SPAWN THE ENEMY");

        //calculate number of enemies
        totalEnemy = 0;
        for (int i = 0; i < EnemyWaves.Length; i++)
        {

            for (int j = 0; j < EnemyWaves[i].enemySpawns.Length; j++)
            {
                var enemySpawn = EnemyWaves[i].enemySpawns[j];
                for (int k = 0; k < enemySpawn.numberEnemy; k++)
                {
                    totalEnemy++;
                }
            }
        }

        currentSpawn = 0;
    }

    IEnumerator SpawnEnemyCo()
    {
        for (int i = 0; i < EnemyWaves.Length; i++)
        {
            yield return new WaitForSeconds(EnemyWaves[i].wait);

            for (int j = 0; j < EnemyWaves[i].enemySpawns.Length; j++)
            {
                var enemySpawn = EnemyWaves[i].enemySpawns[j];
                yield return new WaitForSeconds(enemySpawn.wait);
                for (int k = 0; k < enemySpawn.numberEnemy; k++)
                {
                    GameObject _temp = Instantiate(enemySpawn.enemy, spawnPosition + Vector2.up * 0.1f, Quaternion.identity) as GameObject;
                    _temp.SetActive(false);
                    _temp.transform.parent = transform;

                    yield return new WaitForSeconds(0.1f);
                    _temp.SetActive(true);
                    //_temp.transform.localPosition = Vector2.zero;
                    listEnemySpawned.Add(_temp);

                    currentSpawn++;
                    MenuManager.Instance.UpdateEnemyWavePercent(currentSpawn, totalEnemy);

                    yield return new WaitForSeconds(enemySpawn.rate);
                }
            }
        }

        //check all enemy killed
        while (isEnemyAlive()) { yield return new WaitForSeconds(0.1f); }

        //yield return new WaitForSeconds(1);
        //GameManager.Instance.Victory();
    }


    bool isEnemyAlive()
    {
        for(int i = 0; i< listEnemySpawned.Count;i++)
        {
            if (listEnemySpawned[i].activeInHierarchy)
                return true;
        }

        return false;
    }

    public void IGameOver()
    {
        //throw new System.NotImplementedException();
    }

    public void IOnRespawn()
    {
        //throw new System.NotImplementedException();
    }

    public void IOnStopMovingOff()
    {
        //throw new System.NotImplementedException();
    }

    public void IOnStopMovingOn()
    {
        //throw new System.NotImplementedException();
    }

    public void IPause()
    {
        //throw new System.NotImplementedException();
    }

    public void IPlay()
    {
        StartCoroutine(SpawnEnemyCo());
        //throw new System.NotImplementedException();
    }

    public void ISuccess()
    {
        StopAllCoroutines();
        //throw new System.NotImplementedException();
    }

    public void IUnPause()
    {
        //throw new System.NotImplementedException();
    }
}

[System.Serializable]
public class EnemyWave
{
    public float wait = 3;
    public EnemySpawn[] enemySpawns;
}


