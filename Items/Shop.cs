using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public float interactDistance;
    float distance;
    public GameObject shopParent;
    PlayerController player;

    GameController cont;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        cont = FindObjectOfType<GameController>();
        PopulateShop();
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if(distance <= interactDistance)
            shopParent.SetActive(true);
        else
            shopParent.SetActive(false);
    }

    public void  PopulateShop()
    {
        ShopItem shopItem;
        for(int i = 0; i < 3; i++)
        {
            shopItem = Instantiate(cont.GetRandomItem(cont.shopItems));
            shopItem.transform.SetParent(shopParent.transform);
            shopItem.transform.localPosition = new Vector3((i * 1.5f) - 1.5f, 0, 0);
        }
    }

}
