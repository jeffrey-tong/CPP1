using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioClip pauseSound;

    [Header("Buttons")]
    public Button startButton;
    public Button settingsButton;
    public Button quitButton;
    public Button returnToMenuButton;
    public Button returnToGameButton;

    [Header("Menu")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    [Header("Text")]
    public Text volSliderText;
    public Text livesText;

    [Header("Slider")]
    public Slider volSlider;

    void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);

        if(volSlider && volSliderText)
        {
            float value;
            audioMixer.GetFloat("MasterVol", out value);
            volSlider.value = value + 80;
            volSliderText.text = Mathf.Ceil((value + 80)).ToString();
        }
    }

    void ShowMainMenu()
    {
        if (SceneManager.GetActiveScene().name == "Level")
        {
            SceneManager.LoadScene("Title");
        }
        else
        {
            settingsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void OnSliderValueChanged(float value)
    {
        if (volSliderText)
        {
            volSliderText.text = value.ToString();
            audioMixer.SetFloat("MasterVol", value - 80);
        }
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    void UpdateLifeText(int value)
    {
        livesText.text = value.ToString();
    }

    private void Start()
    {
        if (startButton)
        {
            startButton.onClick.AddListener(StartGame);
        }
        if (settingsButton)
        {
            settingsButton.onClick.AddListener(ShowSettingsMenu);
        }
        if (quitButton)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
        if (volSlider)
        {
            volSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
        if (returnToGameButton)
        {
            returnToGameButton.onClick.AddListener(ResumeGame);
        }
        if (returnToMenuButton)
        {
            returnToMenuButton.onClick.AddListener(ShowMainMenu);
        }
        if (livesText)
        {
            GameManager.instance.OnLifeValueChanged.AddListener(UpdateLifeText);
        }
    }

    private void Update()
    {
        if (!pauseMenu)
        {
            Time.timeScale = 1;
            return;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);

            if (pauseMenu.activeSelf)
            {
                GameManager.instance.playerInstance.GetComponent<AudioSourceManager>().PlayOneShot(pauseSound, false);
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
