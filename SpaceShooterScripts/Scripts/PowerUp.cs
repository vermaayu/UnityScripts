using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private int _powerUpSpeed = 3;

    //ID for powerups 
    [SerializeField]  // powerupID = { 0 -> Trple Shot, 1 -> Speed, 2 -> Shield}
    private int powerupID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _powerUpSpeed * Time.deltaTime);

        if (transform.position.y <= -7.0f)
        {
            Destroy(this.gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            
            Player player = collision.transform.GetComponent<Player>();
            if (player !=null)
            {



                switch(powerupID)
                {
                    case 0:
                        player.TripleShot();
                        break;

                    case 1:
                        Debug.Log("Collected Speed Boost");
                        player.SpeedBoost();
                        break;

                    case 2:
                        Debug.Log("Collected Speed Boost");
                        player.Shield();
                        break;

                    default:
                        break;

                }




                //if (powerupID == 0)
                //{
                //    player.TripleShot();
                //}
                //else if (powerupID == 1)
                //{
                //    Debug.Log("Collected Speed Boost");
                //    //player.Speed();
                //}
                //else if (powerupID == 2)
                //{
                //    Debug.Log("Collected Speed Boost");
                //    //player.Shield();
                //}
            }
            Destroy(this.gameObject);
        }
    }
}
