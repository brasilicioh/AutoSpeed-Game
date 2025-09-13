using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] private GameObject mainCamera;

    [SerializeField] private GameObject carro, rua, menu, moeda, colisor, spawner, gameOver;
    [SerializeField] private float intervaloMoeda, intervaloCollider;
    [SerializeField] private Sprite[] spritesColisores;
    [SerializeField] private RuntimeAnimatorController Gato, Ciclista;
    [SerializeField] private TMP_Text scoreText;
    private int money = 0, lastIncrease = 0;
    private float velocidadeGlobal = 8f;
    private Animator colisorAnimator;
    private bool started, over;
    private Camera cameraMain;

    // Start is called before the first frame update
    void Start()
    {
        cameraMain = mainCamera.GetComponent<Camera>();
        cameraMain.backgroundColor = new Color32(0, 0, 0, 1);
        menu.SetActive(true);
        started = false;
        over = false;
        colisorAnimator = colisor.GetComponent<Animator>();
        InvokeRepeating("SpawnMoeda", 2f, intervaloMoeda);
        InvokeRepeating("SpawnColisor", 3f, intervaloCollider);
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
                AudioController.instance.TocarMusicaFundo();
            }
        }
        else
        {
            cameraMain.backgroundColor = new Color32(126, 109, 97, 255);
            menu.SetActive(false);
            carro.SetActive(true);
            rua.SetActive(true);
            SetScore();
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
            cameraMain.backgroundColor = new Color32(0, 0, 0, 1);
            SetGameOver(false);
        }
    }

    void SpawnMoeda()
    {
        if (started)
        {
            Vector2 positionSpawn = spawner.transform.position;
            positionSpawn.y = Random.Range(-3.6f, 3.1f);
            // metodo que instancia o prefab; o objeto, a posicao, a rotacao padrao do negocio
            Instantiate(moeda, positionSpawn, Quaternion.identity);
        }
    }

    void SpawnColisor()
    {
        if (started)
        {
            int randomIndex = Random.Range(0, 4);
            Vector2 positionSpawn = spawner.transform.position;
            switch (randomIndex)
            {
                case 0:
                    positionSpawn.y = Random.Range(-3.2f, 2.55f);
                    float scalex = Random.Range(0.23f, 0.47f);
                    float scaley = Random.Range(0.23f, 0.35f);
                    colisor.transform.localScale = new Vector3(scalex, scaley, 0f);
                    colisorAnimator.runtimeAnimatorController = null;
                    break;
                case 1:
                    positionSpawn.y = Random.Range(-3.7f, 3.1f);
                    colisor.transform.localScale = new Vector3(-0.68f, 0.68f, 0f);
                    colisorAnimator.runtimeAnimatorController = null;
                    break;
                case 2:
                    positionSpawn.y = Random.Range(-3f, 2.9f);
                    colisor.transform.localScale = new Vector3(1, 1, 0f);
                    colisorAnimator.runtimeAnimatorController = Ciclista;
                    break;
                case 3:
                    positionSpawn.y = Random.Range(-3.6f, 3.1f);
                    colisor.transform.localScale = new Vector3(-1.7f, 1.7f, 0f);
                    colisorAnimator.runtimeAnimatorController = Gato;
                    break;
            }
            colisor.GetComponent<SpriteRenderer>().sprite = spritesColisores[randomIndex];
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

    public void SetScore()
    {
        scoreText.text = money.ToString();
    }

    public void SetMoney(int num)
    {
        money = num;
        SetScore();
    }

    public int GetMoney()
    {
        return money;
    }

    public void SetGameOver(bool itsOver)
    {
        if (itsOver)
        {
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            over = true;
            AudioController.instance.PararMusicaFundo();
        }
        else
        {
            carro.transform.position = new Vector2(-6f, 0f);
            carro.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), 0);
            SetMoney(0);
            velocidadeGlobal = 8f;
            intervaloCollider = 3.5f;
            started = false;
            over = false;
            gameOver.SetActive(false);
            menu.SetActive(true);
            carro.SetActive(false);
            rua.SetActive(false);
            scoreText.text = "";
            Time.timeScale = 1f;
        }
    }

    public float GetVelocidade()
    {
        return velocidadeGlobal;
    }

    public void IncreaseVelocidade()
    {
        if (GetMoney() % 5 == 0 && GetMoney() != lastIncrease)
        {
            if (velocidadeGlobal < 15)
            {
                velocidadeGlobal += 0.5f;
                lastIncrease = GetMoney();
            }
            else if (velocidadeGlobal < 17)
            {
                velocidadeGlobal += 0.1f;
                lastIncrease = GetMoney();
            }
            else
            {
                velocidadeGlobal += 0.02f;
                lastIncrease = GetMoney();
            }

            if (GetMoney() % 10 == 0 && intervaloCollider > 0.5f)
            {
                CancelInvoke("SpawnColisor");
                if (intervaloCollider > 1.5f)
                {
                    intervaloCollider -= 0.5f;
                }
                else if (intervaloCollider > 0.75f)
                {
                    intervaloCollider -= 0.25f;
                }
                else if (intervaloCollider > 0.5f)
                {
                    intervaloCollider -= 0.05f;
                }
                InvokeRepeating("SpawnColisor", 1f, intervaloCollider);
            }
        }
    }
}
