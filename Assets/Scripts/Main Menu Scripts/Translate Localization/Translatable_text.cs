using UnityEngine;
using TMPro;

public class Translatable_text : MonoBehaviour
{

    public int textID;
    [HideInInspector] public TextMeshProUGUI UIText; // TMPro для UI
    //[HideInInspector] public Text UIText; // звичаний текст


    private void Awake()
    {
        UIText = GetComponent<TextMeshProUGUI>();
        //UIText = GetComponent<Text>(); // звичаний текст

        Translator.Add(this);
    }

    private void OnEnable()
    {
        Translator.Update_texts();
    }

    private void OnDestroy()
    {
        Translator.Delete(this);
    }
}
