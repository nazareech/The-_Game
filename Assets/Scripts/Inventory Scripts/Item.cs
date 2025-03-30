using UnityEngine;

public class Item : MonoBehaviour
{

    public string nameItem;
    public int id;
    [HideInInspector]
    public int countItem;
    public bool isStackable;
    [Multiline(5)]
    public string descriptionItem;
    public string pathIcon;
    public string pathPrefab;
}
