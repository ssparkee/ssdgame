using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject cameraObject;
    public AudioClip jumpscare;
    AudioSource audioListener;
    // Start is called before the first frame update
    void Start()
    {
        audioListener = cameraObject.GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            audioListener.PlayOneShot(jumpscare);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayTutorial()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void MenuMain()
    {
        SceneManager.LoadScene(0);
    }
}
