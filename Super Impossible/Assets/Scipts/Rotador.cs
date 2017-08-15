using UnityEngine;

public class Rotador : MonoBehaviour {

    Component[] componentes;
    bool activo;

    private void Awake()
    {
        componentes = GetComponentsInChildren<Rotacion>();
        activo = false;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0) && !activo)
        {
            foreach (Rotacion rot in componentes)
            {
                rot.rotando = true;
            }
            activo = !activo;
        }
	}      
}
