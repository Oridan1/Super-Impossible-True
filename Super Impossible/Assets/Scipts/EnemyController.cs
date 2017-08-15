using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField]
    Component[] componentes;

    private void Awake()
    {
        componentes = GetComponentsInChildren<Rotacion>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void End()
    {
        foreach (Rotacion script in componentes)
        {
            script.End();
            if (script.gameObject.GetComponent<FastEnemy>() != null)
            {
                script.gameObject.GetComponent<FastEnemy>().End();
            }
        }
    }
}
