using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// By Mads.
/// Attach this script to a GameObject that will be picked up by the Player and added to their inventory.
/// </summary>

public class InventoryItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;
    public string useWith;
    public bool isCD;

    private void Awake()
    {
        itemName = gameObject.name;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AddToInventory();
            GameManager.instance.inventoryUpdated = false;
            gameObject.SetActive(false);
        }
    }

    public void AddToInventory()
    {
        if (GameManager.instance.Inventory.Count > 10)
        {
            Debug.Log("L");
        }
        else
        {
            GameManager.instance.Inventory.Add(new Item(itemName, itemIcon, useWith, isCD)); //Add to current inventory
            GameManager.instance.ItemsPickedUp.Add(new Item(itemName, itemIcon, useWith, isCD)); //Add to running items list
        }

        if (isCD)
        {
            //Get the number of the CD which corresponds with the Room Number
            //Should be the last character in the game object's name
            string cdNumberString = gameObject.name.Substring(2);
            int cdNumber = int.Parse(cdNumberString);
            //Bool stored in the game manager is set to true
            GameManager.instance.cdCollected[cdNumber-1] = true;
        }
        
    }
}
