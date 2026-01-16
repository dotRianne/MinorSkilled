using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private string URL_assets = "https://poly.pizza/bundle/Minor-Skilled---Good-Dino-Deeds-pgcE3hz4gI";
    private string URL_music = "https://www.youtube.com/watch?v=5LC3EgpXgZM";

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject tutorialMenu;

    public void ToCredits()
    {
        Debug.Log("ToCredits");
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }
    public void ToTutorial()
    {
        Debug.Log("ToTutorial");
        mainMenu.SetActive(false);
        tutorialMenu.SetActive(true);
    }
    public void ToMenu()
    {
        Debug.Log("ToMenu");
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
        tutorialMenu.SetActive(false);
    }

    public void OpenAssets()
    {
        Debug.Log("OpenAssets");
        Application.OpenURL(URL_assets);
    }
    public void OpenMusic()
    {
        Debug.Log("OpenMusic");
        Application.OpenURL(URL_music);
    }

    public void PlayGame()
    {
        Debug.Log("PlayGame");
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
}
