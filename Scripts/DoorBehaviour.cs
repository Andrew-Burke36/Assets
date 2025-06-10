using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{   
    // Initial door variables
    private bool isOpen = false;

    public void Toggle() {
        if (!isOpen) {
            Open();
        }
        else {
            Close();
        }
    }
    public void Open() {

        Vector3 doorRotation = transform.eulerAngles;
        doorRotation.y -= 90f;
        transform.eulerAngles = doorRotation;
        isOpen = true;
    }

    public void Close()
    {
        Vector3 doorRotationclose = transform.eulerAngles;
        doorRotationclose.y += 90f;
        transform.eulerAngles = doorRotationclose;
        isOpen = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
