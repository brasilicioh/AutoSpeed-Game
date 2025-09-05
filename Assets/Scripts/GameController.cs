using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] private GameObject carro, rua, menu, moeda, colisor, spawner, gameOver;
    [SerializeField] private float intervaloMoeda, intervaloCollider;
    [SerializeField] private Sprite[] spritesColisores;
    private int money = 0;
    private BoxCollider2D boxColisor;
    private bool started, over;

    // Start is called before the first frame update
    void Start()
    {
        started = false;
        over = false;
        boxColisor = colisor.GetComponent<BoxCollider2D>();
        InvokeRepeating("SpawnMoeda", 0f, intervaloMoeda);
        InvokeRepeating("SpawnColisor", 2f, intervaloCollider);
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                started = true;
                over = false;
            }
        }
        else
        {
            menu.SetActive(false);
            carro.SetActive(true);
            rua.SetActive(true);
        }
        if (over && Input.GetKey(KeyCode.Return))
        {
            string[] prefabs = { "Colisor", "Moeda" };

            foreach (string tag in prefabs)
            {
                GameObject[] objetos = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject obj in objetos)
                {
                    Destroy(obj);
                }
            }
            setGameOver(false);
        }
    }

    void SpawnMoeda()
    {
        if (started)
        {
            Vector2 positionSpawn = spawner.transform.position;
            positionSpawn.y = Random.Range(-3.25f, 3.25f);
            // metodo que instancia o prefab; o objeto, a posicao, a rotacao padrao do negocio
            Instantiate(moeda, positionSpawn, Quaternion.identity);
        }
    }

    void SpawnColisor()
    {
        if (started)
        {
            int randomIndex = Random.Range(0, 4);
            switch (randomIndex)
            {
                case 0:
                    colisor.transform.localScale = new Vector3(0.01f, 0.01f, 0f);
                    boxColisor.size = new Vector2(10f, 10f);
                    break;
                case 1:
                    colisor.transform.localScale = new Vector3(-0.68f, 0.68f, 0f);
                    boxColisor.size = new Vector2(2.5f, 1.24f);
                    break;
                case 2:
                    colisor.transform.localScale = new Vector3(0.095f, 0.095f, 0f);
                    boxColisor.size = new Vector2(19.01f, 19.2f);
                    break;
                case 3:
                    colisor.transform.localScale = new Vector3(0.01f, 0.01f, 0f);
                    boxColisor.size = new Vector2(10f, 10f);
                    break;
            }
            colisor.GetComponent<SpriteRenderer>().sprite = spritesColisores[randomIndex];
            Vector2 positionSpawn = spawner.transform.position;
            positionSpawn.y = Random.Range(-3.25f, 3.25f);
            // metodo que instancia o prefab; o objeto, a posicao, a rotacao padrao do negocio
            Instantiate(colisor, positionSpawn, Quaternion.identity);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setMoney(int num)
    {
        money = num;
    }
    public int getMoney()
    {
        return money;
    }

    public void setGameOver(bool itsOver)
    {
        if (itsOver)
        {
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            over = true;
        }
        else
        {
            carro.transform.position = new Vector2(-6f, 0f);
            carro.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), 0);
            setMoney(0);
            started = false;
            over = false;
            gameOver.SetActive(false);
            menu.SetActive(true);
            carro.SetActive(false);
            rua.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
