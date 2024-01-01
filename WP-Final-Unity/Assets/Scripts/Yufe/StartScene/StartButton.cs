using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Button changeSceneButton;
    public string targetSceneName = "GameScene"; // Set this in the Unity Editor to the name of the scene you want to load

    void Start()
    {
        // Add a listener to the change scene button's click event
        changeSceneButton.onClick.AddListener(ChangeSceneFunction);
    }

    void ChangeSceneFunction()
    {
        Debug.Log($"Changing to scene: {targetSceneName}");
        SceneManager.LoadScene(targetSceneName);
    }
}
