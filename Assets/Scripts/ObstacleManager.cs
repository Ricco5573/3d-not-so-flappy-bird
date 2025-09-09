using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> obstaclePrefabs = new List<GameObject>();
    private List<GameObject> obstacles = new List<GameObject>();
    [SerializeField]
    private Transform[] lanes = new Transform[2];
    private float obstacleSpeed = 0.5f;
    private float obstacleCooldown = 2.5f;
    private float obstacleTimer;

    //this class manages obstacles, their spawning, movement, and deletion.

    void Start()
    {
        obstacleTimer = obstacleCooldown;
        for (int i = 0; i < 2; i++)
        {
            GameObject obstacle = obstacles[Random.Range(0, obstacles.Count)];
            GameObject obj = Instantiate(obstacle, lanes[i]);
            obstacles.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject obstacle in obstacles)
        {
            obstacle.transform.position = new Vector3(obstacle.transform.position.x, obstacle.transform.position.y, obstacle.transform.position.z - obstacleSpeed);
        }
        obstacleTimer -= Time.deltaTime;
        if (obstacleTimer < 0)
        {
            obstacleTimer = obstacleCooldown;
            for(int i = 0; i < 3; i++)
            {
                GameObject obstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count - 1)];
                GameObject obj = Instantiate(obstacle, lanes[i]);
                obstacles.Add(obj);
            }
        }
    }
}
