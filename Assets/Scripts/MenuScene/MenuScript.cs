using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public Canvas quitMenu;
	public Canvas helpMenu;
	public Button okText;
    public Button startText;
    public Button exitText;



	// Use this for initialization
	void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
		helpMenu = helpMenu.GetComponent<Canvas> ();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        quitMenu.enabled = false;
		helpMenu.enabled = false;
		okText = okText.GetComponent<Button> ();
	}

	public void HelpPress ()
	{
		helpMenu.enabled = true;
	}

	public void okPress ()
	{
		helpMenu.enabled = false;
	}

    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }
		
    public void NoPress()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }

    public void StartLevel()
    {
      
		SceneManager.LoadScene("main");
    }


	public void StartMenu()
	{
		
		SceneManager.LoadScene("menu");
	}
	public void StartNight()
	{
		
		SceneManager.LoadScene("scene2");
	}

	public void DesertDay()
	{
		
		SceneManager.LoadScene("DesertDay");
	}
	public void DesertNight()
	{
		
		SceneManager.LoadScene("DesertNight");
	}
	public void Volcano()
	{
		
		SceneManager.LoadScene("Volcano");
	}
	public void Otringal()
	{
		SceneManager.LoadScene("Otringal");
	}

	public void OtringalNight()
	{
		SceneManager.LoadScene("OtringalNight");
	}
	public void lobb()
	{
		SceneManager.LoadScene("lobb");	
	}
    public void ExitGame()
    {
        Application.Quit();
    }
}
