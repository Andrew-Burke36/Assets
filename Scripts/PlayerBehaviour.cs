/*
 * Author : Andrew John
 * Date of Creation : 6th June 2025
 * Script Function : PlayerBehaviour script that handles player interactions, projectile firing, and score management.
 */


using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    // Variables are here
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    float fireStrength = 100f;

    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI healthText;

    [SerializeField]
    public GameObject doorOverlay;

    [SerializeField]
    public GameObject collectableOverlay;

    float interactDistance = 2.5f;

    DoorBehaviour currentDoor;
    CollectableBehaviour currentCollectable;

    // Player score
    public int currentScore = 0;

    // Allows the player to interact with objects
    bool canInteract = false;

    // Lifetime of the projectile
    private float lifetime = 2f;


    // Allows the player to interact with objects
    void OnInteract()
    {
        // Interact with the object
        if (canInteract)
        {
            // Interacting with collectables in the game
            if (currentCollectable != null)
            {
                Debug.Log("Collecting..");
                currentCollectable.Collect(this);
                canInteract = false;
                currentCollectable = null;
            }

            // Interacting with doors in the game
            else if (currentDoor != null)
            {
                Debug.Log("Player is interacting with the door");
                currentDoor.Toggle();
            }
        }
    }

    public void HealthModify(int amount)
    {
        int currentHealth = GameManager.gameManager.playerHealth.Health;
        healthText.text = "Health " + currentHealth.ToString() + " / " + GameManager.gameManager.playerHealth.MaxHealth.ToString();
    
        if ( currentHealth == 0 )
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        // Audio cue
        
        // Respawn point
        Vector3 respawnPoint = GameManager.gameManager.respawnPointTransform;

        // Disables the players character controller to prevent physics issues during teleportation
        CharacterController cc = GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            transform.position = respawnPoint;
            cc.enabled = true;
        }
        else
        {
            transform.position = respawnPoint;
        }

        // Resets the players collider so they wont get hurt by the previous environment
        Collider playerCollider = GetComponent<Collider>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
            Invoke(nameof(EnablePlayerCollider), 0.5f); // Re-enable after 0.5 seconds
        }

        // Resets the players health
        GameManager.gameManager.playerHealth.Health = GameManager.gameManager.playerHealth.MaxHealth;
        healthText.text = "Health " + GameManager.gameManager.playerHealth.Health.ToString() + " / " + GameManager.gameManager.playerHealth.MaxHealth.ToString();
    }

    private void EnablePlayerCollider()
    {
        Collider playerCollider = GetComponent<Collider>();
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }
    }

    // Modifys the player's score when they collect a collectable
    public void ModifyScore(int amount)
    {
        currentScore += amount;
        scoreText.text = "COLLECTIBLES: " + currentScore.ToString() + " / 20";

    }

    // Allows the user to fire a projectile
    void OnFire()
    {
        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);

        // For the fire force of the bullet stuff
        Vector3 FireForce = spawnPoint.forward * fireStrength;

        // Get the rigidbody of the projectile and apply the force onto it
        newProjectile.GetComponent<Rigidbody>().AddForce(FireForce);

        Destroy(newProjectile, lifetime);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast function for efficient and high level player interactions.
        RaycastHit hitInfo;
        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * interactDistance, Color.red);

        bool hitSomething = Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo, interactDistance);

        // Always reset
        canInteract = false;

        // Cleanup existing highlights and overlays
        if (currentCollectable != null)
        {
            currentCollectable.Unhighlight();
            currentCollectable = null;
        }

        if (currentDoor != null)
        {
            currentDoor = null;
        }
        collectableOverlay.SetActive(false);
        doorOverlay.SetActive(false);


        // Raycast checking to see if the raycast hits anything
        if (hitSomething)
        {
            GameObject hitObj = hitInfo.collider.gameObject;

            if (hitObj.CompareTag("Collectable"))
            {
                currentCollectable= hitObj.GetComponent<CollectableBehaviour>();
                if (currentCollectable != null)
                {
                    currentCollectable.Highlight();
                    collectableOverlay.SetActive(true);
                    canInteract = true;
                }
            }
            else
            {
                 if (hitObj.CompareTag("KeycardDoor"))
                {
                    currentDoor = hitObj.GetComponent<DoorBehaviour>();
                    if (currentDoor != null) {
                        doorOverlay.SetActive(true);
                        canInteract = true;
                    }
                }
            }
        }
    }
}



