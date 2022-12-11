using Service.SystemBase;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        SystemTime.SetTime();
    }
}
