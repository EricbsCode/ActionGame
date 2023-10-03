using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<ShopItem> shopItems = new List<ShopItem>();

    public List<ChestItem> chestItems = new List<ChestItem>();

    public List<ChestItem> commonItems = new List<ChestItem>();
    public List<ChestItem> uncommonItems = new List<ChestItem>();
    public List<ChestItem> rareItems = new List<ChestItem>();
    public List<ChestItem> legendaryItems = new List<ChestItem>();

    public GameObject[] spawnPoints;
    public float timeBetweenSpawns = 8f;
    float cooldown;

    public GameObject enemy;

    public List<GameObject> enemies;
    
    private void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");


        for(int i = 0; i < chestItems.Count; i++)
        {
            switch(chestItems[i].item.itemRarity)
            {
                case Item.rarity.common:
                    commonItems.Add(chestItems[i]);
                    break;
                case Item.rarity.uncommon:
                    uncommonItems.Add(chestItems[i]);
                    break;
                case Item.rarity.rare:
                    rareItems.Add(chestItems[i]);
                    break;
                case Item.rarity.legendary:
                    legendaryItems.Add(chestItems[i]);
                    break;
            }
        }

        cooldown = timeBetweenSpawns;
    }

    void Update()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
            SpawnEnemy();
    }


    public ShopItem GetRandomItem(List<ShopItem> l)
    {
        int index = Random.Range(0, l.Count);
        ShopItem item = l[index];
        return item;
    }

    public ChestItem GetRandomItem(List<ChestItem> l)
    {
        int index = Random.Range(0, l.Count);
        ChestItem item = l[index];
        return item;
    }

    void SpawnEnemy() 
    {
        GameObject obj = GetEnemy();
        obj.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        obj.SetActive(true);
        cooldown = timeBetweenSpawns;
    }

    GameObject GetEnemy()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if(!enemies[i].activeInHierarchy)
                return enemies[i];
        }
        GameObject en = Instantiate(enemy, transform.position, Quaternion.identity);
        enemies.Add(en);
        en.SetActive(false);
        return en;
    }
}
