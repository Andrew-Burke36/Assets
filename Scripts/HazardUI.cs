/*
 * Author : Andrew John
 * Date of Creation : 15th June 2025
 * Script Function : HazardUI script that manages the user interface for hazards, including displaying hazard warnings and effects when the player enters the trigger zone.
*/


using UnityEngine;

public class HazardUI : MonoBehaviour
{
    [SerializeField]
    private GameObject hazardWarningUI; // Reference to the UI element that displays the hazard warning

    [SerializeField]
    private TMPro.TextMeshProUGUI hazardText; // Reference to the text component for displaying hazard messages

    [SerializeField]
    private string hazardMessage = ""; // Default message to display

    [SerializeField]
    private AudioClip HazardAudio; // Audio clip to play when the player enters the hazard area


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           if (hazardWarningUI != null && hazardMessage != null)
            {
                // Show the hazard warning UI when the player enters the hazard area
                hazardWarningUI.SetActive(true);
                hazardText.text = hazardMessage; // Set the hazard message text
                // Play the hazard audio if assigned
                if (HazardAudio != null)
                {
                    AudioSource.PlayClipAtPoint(HazardAudio, other.transform.position, 0.2f);
                }
            }
            else
            {
                Debug.LogWarning("Hazard Warning UI is not assigned in the inspector.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hazardWarningUI != null && hazardMessage != null)
            {
                // Show the hazard warning UI when the player enters the hazard area
                hazardWarningUI.SetActive(false);
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hazardWarningUI.SetActive(false); // Ensure the hazard warning UI is hidden at the start
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
