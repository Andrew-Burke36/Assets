using UnityEngine;

public class MatchaBallBehaviour : MonoBehaviour
{
    int MatchaValue = 1;

    [SerializeField]
    AudioClip MatchaCollectAudio;
    
    MeshRenderer myMeshRenderer;

    [SerializeField]
    Material highlightMat;

    Material originalMat;

    public void CollectMatcha(PlayerBehaviour player)
    {
        // Play audio sound
        AudioSource.PlayClipAtPoint(MatchaCollectAudio, transform.position);

        // Increases the players score by 1
        player.ModifyScore(MatchaValue);

        // Destroys the matcha ball
        Destroy(gameObject);
    }

    public void Highlight()
    {
        // Changes the Matcha ball to the highlight colour 
        myMeshRenderer.material = highlightMat;
    }

    public void Unhighlight()
    {
        // Changes the color of the Matcha ball to its original colour
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
   
    }
}
