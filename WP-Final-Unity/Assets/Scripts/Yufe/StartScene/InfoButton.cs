using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    public GameObject objectToShow; 

    void Start()
    {
        Button button = GetComponent<Button>();

        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(!objectToShow.activeSelf);
        }
    }
}
