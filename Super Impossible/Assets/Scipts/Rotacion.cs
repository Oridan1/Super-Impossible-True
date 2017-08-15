
using UnityEngine;

public class Rotacion : MonoBehaviour {

   
    public bool rotando;
    [SerializeField]
    float speed;

	// Update is called once per frame
	void Update () {
        if (rotando)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * speed);
        }
	}

    public void End()
    {
        rotando = false;
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        if (col)
        {
            col.enabled = !col.enabled;
        }
        else
        {
            BoxCollider2D box = GetComponent<BoxCollider2D>();
            if (box)
            {
                box.enabled = !enabled;
            }
        }
    }

    private void OnBecameInvisible()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = !renderer;
        End();
    }

}
