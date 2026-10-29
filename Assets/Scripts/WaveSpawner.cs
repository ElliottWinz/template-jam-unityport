using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] TextMeshProUGUI ScoreText;

    public int waveNumber = 0;
    private int[] enemiesPerWave = { 4, 10, 15, 20, 25};
    public float dropRate = .1f;
    private List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        for(int i = enemies.Count - 1; i >= 0; i--)
        {
            if(enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
        if (enemies.Count == 0)
        {
            waveNumber++;
            ScoreText.text = "Wave: " + waveNumber;
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave[waveNumber-1]; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        enemies.Add(Instantiate(enemyPrefab, GetRandomPoint2D(), enemyPrefab.transform.rotation));
    }

    public Vector3 GetRandomPoint2D()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        Vector3 randomPosition = transform.position + new Vector3(randomDirection.x, randomDirection.y, 0f) * 14;

        return randomPosition;
    }
}
