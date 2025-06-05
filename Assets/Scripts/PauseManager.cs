using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject cont; // Pause menü paneli
    [SerializeField] private int mainMenuScene = 0; // Ana menü sahnesinin adı

    private bool isPaused = false;

    void Start()
    {
        Debug.Log("PauseManager başlatıldı");
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    void Update()
    {
        // P tuşu kontrolü
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P tuşuna basıldı!");
            cont.SetActive(false);
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Oyunu durdur
            Time.timeScale = 0f;
            if (pauseMenuUI != null)
            {
                pauseMenuUI.SetActive(true);
                Debug.Log("Pause menüsü açıldı");
            }
        }
        else
        {
            // Oyunu devam ettir
            Time.timeScale = 1f;
            if (pauseMenuUI != null)
            {
                pauseMenuUI.SetActive(false);
                Debug.Log("Pause menüsü kapandı");
            }
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void showController()
    {
        cont.SetActive(true);
    }
    public void closeController()
    {
        cont.SetActive(false);
    }
    public void menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(8);
    }
    public void bastanBasla()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}
