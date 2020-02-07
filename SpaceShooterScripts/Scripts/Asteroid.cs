using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 20f;

    [SerializeField]
    private GameObject _explosionPrefab;
    
    void Update()
    {
        if (transform.position.y > 0)
        {
            transform.Translate(Vector3.down * 2 * Time.deltaTime);
        }

        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);

        //if (transform.position.y <= -7.0f)
        //{
        //    Destroy(this.gameObject);
        //    //transform.position = new Vector3(Random.Range(-9f,9f), 6, 0);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser") && transform.position.y <=0)
        {
            //Player player = collision.transform.GetComponent<Player>();

            //if (player != null)
            //{
            //    player.Damage();
            //}
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 1f);
            Destroy(collision.gameObject);
        }
    }
}
