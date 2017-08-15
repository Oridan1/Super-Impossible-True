
using UnityEngine;

public class FastEnemy : MonoBehaviour {

    private bool visib;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool horizontal;
    [SerializeField]
    private bool vertical;
    private Vector3 positionAux;

    private void Awake()
    {
        visib = false;
        //speed =  5;
    }

    // Update is called once per frame
    void Update () {
        if (visib)
        {
            if (horizontal)
            {
                MoveX(transform, speed * Time.deltaTime);
            }
            if (vertical)
            {
                MoveY(transform, speed * Time.deltaTime);
            }
        }
	}

    private void OnBecameVisible()
    {
        visib = !visib;
    }

    void MoveX(Transform obj, float cantidad)
    {
        positionAux = obj.position;
        positionAux.x += cantidad;
        obj.position = positionAux;
    }

    void MoveY(Transform obj, float cantidad)
    {
        positionAux = obj.position;
        positionAux.y += cantidad;
        obj.position = positionAux;
    }

    public void End()
    {
        visib = false;
    }
    private void OnBecameInvisible()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = !enabled;
        End();
    }
}
