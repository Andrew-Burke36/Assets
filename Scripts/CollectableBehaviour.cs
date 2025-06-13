using UnityEngine;

public class CollectableBehaviour : MonoBehaviour
{
    // This class is for all the collectable items in the game

    int CollectableValue = 1;

    [SerializeField] PlayerInventory.AllCollectables ItemType; // Stores the type of item

    [SerializeField]
    AudioClip CollectAudio;
    MeshRenderer myMeshRenderer;

    [SerializeField]
    Material highlightMat;

    Material originalMat;

    float startY; // Used for the bobbing effect

    public void Collect(PlayerBehaviour player)
    {
        if (CompareTag("Collectable"))
        {
            // Adds the key to the player's inventory
                PlayerInventory.Instance.AddItem(ItemType);

            // Play audio sound
            AudioSource.PlayClipAtPoint(CollectAudio, transform.position);

            // Increases the players score by 1
            player.ModifyScore(CollectableValue);

            // Destroys the coin
            Destroy(gameObject);

        }
    }

    public void Highlight()
    {
        // Changes the coin to the highlight colour 
        myMeshRenderer.material = highlightMat;
    }

    public void Unhighlight()
    {
        // Changes the color of the coin to its original colour
        myMeshRenderer.material = originalMat;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Gets the mesh renderer of the coin
        myMeshRenderer = GetComponent<MeshRenderer>();

        // Stores the original colour of the coin
        originalMat = myMeshRenderer.material;

        startY = transform.position.y; // Store the initial Y position for bobbing effect
    }

    // Update is called once per frame
    void Update()
    {
        // Makes the collectable bob up and down
        transform.position = new Vector3(
            transform.position.x, startY + Mathf.Sin(Time.time) * 0.1f, 
            transform.position.z
        );

        // Rotate slowly around the Y axis
        transform.Rotate(0, 30f * Time.deltaTime, 0);
    }
}
