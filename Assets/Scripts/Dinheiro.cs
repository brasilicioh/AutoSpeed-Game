using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dinheiro : MonoBehaviour
{
    [SerializeField] private float velocidade;
    private PolygonCollider2D colliderMoney;
    // Start is called before the first frame update
    void Start()
    {
        colliderMoney = gameObject.AddComponent<PolygonCollider2D>();
        colliderMoney.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x - velocidade * Time.deltaTime, transform.position.y);

        Destroy(colliderMoney);
        colliderMoney = gameObject.AddComponent<PolygonCollider2D>();
        colliderMoney.isTrigger = true;
    }

    void FixedUpdate()
    {
        GameController.instance.IncreaseVelocidade();
        velocidade = GameController.instance.GetVelocidade();
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
