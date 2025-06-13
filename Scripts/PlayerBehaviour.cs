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
    int currentScore = 0;

    // Allows the player to interact with objects
    bool canInteract = false;

    // Lifetime of the projectile
    private float lifetime = 2f;

    // Player Health system

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
        scoreText.text = "COLLECTIBLES: " + currentScore.ToString() + " / 20";

        // Sets the door and collectable overlay to inactive at the start of the game
        doorOverlay.SetActive(false);
        collectableOverlay.SetActive(false);
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



