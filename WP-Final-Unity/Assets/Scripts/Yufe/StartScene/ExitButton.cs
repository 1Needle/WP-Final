using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public Button exitButton;

    void Start()
    {
        exitButton.onClick.AddListener(QuitGame);
    }

    void QuitGame()
    {
        Debug.Log("Quitting the game!");
        Application.Quit();
    }
}
