using UnityEngine;

public class GiftBoxBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject gift;

    [SerializeField]
    Transform spawnPoint;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MatchaBullet"))
        {
            Quaternion OriginalPrefabRotation = gift.transform.rotation;
            GameObject newGift = Instantiate(gift, spawnPoint.position, OriginalPrefabRotation);

            
            Destroy(gameObject);
            GameObject bullet = collision.gameObject;
            Destroy(bullet);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
