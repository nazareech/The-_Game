using Unity.Cinemachine.Samples;
using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject mainMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}
