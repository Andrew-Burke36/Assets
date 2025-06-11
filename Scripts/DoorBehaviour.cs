using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    // Initial door variables
    [SerializeField] PlayerInventory.AllCollectables RequiredItem;


    private bool isDoorOpen = false;

    [SerializeField]
    AudioSource doorAudioSource;

    public void Toggle()
    {
        if (HasKey(RequiredItem))
        {
            if (!isDoorOpen)
            {
                Open();
                doorAudioSource.Play();
            }

            else
            {
                Close();
            }
        } 
    }
    public void Open() {

        Vector3 doorRotation = transform.eulerAngles;
        doorRotation.y -= 90f;
        transform.eulerAngles = doorRotation;
        isDoorOpen = true;
    }

    public void Close()
    {
        Vector3 doorRotationclose = transform.eulerAngles;
        doorRotationclose.y += 90f;
        transform.eulerAngles = doorRotationclose;
        isDoorOpen = false;
    }

    public bool HasKey(PlayerInventory.AllCollectables RequiredItem)
    {
        if (PlayerInventory.Instance.InventoryItems.Contains(RequiredItem))
        {
            return true;
        }

        else
        {
            return false;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
