using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> roomPrefabs = new List<GameObject>();
    private List<GameObject> removalQueue = new List<GameObject>(); 
    private List<GameObject> rooms = new List<GameObject>();
    private GameManager gameManager;
    private float removalTimer = 0;
    [SerializeField]
    private PlayerController controller;
    [SerializeField]
    private UIManager uiMan;
    [SerializeField]
    private Transform spawnPos;
    [Header("Obstacle settings")]
    [SerializeField]
    private float obstacleSpeed = 0.5f;
    [SerializeField]
    private float obstacleCooldown = 2.2f;
    private float obstacleTimer = 0;

    //this class manages obstacles, their spawning, movement, and deletion.

    void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        for (int j = 0; j <= 3; j++)
        {

                Debug.Log("Spawning");
                float spawnDistance = spawnPos.position.z - controller.transform.position.z;
                Vector3 spawnPosition = new Vector3(spawnPos.position.x, spawnPos.position.y,  spawnDistance  * (0.25f * j));
                GameObject obstacle = roomPrefabs[Random.Range(0, roomPrefabs.Count - 1)];
                GameObject obj = Instantiate(obstacle, spawnPosition, Quaternion.identity);
                rooms.Add(obj);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject obstacle in rooms)
        {
            obstacle.transform.position = new Vector3(obstacle.transform.position.x, obstacle.transform.position.y, (obstacle.transform.position.z - obstacleSpeed * Time.deltaTime * gameManager.GetGameSpeed()));
            if(obstacle.transform.position.z < controller.transform.position.z - 100)
            {
                removalQueue.Add(obstacle);
            }
        }
        if (removalQueue.Count > 0)
        {
            int removalcount = removalQueue.Count;
            uiMan.Score();
            for (int i = 0; i <= removalcount - 1; i++)
            {
                GameObject obj = removalQueue[i];
                rooms.Remove(obj);
                Destroy(obj);
                Debug.Log(removalQueue.Count + i);


            }
            removalQueue.Clear();
            removalTimer = 0;
        }
        removalTimer += Time.deltaTime;
        obstacleTimer -= Time.deltaTime * gameManager.GetGameSpeed();
        if (obstacleTimer < 0)
        {
            obstacleTimer = obstacleCooldown;
            GameObject obstacle = roomPrefabs[Random.Range(0, roomPrefabs.Count - 1)];
            GameObject obj = Instantiate(obstacle, spawnPos.position, Quaternion.identity);
            rooms.Add(obj);
        }
    }
}
