using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisor : MonoBehaviour
{
    [SerializeField] private float velocidade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x - velocidade * Time.deltaTime, transform.position.y);
    }
    
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
