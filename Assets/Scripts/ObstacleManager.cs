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
    private float removalTimer = 0;
    [SerializeField]
    private PlayerController controller;
    [SerializeField]
    private UIManager uiMan;
    [SerializeField]
    private Transform[] lanes = new Transform[2];
    private float obstacleSpeed = 0.5f;
    private float obstacleCooldown = 2.5f;
    private float obstacleTimer;

    //this class manages obstacles, their spawning, movement, and deletion.

    void Start()
    {
        obstacleTimer = obstacleCooldown;

        
        for (int j = 0; j <= 2; j++)
        {
            for (int i = 0; i <= lanes.Length -1; i++)
            {
                Debug.Log("Spawning");
                Vector3 spawnPos = lanes[i].position;
                float spawnDistance = spawnPos.z - controller.transform.position.z;
                spawnPos = new Vector3(spawnPos.x, spawnPos.y, spawnPos.z - (spawnDistance  / (j+1)) + 15);
                GameObject obstacle = roomPrefabs[Random.Range(0, roomPrefabs.Count - 1)];
                GameObject obj = Instantiate(obstacle, spawnPos, Quaternion.identity);
                rooms.Add(obj);
            }
        } 
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject obstacle in rooms)
        {
            obstacle.transform.position = new Vector3(obstacle.transform.position.x, obstacle.transform.position.y, obstacle.transform.position.z - obstacleSpeed);
            if(obstacle.transform.position.z < controller.transform.position.z)
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
        obstacleTimer -= Time.deltaTime;
        if (obstacleTimer < 0)
        {
            obstacleTimer = obstacleCooldown;
            for(int i = 0; i <= 2; i++)
            {
                GameObject obstacle = roomPrefabs[Random.Range(0, roomPrefabs.Count - 1)];
                GameObject obj = Instantiate(obstacle, lanes[i]);
                rooms.Add(obj);
            }
        }
    }
}
