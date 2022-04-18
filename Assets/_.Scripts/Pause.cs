using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    StarterAssetsInputs _input;

    public static bool gamePaused = false;
    public GameObject pauseMenu;

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    public void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            if (gamePaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        _input.SetCursorState(false);
        gamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        _input.SetCursorState(true);
        gamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
