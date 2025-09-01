using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] private GameObject moeda, colisor, spawner;
    [SerializeField] private float intervaloMoeda, intervaloCollider;
    [SerializeField] private Sprite[] spritesColisores;

    private int money = 0;
    // Start is called before the first frame update
    void Start()
    {
        string[] colisores = { "Buraco", "Carro", "Pessoa", "Cachorro" };
        InvokeRepeating("SpawnMoeda", 0f, intervaloMoeda);
        InvokeRepeating("SpawnColisor", 2f, intervaloCollider);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnMoeda()
    {
        Vector2 positionSpawn = spawner.transform.position;
        positionSpawn.y = Random.Range(-3.25f, 3.25f);
        // metodo que instancia o prefab; o objeto, a posicao, a rotacao padrao do negocio
        Instantiate(moeda, positionSpawn, Quaternion.identity);
    }

    void SpawnColisor()
    {
        int randomIndex = Random.Range(0, 4);
        colisor.GetComponent<SpriteRenderer>().sprite = spritesColisores[randomIndex];
        Vector2 positionSpawn = spawner.transform.position;
        positionSpawn.y = Random.Range(-3.25f, 3.25f);
        // metodo que instancia o prefab; o objeto, a posicao, a rotacao padrao do negocio
        Instantiate(colisor, positionSpawn, Quaternion.identity);
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
}
