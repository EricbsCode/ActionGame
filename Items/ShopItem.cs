using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Item item;
    PlayerController player;
    Inventory inventory;
    SpriteRenderer rend;
    Text itemText;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        inventory = FindObjectOfType<Inventory>();
        rend = GetComponent<SpriteRenderer>();
        itemText = GetComponentInChildren<Text>();

        rend.sprite = item.itemSprite;

    }

    public void BuyItem()
    {
        if(player.money >= item.itemCost)
        {
            player.AddMoney(-item.itemCost);
            inventory.AddItem(item);
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        BuyItem();
    }

    private void Update()
    {
        itemText.text = item.itemName + "\n" + item.itemCost;
        itemText.color = player.money > item.itemCost ? Color.green : Color.red;
    }
}
