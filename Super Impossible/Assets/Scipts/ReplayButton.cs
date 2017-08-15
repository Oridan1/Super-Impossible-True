using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour {

    private void Awake()
    {
        //StartCoroutine(Inicio());
        Button[] botones = GetComponentsInChildren<Button>();
        foreach (Button n in botones)
        {
            if (n.name == "BotonReplay")
            {
                n.onClick.AddListener(Replay);               
            }
            else
            {
                n.onClick.AddListener(Menu);              
            }
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(Camera.main.gameObject.GetComponent<Level>().level);
    }  
    
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    } 

    IEnumerator Inicio()
    {       
        Button[] botones = GetComponentsInChildren<Button>();
        foreach (Button n in botones)
        {
            if (n.name == "BotonReplay")
            {
                n.onClick.AddListener(Replay);
                n.enabled = false;
                yield return new WaitForSecondsRealtime(3);
                n.enabled = true;
            }
            else
            {
                n.onClick.AddListener(Menu);
                n.enabled = false;
                yield return new WaitForSecondsRealtime(3);
                n.enabled = true;
            }
        }
    }
}
