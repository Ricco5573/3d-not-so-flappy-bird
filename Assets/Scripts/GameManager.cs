using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance ;
    private float gameSpeed = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    public float GetGameSpeed() => gameSpeed; 


    public void StartGame()
    {
        gameSpeed = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameSpeed > 0)
        {
            gameSpeed += 0.001f;
        }
    }
}
