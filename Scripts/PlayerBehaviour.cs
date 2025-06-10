using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerBehaviour : MonoBehaviour
{
    // Variables are here
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    float fireStrength = 100f;

    private int matchaHealth = 100;
    private int maxMatchaHealth = 100;
    int score = 0;
    
    bool canInteract = false;
    CoinBehaviour currentCoin;
    DoorBehaviour currentDoor;


    void OnInteract() {
        // Interact with the object
        if(canInteract) {
            if (currentCoin != null) {
                Debug.Log("Collecting..");
                currentCoin.CollectCoin(this);
                Debug.Log("Score: " + score);
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
        if(other.CompareTag("Coin")) {
            canInteract = true;
            currentCoin = other.gameObject.GetComponent<CoinBehaviour>();
        }
        else if(other.CompareTag("KeycardDoor")) {
            canInteract = true;
            currentDoor = other.gameObject.GetComponent<DoorBehaviour>();
        }


    }
    public void ModifyScore(int amount) {
        score += amount;
    }

    // void OnCollisionEnter(Collision collision)
    // {

    //     if (collision.gameObject.CompareTag("matcha"))
    //     {
    //         matchaHealth++;
    //         Debug.Log("Matcha health: " + matchaHealth);
    //     }
    //     else if (collision.gameObject.CompareTag("coffee")){
    //         matchaHealth--;
    //         Debug.Log("Matcha health: " + matchaHealth);
    //     }
    //     else {
    //         Debug.Log("Player collided with a non-matcha object");
    //     }
    // }

    // void OnCollisionEnter(Collision collision) {
    //     if (collision.gameObject.CompareTag("matcha_boost")) {
    //         Debug.Log("player collided with a matcha boost object");

    //         RecoveryBehaviour recover = collision.gameObject.GetComponent<RecoveryBehaviour>();
    //     }
    // }

    // public void ModifyHealth(int amount) {
    //     if(matchaHealth < maxMatchaHealth) {
    //         matchaHealth += amount;
    //         if (matchaHealth > maxMatchaHealth) {

    //             matchaHealth = maxMatchaHealth;
    //         }
    //     } 
    // }

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
