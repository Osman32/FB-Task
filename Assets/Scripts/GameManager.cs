using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("Enemy")]
    [SerializeField] int enemyCount = 1;
    [SerializeField] float enemySpeed = 1.8f;
    [SerializeField] int enemyHealth = 100;

    [Header("Player")]
    [SerializeField] float playerSpeed = 1.7f;
    [SerializeField] float playerWeaponDamage = 34f;
    [SerializeField] float playerWeaponFireRate = 0.5f;
    [SerializeField] float playerWeaponFireRange = 10f;

    [Space]
    [SerializeField] private Text counter;

    [HideInInspector]
    public List<EnemyController> enemies = new List<EnemyController>();

    private int count = 4;
    private PlayerController player;
    private Transform enemySpawnArea;

    #region Private Methods
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        StartCoroutine(CounterCoroutine());
        GameSetup();
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (enemies.Count == 0)
        {
            StartCoroutine(player.Win());
        }
    }

    private void GameSetup()
    {

        //--------------------------------
        player.speed = playerSpeed;
        player.GetComponent<WeaponController>().damage = playerWeaponDamage;
        player.GetComponent<WeaponController>().range = playerWeaponFireRange;
        player.GetComponent<WeaponController>().fireRate = playerWeaponFireRate;
        //--------------------------------
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(Resources.Load("Enemy")) as GameObject;
            enemy.name = "Enemy " + i;
            enemy.GetComponent<EnemyController>().speed = enemySpeed;
            enemy.GetComponent<EnemyController>().health = enemyHealth;
            enemies.Add(enemy.GetComponent<EnemyController>());
        }
        EnemySpawn(enemies);
    }

    private void EnemySpawn(List<EnemyController> enemies)
    {
        Transform spawnArea = GameObject.Find("SpawnArea").transform;

        float leftEdge = spawnArea.position.x + (spawnArea.localScale.x / 2f) - 0.5f;
        float rightEdge = spawnArea.position.x - (spawnArea.localScale.x / 2f) + 0.5f;
        float upEdge = spawnArea.position.z - (spawnArea.localScale.z / 2f) + 0.5f;
        float downEdge = spawnArea.position.z + (spawnArea.localScale.z / 2f) - 0.5f;

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].transform.position = new Vector3(Random.Range(leftEdge, rightEdge), spawnArea.position.y + 0.5f, Random.Range(upEdge, downEdge));
        }
    }

    private IEnumerator CounterCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        while (true)
        {
            count -= 1;
            yield return new WaitForSecondsRealtime(1f);
            counter.text = count.ToString();
            if (count == 0)
            {
                counter.text = "Go!";
                Time.timeScale = 1f;
                yield return new WaitForSecondsRealtime(0.5f);
                counter.enabled = false;
                break;
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawWireSphere(GameObject.FindGameObjectWithTag("Player").transform.position, playerWeaponFireRange);
    }
    #endregion
}
