using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class MainMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private VisualTreeAsset ui;
    private Button startButton;
    private Button quitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("Play");
        startButton.clicked += StartGame;
        quitButton = root.Q<Button>("Quit");
        quitButton.clicked += QuitGame;
        
    }
    private void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    private void QuitGame()
    {
        Application.Quit();
    }
}
