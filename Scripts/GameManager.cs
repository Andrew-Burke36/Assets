using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public HealthBehaviour playerHealth = new HealthBehaviour(100, 100);

    void Awake()
    {
        if ( gameManager != null && gameManager != this )
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }
}
