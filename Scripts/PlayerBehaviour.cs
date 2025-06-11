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
    public GameObject doorOverlay;

    [SerializeField]
    public GameObject collectableOverlay;

    float interactDistance = 2.5f;

    MatchaBallBehaviour currentMatchaBall;
    CoinBehaviour currentCoin;
    DoorBehaviour currentDoor;
    KeyBehaviour currentKey;

    int currentScore = 0;

    bool canInteract = false;

    private float lifetime = 2f;

    void OnInteract()
    {
        // Interact with the object
        if (canInteract)
        {
            // Interacting with coins in the game
            if (currentCoin != null)
            {
                Debug.Log("Collecting..");
                currentCoin.CollectCoin(this);
                canInteract = false;
                currentCoin = null;
            }

            // Interacting with doors in the game
            else if (currentDoor != null)
            {
                Debug.Log("Player is interacting with the door");
                currentDoor.Toggle();
            }

            // Interacting with the matcha ball in the game
            else if (currentMatchaBall != null)
            {
                Debug.Log("Player is interacting with the matcha ball");
                currentMatchaBall.CollectMatcha(this);
                canInteract = false;
                currentMatchaBall = null;
            }

            else if (currentKey != null)
            {
                Debug.Log("Player is interacting with the keycard");
                currentKey.CollectKey(this);
                canInteract = false;
                currentKey = null;
            }
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
        if (currentCoin != null)
        {
            currentCoin.Unhighlight();
            currentCoin = null;
        }

        if (currentMatchaBall != null)
        {
            currentMatchaBall.Unhighlight();
            currentMatchaBall = null;
        }

        if (currentDoor != null)
        {
            currentDoor = null;
        }

        if (currentKey != null)
        {
            currentKey = null;
        }

        collectableOverlay.SetActive(false);
        doorOverlay.SetActive(false);

        if (hitSomething)
        {
            GameObject hitObj = hitInfo.collider.gameObject;

            if (hitObj.CompareTag("Coin"))
            {
                currentCoin = hitObj.GetComponent<CoinBehaviour>();
                if (currentCoin != null)
                {
                    currentCoin.Highlight();
                    collectableOverlay.SetActive(true);
                    canInteract = true;
                }
            }
            else if (hitObj.CompareTag("matcha"))
            {
                currentMatchaBall = hitObj.GetComponent<MatchaBallBehaviour>();
                if (currentMatchaBall != null)
                {
                    currentMatchaBall.Highlight();
                    collectableOverlay.SetActive(true);
                    canInteract = true;
                }
            }
            else if (hitObj.CompareTag("KeycardDoor"))
            {
                currentDoor = hitObj.GetComponent<DoorBehaviour>();
                if (currentDoor != null)
                {
                    doorOverlay.SetActive(true);
                    canInteract = true;
                }
            }

            else
            {
                 if (hitObj.CompareTag("KeyCard"))
                {
                    currentKey = hitObj.GetComponent<KeyBehaviour>();
                     if (currentKey != null)
                     {
                         collectableOverlay.SetActive(true);
                         canInteract = true;
                    }
                }
            }
        }
    }
}



