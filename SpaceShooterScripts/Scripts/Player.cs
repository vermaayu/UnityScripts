using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] //for it to display in the Script Component
    private float _speed = 7.0f;
    private float _speedMultiplier = 2;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _fireRate = 0.2f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;

    [SerializeField] //----------------------TripleShotPowerUp
    private bool _tripleShotEnabled = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private bool _speedBoostEnabled = false;

    private bool _shieldEnabled = false;

    [SerializeField]
    private GameObject _shieldVisuals;

    private int _score;

    private UI_manager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -2, 0);
        _spawnManager = GameObject.Find("Spawn_manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_manager>();

        if (_spawnManager == null)
        {
            Debug.Log("SpawnManager is NULL.");
        }
        if (_uiManager == null)
        {
            Debug.Log("UIManager is NULL");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        //Spawning Laser

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement() //All movement related commands go here.
    {
        if (_speedBoostEnabled)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * (_speed * _speedMultiplier) * Time.deltaTime);

            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * verticalInput * (_speed * _speedMultiplier) * Time.deltaTime);
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);

            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        }

        /*Alternatively:
         *                                 new Vector3(1,1,0) 
         * transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed *  Time.deltaTime);
         * 
         *------------------------OR-----------------------------------------------
         *
         *  Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
         *  tranform.Translate(direction * _speed * Time.deltaTime);
         */


        //Boundaries
        //if (transform.position.y >= 2)
        //    transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        //else if (transform.position.y <= -2)
        //    transform.position = new Vector3(transform.position.x, -2, transform.position.z);

        // Or Clamping for setting boundaries:
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.65f, 2f), transform.position.z);

        // Not applicable for warping
        if (transform.position.x >= 11.42f)
            transform.position = new Vector3(-11.42f, transform.position.y, transform.position.z);
        else if (transform.position.x <= -11.42f)
            transform.position = new Vector3(11.42f, transform.position.y, transform.position.z);
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        // Debug.Log("Space Key Pressed");   // Print in console
        if (_tripleShotEnabled)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        if (_shieldEnabled)
        {
            _shieldEnabled = false;
            _shieldVisuals.SetActive(false);
            return;
        }
        else
        {
            _lives -= 1;
            _uiManager.UpdateLives(_lives);

            if (_lives < 1)
            {
                _uiManager.SetGameOver();
                _uiManager.GameFlicker();
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
            }
        }
    }

    public void TripleShot()
    {
        _tripleShotEnabled = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _tripleShotEnabled = false;
    }

    public void SpeedBoost()
    {
        _speedBoostEnabled = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _speedBoostEnabled = false;
    }

    public void Shield()
    {
        _shieldEnabled = true;
        _shieldVisuals.SetActive(true);

    }

    public void ScoreAdd(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
