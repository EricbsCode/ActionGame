using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    [SerializeField]
    int iSlot = 0;
    [SerializeField]
    int nextSlot = 0;
    [SerializeField]
    bool rotate = false;

    public SpriteRenderer parentRend;


    public void Awake()
    {

        nextSlot = iSlot;
    }

    public void Update()
    {
        if(rotate)
        {
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if(transform.localScale.x != 1)
                angle += 180;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * 10f);
        }

        transform.localScale = new Vector3(parentRend.flipX ? -1:1, 1, 1);

        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(items.Count != 0)
            {
                EquipItem(nextSlot);
            }
            else
            {
                Debug.Log("No Item In Inventory");
            }
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            if(items[iSlot] != null)
            {
                items[iSlot].Use();
            } 
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            if(items[iSlot] != null)
            {
                RemoveItem(items[iSlot]);
            } 
        }
    }

    public void AddItem(Item item)
    {
        Item newItem = Instantiate(item);
        newItem.transform.SetParent(transform);
        newItem.transform.localPosition = Vector3.zero;
        newItem.transform.localRotation = Quaternion.identity;
        newItem.transform.localScale = new Vector3(1, 1, 1);
        items.Add(newItem);
        newItem.gameObject.SetActive(false);
    }
    
    public void EquipItem(int slot)
    {
        if(items.Count != 0)
        {
            items[iSlot % items.Count].gameObject.SetActive(false);
            iSlot = slot % items.Count;
            items[iSlot].gameObject.SetActive(true);
            transform.rotation = Quaternion.Euler(0 ,0 ,0);
            rotate = items[iSlot].itemRotate;
            nextSlot = (iSlot + 1) % items.Count;
        }

    }

    public void RemoveItem(Item item)
    {
        if(items.Count != 0)
        {
            items.Remove(item);
            item.gameObject.SetActive(false);
        }

    }

}
