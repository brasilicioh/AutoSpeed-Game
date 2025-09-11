using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisor : MonoBehaviour
{
    [SerializeField] private float velocidade;
    [SerializeField] private LayerMask camada;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PolygonCollider2D>() == null)
        {
            gameObject.AddComponent<PolygonCollider2D>();
        }
        else
        {
            Destroy(GetComponent<PolygonCollider2D>());
            gameObject.AddComponent<PolygonCollider2D>();
        }

        transform.position = new Vector2(transform.position.x - velocidade * Time.deltaTime, transform.position.y);

        Collider2D[] colisores = Physics2D.OverlapCircleAll(transform.position, 1f, camada);

        foreach (Collider2D colider in colisores)
        {
            if (colider.gameObject != gameObject)
            {
                Destroy(gameObject);
                break;
            }
        }
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
