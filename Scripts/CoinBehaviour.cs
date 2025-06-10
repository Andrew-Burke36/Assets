using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    int CoinValue = 1;
    public void CollectCoin(PlayerBehaviour player) {
        player.ModifyScore(CoinValue);
        Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0.2f,0);
    }
}
