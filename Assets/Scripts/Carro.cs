using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Carro : MonoBehaviour
{
    [SerializeField] private float velocidade;
    private float rotacao = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rotacao = 35f;
            transform.Translate(Vector2.up * velocidade * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.S))
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
            GameController.instance.setMoney(GameController.instance.getMoney() + 1);
            Destroy(other.gameObject);
        }
        Debug.Log(GameController.instance.getMoney());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Colisor"))
        {
            Debug.Log("Game Over");
            Time.timeScale = 0f;
        }
    }
}
