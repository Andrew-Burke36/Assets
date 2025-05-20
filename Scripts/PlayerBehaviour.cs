using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerBehaviour : MonoBehaviour
{
    // Variables are here

    private int matchaHealth = 100;
    private int maxMatchaHealth = 100;
    int score = 0;
    
    bool canInteract = false;
    CoinBehaviour currentCoin;

    void OnInteract() {
        // Interact with the object
        if(canInteract) {
            Debug.Log("Collecting..");
            currentCoin.CollectCoin(this);
            Debug.Log("Score: " + score);
            canInteract = false;
            currentCoin = null;
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player collided with: " + other.gameObject.name);
        if(other.CompareTag("Coin")) {
            canInteract = true;
            currentCoin = other.gameObject.GetComponent<CoinBehaviour>();
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
