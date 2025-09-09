using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverScreen;
    public void PlayerDeath()
    {
        gameOverScreen.SetActive(true);
    }

    public void Reset()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
