using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Carro : MonoBehaviour
{
    [SerializeField] private float velocidade;
    [SerializeField] private AudioClip colisionSound, colectedMoney;
    private float rotacao = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rotacao = 35f;
            transform.Translate(Vector2.up * velocidade * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rotacao = -35f;
            transform.Translate(Vector2.down * velocidade * Time.deltaTime, Space.World);
        }
        else
        {
            rotacao = 0f;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, rotacao), Time.deltaTime * 5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Moeda"))
        {
            AudioController.instance.TocarAudio(colectedMoney);
            GameController.instance.SetMoney(GameController.instance.GetMoney() + 1);
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Colisor") || other.gameObject.CompareTag("Rua"))
        {
            AudioController.instance.TocarAudio(colisionSound);
            GameController.instance.SetGameOver(true);
        }
    }
}
