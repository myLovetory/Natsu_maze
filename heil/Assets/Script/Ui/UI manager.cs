using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UImanager : MonoBehaviour
{

    [Header ("Gameover")]
    [SerializeField] private GameObject game_Over_Screen;
    [SerializeField] private AudioClip gameover_Sound;

    [Header("pause")]
    [SerializeField] private GameObject pause_Screeen;

    [Header("complete")]
    [SerializeField] private GameObject complete_sceen;




    private void Awake()
    {
        game_Over_Screen.SetActive (false);
        pause_Screeen.SetActive (false);
        complete_sceen.SetActive (false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            click();
        }
    }
    #region game Over 
    public void Gameover()
    {
        game_Over_Screen.SetActive(true);
        Soundmanager.Instance.PlaySound(gameover_Sound);
        
    }

    // các hàm của gameover

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void Mainmenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();

        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    #endregion

    #region Pause_Game
    public void PauseGame(bool status)
    {
        pause_Screeen.SetActive(status);

        if(status ) 
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    private void click()
    {
        
            if(pause_Screeen.activeInHierarchy)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }
        
    }
    #endregion

    #region complete
    public void completed_level()
    {
        
        complete_sceen.SetActive(true);
        
    }

    #endregion

}
