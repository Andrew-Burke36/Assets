using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    int CoinValue = 1;

    [SerializeField]
    AudioClip coinCollectAudio;
    MeshRenderer myMeshRenderer;

    [SerializeField]
    Material highlightMat;

    Material originalMat;

    public void CollectCoin(PlayerBehaviour player)
    {
        // Play audio sound
        AudioSource.PlayClipAtPoint(coinCollectAudio, transform.position);

        // Increases the players score by 1
        player.ModifyScore(CoinValue);

        // Destroys the coin
        Destroy(gameObject);
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
    }

    // Update is called once per frame
    void Update()
    {
        // Gives the coin a rotating animation
        transform.Rotate(0,0.2f,0);
    }
}
