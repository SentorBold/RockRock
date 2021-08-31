using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterHook : MonoBehaviour
{
    public float speed;

    
    public GameObject spawnPos;
    public GameObject player;

    private void Awake()
    {
        
    }
    public void FixedUpdate()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {   
            Vector3 medium =(player.transform.position - other.transform.position);
            Debug.Log(medium);  
            Vector3 tam = (other.transform.position - medium);
            Debug.Log(tam);
            player.transform.position = tam;
            Debug.Log(tam);
        }
    }
    void DeleteGameObject()
    {
       
    }

}