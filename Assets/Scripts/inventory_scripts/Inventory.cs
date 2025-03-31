using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using JetBrains.Annotations;
using UnityEditor.Search;

public class Inventory : MonoBehaviour
{
    [Header("Main inventory setings")]
    public KeyCode openInventory = KeyCode.Q;
    public int itemsInStack = 32;

    [Header("Linking objects")]
    public DataBase data;

    public List<ItemInventory> items = new List<ItemInventory>();

    public GameObject gameObjShow;

    public GameObject inventoryMainObject;

    [Header("Number of cells")]
    public int maxCount;

    [Header("Setings for display")]
    public Camera cam;
    public EventSystem es;

    public int currentID;
    public ItemInventory currentItem;

    [Header("Object to display the movement")]
    public RectTransform movingObject;
    public Vector3 offset;

    [Header("Object to hide inventory")]
    public GameObject backGround;

    // Додаємо публічну властивість для перевірки стану
    public static bool IsInventoryOpen { get; private set; }

    private void Start()
    {
        // Ініціалізуємо CanvasGroup один раз для плавності
        CanvasGroup cg = movingObject.GetComponent<CanvasGroup>();
        if (cg == null) cg = movingObject.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 0.8f;
        cg.blocksRaycasts = false;

        if (items.Count == 0)
        {
            AddGraphics();
        }

        /*// Заповнення інвентаря рандомними елементами
        for(int i = 0; i < maxCount; i++)
        {
            AddItem(i, data.items[Random.Range(0, data.items.Count)], Random.Range(1, itemsInStack));
        }
        UpdateInventory();
*/
        // Вимикаємо інвентарь при запуску гри
        backGround.SetActive(false);
    }

    public void Update()
    {
        if (currentID != -1)
        {
            MoveObject();
        }

        if (Input.GetKeyDown(openInventory))
        {
            backGround.SetActive(!backGround.activeSelf);
            IsInventoryOpen = backGround.activeSelf; // Оновлюємо стан

            if (backGround.activeSelf)
            {
                UpdateInventory();
            }
        }
    }

    public void SearchForSameItem(Item item, int count)
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id == item.id)
            {
                if (items[0].count < itemsInStack)
                {
                    items[i].count += count;
                    if (items[i].count > itemsInStack)
                    {
                        count = items[i].count - itemsInStack;
                        items[i].count = itemsInStack / 2;
                    }
                    else
                    {
                        count = 0;
                        i = maxCount;
                    }
                }
            }
        }

        if (count > 0)
        {
            for (int i = 0; i < maxCount; i++)
            {
                if (items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i = maxCount;
                }
            }
        }
    }

    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObject.GetComponent<Image>().sprite = data.items[item.id].img;

        if(count > 1 && item.id != 0)
        {
            items[id].itemGameObject.GetComponentInChildren<TextMeshProUGUI>().text = items[id].count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    public void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemGameObject.GetComponent<Image>().sprite = data.items[invItem.id].img;

        if (invItem.count > 1 && invItem.id != 0)
        {
            items[id].itemGameObject.GetComponentInChildren<TextMeshProUGUI>().text = invItem.count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    public bool AddItemToInventory(Item item, int count)
    {
        // Спочатку шукаємо існуючий стак
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id == item.id && items[i].count < itemsInStack)
            {
                int spaceLeft = itemsInStack - items[i].count;
                if (count <= spaceLeft)
                {
                    items[i].count += count;
                    UpdateInventory();
                    return true;
                }
                else
                {
                    items[i].count = itemsInStack;
                    count -= spaceLeft;
                }
            }
        }

        // Потім шукаємо вільний слот
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id == 0)
            {
                AddItem(i, item, Mathf.Min(count, itemsInStack));
                return true;
            }
        }

        return false;
    }

    public void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjShow, inventoryMainObject.transform) as GameObject;

            newItem.name = i.ToString();

            ItemInventory ii = new ItemInventory();
            ii.itemGameObject = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0,0,0);
            rt.localScale = new Vector3(1,1,1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1,1,1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            items.Add(ii);
        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < maxCount; i++)
        {

            Image img = items[i].itemGameObject.GetComponent<Image>();
            TextMeshProUGUI text = items[i].itemGameObject.GetComponentInChildren<TextMeshProUGUI>();


            img.sprite = data.items[items[i].id].img;

            if (items[i].id != 0 && items[i].count > 1)
            {
                text.text = items[i].count.ToString();
            }
            else
            {
                text.text = "";
            }

        }
    }

    public void SelectObject()
    {
        if(currentID == -1)
        {
            currentID = int.Parse( es.currentSelectedGameObject.name);

            // Не дозволяємо перетягувати пусті слоти
            if (items[currentID].id == 0)
            {
                currentID = -1;
                return;
            }

            currentItem = CopyInventoryItem(items[currentID]);
            movingObject.gameObject.SetActive(true);

            movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;

            // Очищаємо вихідний слот
            items[currentID].id = 0;
            items[currentID].count = 0;
            UpdateInventory();
        }
        else
        {
            ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];

            if (currentItem.id != II.id)
            {
                 AddInventoryItem(currentID, II);

                AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
            }
            else
            {
                if (II.count + currentItem.count <= itemsInStack)
                {
                    II.count += currentItem.count;
                }
                else
                {
                    AddItem(currentID, data.items[II.id], II.count + currentItem.count - itemsInStack);
                    II.count = itemsInStack;
                }

                // Викликаємо UpdateInventory замість прямого оновлення тексту
                UpdateInventory();

                //II.itemGameObject.GetComponentInChildren<TextMeshProUGUI>().text = II.count.ToString();
            }
           
            currentID = -1;
            movingObject.gameObject.SetActive(false);
        }
    }

    public void MoveObject()
    {
        if (currentID == -1 || movingObject == null) return;

        // Отримуємо позицію курсора в координатах канвасу
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            inventoryMainObject.GetComponent<RectTransform>(),
            Input.mousePosition,
            cam,
            out localPoint
        );

        // Встановлюємо позицію перетягуваного об'єкта
        movingObject.localPosition = localPoint + new Vector2(offset.x, offset.y);

        // Додаткова перевірка видимості
        if (!movingObject.gameObject.activeSelf)
        {
            movingObject.gameObject.SetActive(true);
        }

        // Оновлюємо зображення (на випадок змін)
        movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;
        movingObject.GetComponent<Image>().color = Color.white;
    }

    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();

        New.id = old.id;
        New.itemGameObject = old.itemGameObject;
        New.count = old.count;

        return New;
    }
}

[System.Serializable]

public class ItemInventory
{
    public int id;
    public GameObject itemGameObject;

    public int count;
}
