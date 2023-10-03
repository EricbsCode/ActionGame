using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
    public int itemCost;
    public bool itemRotate;
    public float amount;
    public enum rarity {legendary, rare, uncommon, common };
    public rarity itemRarity;

    public Sprite itemSprite;
    protected Inventory inventory;
    SpriteRenderer itemRend;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        itemRend = GetComponent<SpriteRenderer>();
        itemRend.sprite = itemSprite;
    }

    public virtual void Use()
    {

    }

    public virtual void Remove()
    {
        inventory.RemoveItem(this);
    }
}
