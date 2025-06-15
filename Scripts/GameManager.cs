/*
 * Author : Andrew John
 * Date of Creation : 14th June 2025
 * Script Function : GameManger script that handles certain UI overlays, game flow logic, and player health management.
 */


using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    [SerializeField]
    private GameObject congratsUI; // UI to show when the player wins

    [SerializeField]
    public Vector3 respawnPointTransform;


    public HealthBehaviour playerHealth = new HealthBehaviour(100, 100);

    public void RestartGame()
    {
        // Reloads the current scene to restart the game
        Time.timeScale = 1f; // Resets time scale to normal
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor
        Cursor.visible = false; // Hides the cursor
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        Debug.Log("hi");
    }

    public void ResumeGame()
    {
        // Resumes the game by setting time scale to normal and hiding the congrats UI
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor
        Cursor.visible = false; // Hides the cursor
        if ( congratsUI != null )
        {
            congratsUI.SetActive(false);
        }
    }

    public void ShowsCongratsUI()
    {
       if ( congratsUI != null )
        {
            congratsUI.SetActive(true);
            Time.timeScale = 0f;

            // Shows the players cursour 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }

    void Awake()
    {
        if ( gameManager != null && gameManager != this )
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }

    private void Start()
    {
        // Initialize UI overlays
        if ( congratsUI != null )
        {
            congratsUI.SetActive(false);
        }
    }

}
