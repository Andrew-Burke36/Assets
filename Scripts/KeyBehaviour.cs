using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    [SerializeField] PlayerInventory.AllCollectables ItemType; // Stores the type of item

    [SerializeField] AudioClip KeyCollectAudio;


    public void CollectKey(PlayerBehaviour player)
    {
        // Adds the key to the player's inventory
        PlayerInventory.Instance.AddItem(ItemType);
        
        AudioSource.PlayClipAtPoint(KeyCollectAudio, transform.position); // Plays the key collect audio

        // Destroys the key object
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 0.2f);
    }
}
