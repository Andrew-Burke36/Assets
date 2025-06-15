/*
 * Author : Andrew John
 * Date of Creation : 11th June 2025
 * Script Function : DoorBehaviour script that handles door interactions, including opening, closing, and checking for required items to unlock doors.
 */

using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    // Initial door variables
    [SerializeField] PlayerInventory.AllCollectables RequiredItem;

    private Coroutine autoCloseCoroutine;

    private bool isDoorOpen = false;

    [SerializeField]
    AudioClip doorAudioOpen;

    [SerializeField]
    AudioClip doorAudioClose;

    [SerializeField]
    AudioClip doorAudioLocked;

    [SerializeField]
    private bool isExitDoor = false; // Used to determine if the door is an exit door

    public void Toggle()
    {
        // Exit door logic
        if (isExitDoor)
        {
            // Checks if the player has 20 collectables first
            PlayerBehaviour player = FindFirstObjectByType<PlayerBehaviour>();

            if (player != null && player.currentScore >= 20)
            {
                GameManager.gameManager.ShowsCongratsUI();
                return; // Exit door logic ends
            }

            else
            {
                AudioSource.PlayClipAtPoint(doorAudioLocked, transform.position);
                return; // Exit door logic ends
            }
        }

        if (HasKey(RequiredItem))
        {
            if (!isDoorOpen)
            {
                
                Open();
                AudioSource.PlayClipAtPoint(doorAudioOpen, transform.position);

                // Closes the door after 4 seconds if it is open
                if (autoCloseCoroutine != null)
                {
                    StopCoroutine(autoCloseCoroutine);
                }
                    autoCloseCoroutine = StartCoroutine(AutoCloseDoor());
            }

            else
            {
                AudioSource.PlayClipAtPoint(doorAudioClose, transform.position);
                Close();
            }
        }

        else
        {
            AudioSource.PlayClipAtPoint(doorAudioLocked, transform.position);
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

        // Stops the coroutine if the door was manually clsoed
        if (autoCloseCoroutine != null )
        {
            StopCoroutine(autoCloseCoroutine);
            autoCloseCoroutine = null;
        }
    }

    // Defines the duration of time for the auto close coroutine
    private System.Collections.IEnumerator AutoCloseDoor()
    {
        yield return new WaitForSeconds(4f);
        
        if (isDoorOpen)
        {
            AudioSource.PlayClipAtPoint(doorAudioClose, transform.position);
            Close();
        }
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
