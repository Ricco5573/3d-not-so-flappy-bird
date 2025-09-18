using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicSource;

    [Header("Songs")]
    [SerializeField]
    private AudioClip gameplayMusic;
    [SerializeField]
    private AudioClip gameOverMusic;

    private GameManager gameManager;
    private bool musicStarted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        musicSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetGameSpeed() > 0)
        {
            musicSource.pitch = 1 +  ((gameManager.GetGameSpeed()-1) / 6);
            if (!musicStarted)
            {
                musicSource.clip = gameplayMusic;
                musicSource.Play();
                musicStarted = true;
            }
        }
        else
        {
            musicSource.pitch = 1;
            musicSource.Stop(); 
        }
    }
}
