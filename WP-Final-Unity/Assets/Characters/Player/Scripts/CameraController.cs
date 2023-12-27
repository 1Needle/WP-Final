using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform WatchAt;
    
    void Update()
    {

        Vector3 playerForwardDirection = player.forward;

        transform.position = player.position -4 * playerForwardDirection;
        transform.position += new Vector3(0, 4f, 0);

        transform.LookAt(WatchAt);

    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }
}
