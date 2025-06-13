using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public static PlayerInventory Instance;

    public List<AllCollectables> InventoryItems = new List<AllCollectables>(); // Players inventory to store items

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(AllCollectables item)
    {
        if (!InventoryItems.Contains(item)) // Checks if the item is in the inventory already
        {
            InventoryItems.Add(item); // Adds the item to the inventory if not already present

        }
    } 

    

    public enum AllCollectables // all my collectables
    {
        Coins,
        MatchaBalls,
        KeyCard,
        GasMask,
        None // This is used to check if the player has no items in their inventory or for hazard behaviours that require no items
    }
}
