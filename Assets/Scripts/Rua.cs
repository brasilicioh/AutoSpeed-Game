using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rua : MonoBehaviour
{
    [SerializeField] private float velocidade;
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
            posicaoXRua.x += larguraRua * 1.98f;
            transform.position = posicaoXRua;
        }
    }

    void FixedUpdate()
    {
        GameController.instance.IncreaseVelocidade();
        velocidade = GameController.instance.GetVelocidade();
    }
}
