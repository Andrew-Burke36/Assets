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
    
    int currentScore = 0;
    
    bool canInteract = false;

    private float lifetime = 2f;


    void OnInteract() {
        // Interact with the object
        if(canInteract) {
            if (currentCoin != null) {
                Debug.Log("Collecting..");
                currentCoin.CollectCoin(this);
                canInteract = false;
                currentCoin = null;
            }
            
            else if(currentDoor != null) {
                Debug.Log("Player is interacting with the door");
                currentDoor.Toggle();   
            }

            else if (currentMatchaBall != null )
            {
                Debug.Log("Player is interacting with the matcha ball");
                currentMatchaBall.CollectMatcha(this);
                canInteract = false;
                currentMatchaBall = null;
            }
        }
    }

    public void ModifyScore(int amount)
    {
        currentScore += amount;
        scoreText.text = "COLLECTIBLES: " + currentScore.ToString() + "/20";

    }

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
        scoreText.text = "COLLECTIBLES: " + currentScore.ToString() + "/20";

        // Sets the door and collectable overlay to inactive at the start of the game
        doorOverlay.SetActive(false);
        collectableOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        // Draws the ray
        Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.red);

        // Checks if the player's raycast hits something and stores the information
        bool hitSomething = Physics.Raycast(transform.position, transform.forward, out hitInfo, interactDistance);

        // Resets pre-existing stuff
        canInteract = false;

        // Disable any overlays or highlights

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
        
        collectableOverlay.SetActive(false);
        doorOverlay.SetActive(false);

        if (hitSomething)
        {
            GameObject hitObj = hitInfo.collider.gameObject;

            if ( hitObj.CompareTag('Coin'))
            {
                currentCoin.GetComponent<CoinBehaviour>();


            }
        }

    }
}


