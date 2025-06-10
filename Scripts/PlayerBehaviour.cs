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

    int currentScore = 0;
    
    bool canInteract = false;
    CoinBehaviour currentCoin;
    DoorBehaviour currentDoor;


    void OnInteract() {
        // Interact with the object
        if(canInteract) {
            if (currentCoin != null) {
                Debug.Log("Collecting..");
                currentCoin.CollectCoin(this);
                Debug.Log("Score: " + currentScore);
                canInteract = false;
                currentCoin = null;
            }
            
            else if(currentDoor != null) {
                Debug.Log("Player is interacting with the door");
                currentDoor.Toggle();   
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player collided with: " + other.gameObject.name);
        if (other.CompareTag("Coin"))
        {
            canInteract = true;
            currentCoin = other.gameObject.GetComponent<CoinBehaviour>();
            currentCoin.Highlight();
        }
        else if (other.CompareTag("KeycardDoor"))
        {
            canInteract = true;
            currentDoor = other.gameObject.GetComponent<DoorBehaviour>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player has detected a coin 
        if (currentCoin != null)
        {
            // If the object that exited the trigger is the same as the current coin
            if (other.gameObject == currentCoin.gameObject)
            {
                canInteract = false;
                currentCoin.Unhighlight();
                currentCoin = null;
            }
        }
    }
    public void ModifyScore(int amount)
    {
        currentScore += amount;
        scoreText.text = "SCORE: " + currentScore.ToString();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnFire()
    {
        Debug.Log("Fire");
        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);

        // For the fire force of the bullet stuff
        Vector3 FireForce = spawnPoint.forward * fireStrength;

        // Get the rigidbody of the projectile and apply the force onto it
        newProjectile.GetComponent<Rigidbody>().AddForce(FireForce);
    }
    void Start()
    {
        scoreText.text = "SCORE: " + currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
