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

    float interactDistance = 2.5f;

    CoinBehaviour currentCoin;
    DoorBehaviour currentDoor;
    
    int currentScore = 0;
    
    bool canInteract = false;


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
        RaycastHit hitInfo;
        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * interactDistance, Color.red);

        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo, interactDistance))
        {
            if (hitInfo.collider.gameObject.CompareTag("Coin"))
            {
                if (currentCoin != null)
                {
                    // If the current coin is not null, unhighlight it
                    currentCoin.Unhighlight();
                }

                canInteract = true;
                currentCoin = hitInfo.collider.gameObject.GetComponent<CoinBehaviour>();
                currentCoin.Highlight();
            }

            else if (hitInfo.collider.gameObject.CompareTag("KeycardDoor"))
            {
                canInteract = true;
                currentDoor = hitInfo.collider.gameObject.GetComponent<DoorBehaviour>();
            }
        }

        else if (currentCoin != null)
        {
            // If the raycast does not hit a coin, unhighlight the current coin
            currentCoin.Unhighlight();
            currentCoin = null;
        }
    }


}
