using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rua : MonoBehaviour
{
    [SerializeField] private float velocidade = 6f;
    private float larguraRua;

    // Start is called before the first frame update
    void Start()
    {
        larguraRua = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 posicaoXRua = transform.position;
        posicaoXRua.x -= velocidade * Time.deltaTime;
        transform.position = posicaoXRua;

        if (posicaoXRua.x <= -larguraRua)
        {
            posicaoXRua.x += larguraRua * 2f;
            transform.position = posicaoXRua;
        }
    }
}
