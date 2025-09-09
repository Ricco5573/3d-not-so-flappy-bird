using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverScreen;
    private int score = 0;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    public void PlayerDeath()
    {
        gameOverScreen.SetActive(true);
    }

    public void Score()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public void Reset()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
