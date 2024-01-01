using UnityEngine;
using UnityEngine.UI;

public class CloseInfoButton : MonoBehaviour
{
    public GameObject objectToHide;

    void Start()
    {
        Button button = GetComponent<Button>();

        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (objectToHide != null)
        {
            objectToHide.SetActive(false);
        }
    }
}
