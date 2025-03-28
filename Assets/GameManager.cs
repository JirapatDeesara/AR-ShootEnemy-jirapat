using NUnit.Framework;
using System.Collections.Generic; // Collect spawn enemy
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Random = UnityEngine.Random; // random 
using System.Collections;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private ARSession _arSession;
    [SerializeField] private ARPlaneManager _planeManager;
    //W11
    [SerializeField] private UImanager uiManager;
    [SerializeField] protected GameObject enemyPrefab;

    [Header("Enemy Settings")]
    [SerializeField] private int enemyCount = 2;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float deSpawnRate = 4f;

    private List<GameObject> _spawnEnemies = new List<GameObject>();
    private int _score = 0;

    private bool _gameStarted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // _arSession = FindFirstObjectByType<ARSession>();
        UImanager.OnUIStartButtonPressed += StartGame;
        UImanager.OnUIRestartButtonPressed += RestartGame;
    }

    void StartGame()
    { 
     if (_gameStarted) return;
     _gameStarted = true;   

     _planeManager.enabled = false; // disable plane generation
        foreach (var plane in _planeManager.trackables)
        {
            var meshVisualize = plane.GetComponent<ARPlaneMeshVisualizer>();
            if (meshVisualize) meshVisualize.enabled = false;

            var lineVisualize = plane.GetComponent<LineRenderer>();
            if(lineVisualize) lineVisualize.enabled = false;
        }// End Foreach loop
      
        StartCoroutine(SpawnEnemies());
    }//end StartGame

    void RestartGame()
    { 
     _gameStarted=false;
     _planeManager.enabled = true;
     _arSession.Reset();

        _score = 0;
        uiManager.UpdateScore(_score);

        foreach (var enemy in _spawnEnemies)
        { 
         Destroy(enemy);
        }
        _spawnEnemies.Clear();
    }//End RestartGame method

    void SpawnEnemy()
    {
        if (_planeManager.trackables.count == 0) return;
        List<ARPlane> planes = new List<ARPlane>();
        foreach (var plane in _planeManager.trackables) 
        { 
        planes.Add(plane);
        }// found floor! 
        var randomPlane = planes[Random.Range(0, planes.Count)];
        var randomPlanePosition = GetRandomPosition(randomPlane);

        var enemy = Instantiate(enemyPrefab, randomPlanePosition, Quaternion.identity);
        _spawnEnemies.Add(enemy); 
        
        var enemyScript = enemy.GetComponentInChildren<EnemyScript>();
        if (enemyScript != null) 
        {
            
            enemyScript.OnEnemyDestroyed += AddScore;
            
        }// End if

        StartCoroutine(DespawnEnemies(enemy));
    }//End spawnEnemy

    Vector3 GetRandomPosition( ARPlane plane)
    {
        var center = plane.center;
        var size = plane.size * 0.5f;
        var randomX = Random.Range(-size.x, size.x);
        var randomZ = Random.Range(-size.y, size.y);
        return new Vector3(center.x + randomX, center.y,center.z + randomZ);
    }// GetRandomPosition
    IEnumerator SpawnEnemies()
    {
        while (_gameStarted)
        {
            if (_spawnEnemies.Count < enemyCount)
            {
                SpawnEnemy();
            }// end if loop
            yield return new WaitForSeconds(spawnRate);
        }// End while loop
    }// End SpawnEnemies

    IEnumerator DespawnEnemies(GameObject enemy)
    {
       yield return new WaitForSeconds(deSpawnRate);
        if (_spawnEnemies.Contains(enemy)) 
        {
            _spawnEnemies.Remove(enemy);
            Destroy(enemy);
        }
    }// DespawnEnemies

     void AddScore()
    {
        _score++;
        uiManager.UpdateScore(_score);
      
    }
}// GameManager
