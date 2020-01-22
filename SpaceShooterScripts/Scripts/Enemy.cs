using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;

    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y <= -7.0f)
        {
            float randomX = Random.Range(-9.0f, 9.0f);
            transform.position = new Vector3(randomX, 6, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit : " + other.transform.name);

        if (other.transform.tag == "Player")
        {
            //Damage Player health -= 1;
            //other.transform.GetComponent<Player>().Damage();

            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }
        else if (other.transform.CompareTag("Laser"))
        {
            //Player player = GameObject.Find("Player").GetComponent<Player>();-------------------- Too expensive => Make global.
            if (_player != null)
            {
                _player.ScoreAdd(Random.Range(5,21));
            }
            Destroy(other.gameObject);
            Destroy(this.gameObject);

        }
    }
}