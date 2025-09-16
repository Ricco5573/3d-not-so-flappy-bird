using UnityEngine;

public class FOVScaling : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;
    [Header("FOV settings")]
    [SerializeField]
    private float startingFOV = 60f;
    [SerializeField]
    private float fovScaling = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetGameSpeed() != 0)
        {
            cam.fieldOfView = (startingFOV - fovScaling) + (gameManager.GetGameSpeed() * fovScaling);
        }
        else cam.fieldOfView = startingFOV;
    }
}
