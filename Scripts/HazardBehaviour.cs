/*
 * Author : Andrew John
 * Date of Creation : 13th June 2025
 * Script Function : HazardBehaviour script that handles player interactions with hazards, including damage over time and audio feedback.
 */


using System.Collections;
using UnityEngine;

public class HazardBehaviour : MonoBehaviour
{
    [SerializeField]
    PlayerInventory.AllCollectables RequiredItem;

    [SerializeField]
    AudioClip waterSplashAudio;

    [SerializeField]
    AudioClip damageAudio;


    private Coroutine damageRoutine;


    [SerializeField]
    private int damageAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damageRoutine = StartCoroutine(DealDamageOverTime(other));

            if (gameObject.CompareTag("ElectricWater"))
            {
                // Play water splash sound
                AudioSource.PlayClipAtPoint(waterSplashAudio, other.transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (damageRoutine != null)
            {
                StopCoroutine(damageRoutine);
                damageRoutine = null;
            }
        }
    }

    IEnumerator DealDamageOverTime(Collider player)
    {
        var PlayerBehaviour = player.GetComponent<PlayerBehaviour>();
        var Inventory = PlayerInventory.Instance;

        while (true)
        {
            // stop damaging plaer if they are not in the trigger collider
            if (player == null || !GetComponent<Collider>().bounds.Intersects(player.bounds))
            {
                yield break; // Exit the coroutine if the player is not in the trigger collider
            }

            if ( Inventory.InventoryItems.Contains(RequiredItem))
            {
                // If the player does not have the required item, break out of the loop
                yield break; 
            }

            // Assuming the player has a method to take damage
            GameManager.gameManager.playerHealth.TakeDamage(damageAmount);
            
            // Update health text
            if ( PlayerBehaviour != null)
            {
                PlayerBehaviour.HealthModify(damageAmount);
            }

            // Play audio sound
            AudioSource.PlayClipAtPoint(damageAudio, player.transform.position);


            yield return new WaitForSeconds(1f); // Wait for 1 second before dealing damage again
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
