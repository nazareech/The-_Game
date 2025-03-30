using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    List<Item> items;
    public GameObject cellContainer;

    public KeyCode showInventory = KeyCode.Q;
    public KeyCode takeButton = KeyCode.E;
    public float distance = 10f;
    RaycastHit2D hit;
    RaycastHit hitItem;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Виключаємо інвентар при запуску гри
        cellContainer.SetActive(false);

        items = new List<Item>();
        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            items.Add(new Item());
        }
    }

    // Update is called once per frame
    void Update()
    {
        ToggleInventory();

        // Перевіряємо, чи гравець натиснув клавішу  і чи є пердмети для підбору
        if (Input.GetKeyDown(takeButton))
        {
            Physics2D.queriesStartInColliders = false;
            
            hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

            if (hit)
            {
                if (hitItem.collider.GetComponent<Item>())
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].id == 0)
                        {
                            items[i] = hitItem.collider.GetComponent<Item>();
                            DisplayItems();
                            Destroy(hitItem.collider.GetComponent<Item>().gameObject);
                            break;
                        }
                    }
                }
            }
        }
    }

    void ToggleInventory()
    {
        if (Input.GetKeyDown(showInventory))
        {
            if (cellContainer.activeSelf)
            {
                cellContainer.SetActive(false);
            }
            else
            {
                cellContainer.SetActive(true);
            }
        }
    }

    void DisplayItems()
    {

        for(int i = 0; i < items.Count; i++)
        {
            Transform cell = cellContainer.transform.GetChild(i);
            Transform icon = cell.GetChild(0);
            Image img = icon.GetComponent<Image>();

            if (items[i].id != 0)
            {
                img.enabled = true;
                img.sprite = Resources.Load<Sprite>(items[i].pathIcon);
            }
            else 
            {
                img.enabled = false;
                img.sprite = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}
