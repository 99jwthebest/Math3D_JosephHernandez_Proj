using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    public bool GameIsPaused = false;
    public float timeScaleValue;
    public GameObject pauseMenuUI;
    public GameObject endLevelMenu;
    public GameObject ProjectileToDestroy;
    public TextMeshProUGUI endLevelText;
    public bool pressedCalculateButton;
    public GameObject equationImages;
    public bool calculatedAngle;
    public bool calculatedVelocity;



    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;

        SceneManager.LoadScene(0);
    }

    public void LoadMenu()
    {
        //WaitToLoadMenu();
        SceneManager.LoadScene("MainMenu");

    }
    public void WaitToLoadMenu()
    {
        Debug.Log("Main Menu, Before Wait");

        //yield return new WaitForSeconds(.1f);
        Debug.Log("Main Menu, Wait FOR.");
        //LoadScreen.SetActive(true);
        //yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MainMenu");

    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void ActivateEndLevelMenu()
    {
        if(!pressedCalculateButton)
        {
            endLevelText.text = "You Win!!\n" + "You Solved the Problem!!";
        }
        else
        {
            endLevelText.text = "You used the calculate button!\n" +
                                "Grab a Calculator!!\n" +
                                "Use the formula given and Try again!";
            if(calculatedAngle)
            {
                equationImages.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (calculatedVelocity)
            {
                equationImages.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        endLevelMenu.SetActive(true);
        ProjectileToDestroy = FindObjectOfType<Projectile>().gameObject;
    }
    public void DeactivateEndLevelMenu()
    {
        endLevelMenu.SetActive(false);
    }
}
