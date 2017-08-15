using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    float jumpPower;
    [SerializeField]
    float speed;
    [SerializeField]
    float gravity;
    [SerializeField]
    float aceleration;
    [SerializeField]
    float jumpCap;
    [SerializeField]
    float gravityCap;
    [SerializeField]
    float porcentaje;

    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Transform partics;
    [SerializeField]
    GameObject replayButton;
    [SerializeField]
    GameObject meta;
    [SerializeField]
    GameObject linea;
    [SerializeField]
    GameObject botonMenu;
    [SerializeField]
    GameObject empty;

    [SerializeField]
    Text txtPorcentaje;
    [SerializeField]
    Text txtScore;
    [SerializeField]
    Text txtBest;
    [SerializeField]
    Text endText;
    [SerializeField]
    Text monedasTxt;
    [SerializeField]
    Image panel;
    [SerializeField]
    Image imagen;

    [SerializeField]
    Sprite muerteSprite;
    [SerializeField]
    Sprite winSprite;

    [SerializeField]
    Saves saves;
    [SerializeField]
    int level;

    [SerializeField]
    float[] bestScore;
    [SerializeField]
    float[] bestPosition;
    [SerializeField]
    int[] setMonedas;
    [SerializeField]
    int[] maxMonedas;

    [SerializeField]
    Animator anim;
    [SerializeField]
    Animator anim2;
    [SerializeField]
    Canvas canvas;

    Vector3 positionAux;
    float distInicial;
    float distActual;
    float tiempo;
    [SerializeField]
    int monedas;
    [SerializeField]
    bool testing;
    bool vivo;
    bool dying;

    [SerializeField]
    private EnemyController enemyScript;

    public float[] BestScore()
    {
        return bestScore;
    }

    public float[] BestPosition()
    {
        return bestPosition;
    }
    public int[] MaxMonedas()
    {
        return maxMonedas;
    }

    public int[] SetMonedas()
    {
        return setMonedas;
    }

    private void Awake()
    {
        saves.Load(this);              
        distInicial = meta.transform.position.x;
        GameObject o = Instantiate(linea, Vector3.zero, Quaternion.identity);
        MoveX(o.transform, bestPosition[level]);
        vivo = false;
        dying = false;
    }

    // Use this for initialization
    void Start () {
        if (level > 0)
        {
            saves.LoadMonedas(this);
            GameObject instance = Instantiate(Resources.Load("Monedas/Monedas" + level.ToString() + "_" + (setMonedas[level]).ToString(), typeof(GameObject))) as GameObject;
            GameObject obs = Instantiate(Resources.Load("Obstáculos/Obstáculos" + level.ToString(), typeof(GameObject))) as GameObject;
            enemyScript = obs.GetComponent<EnemyController>();
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (vivo)
        {
            if (Input.GetMouseButtonDown(0) && gravity <= jumpCap)
            {
                gravity += jumpPower;
            }
            if (gravity >= jumpCap)
            {
                gravity = jumpCap;
            }
            if (gravity >= gravityCap)
            {
                gravity -= Time.deltaTime * aceleration;
            }
            if (gravity <= gravityCap)
            {
                gravity = gravityCap;
            }
            MoveX(transform, speed * Time.deltaTime);
            MoveY(transform, gravity * Time.deltaTime);
            if (transform.position.y >= 5.3f || transform.position.y <= -3.1f)
            {
               StartCoroutine(Muerte());
            }
            distActual = transform.position.x;
            porcentaje = (100 / (distInicial / distActual));
            txtPorcentaje.text = porcentaje.ToString("F0") + "%";
            tiempo += Time.deltaTime;
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !dying)
            {
                vivo = !vivo;
            }
        }

	}

    private void LateUpdate()
    {
        if (vivo)
        {
            MoveX(mainCamera.transform, speed * Time.deltaTime);
            MoveX(partics, speed * Time.deltaTime);
            MoveY(partics, gravity * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            StartCoroutine(Muerte());
        }
        else if (collision.gameObject.CompareTag("Moneda"))
        {
            monedas++;
            collision.GetComponent<SpriteRenderer>().enabled = !enabled;
            collision.GetComponent<CircleCollider2D>().enabled = !enabled;
        }
    }

    void MoveX(Transform obj , float cantidad)
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

    IEnumerator Muerte()
    {
        vivo = !vivo;
        dying = !dying;
        enemyScript.End();
        if (porcentaje >= 99.6f)
        {
            GetComponent<SpriteRenderer>().sprite = winSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = muerteSprite;
        }
        if (porcentaje > bestScore[level])
        {            
            bestScore[level] = porcentaje;
            bestPosition[level] = transform.position.x;
            saves.Save(this);
        }
        if (level > 0 && monedas > maxMonedas[level])
        {
            if (porcentaje >= 99.6f || testing)
            {
                if (monedas > 4 && setMonedas[level] < 3)
                {
                    setMonedas[level]++;
                    maxMonedas[level] = 0;
                }
                else
                {
                    maxMonedas[level] = monedas;
                }
            }
            saves.SaveMonedas(this);         
        }
        yield return new WaitForSecondsRealtime(1);
        panel.enabled = true;
        imagen.enabled = true;
        anim.SetTrigger("Muerte");
        anim2.SetTrigger("Muerte");
        GameObject boton = GameObject.Instantiate(replayButton, replayButton.transform.position, Quaternion.identity);
        boton.transform.SetParent(canvas.transform, false);        
        txtPorcentaje.text = "";
        if (porcentaje >= 99.6f)
        {
            endText.text = "YOU WIN!!!!!!";
        }
        else
        {
            endText.text = "GAME OVER";
        }
        txtBest.text = "RECORD" + "\n" + bestScore[level].ToString("F0") + "%";
        txtScore.text = "SCORE" + "\n" + porcentaje.ToString("F0") + "%";
        monedasTxt.text = monedas + "/5 MONEDAS";        
        Destroy(gameObject);      
    }
}