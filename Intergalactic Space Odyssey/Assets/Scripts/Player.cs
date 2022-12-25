using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public Image healthBar;
    public float life = 100f;
    private float initialLife;
    public GameObject spawnPoint;
    public float speed = 5f;
    float smooth = 10f;
    float time;
    public Rigidbody rb;

    public GameObject explotionEffect;

    public static bool respawByNewGame = false;

    public HUDGame hud;

    void Start()
    {
        initialLife = life;
        transform.position = spawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (respawByNewGame == true)
        {
            respawByNewGame = false;
            GameManager.NewGame();
            Start();
        }

        healthBar.fillAmount = life / initialLife;
        if (life <= 0)
        {
            GameManager.LoseLife();
            StartCoroutine(Spawn());
        }
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        if (hor != 0)
        {
            if (hor < 0)
            {
                InclineZ(30);
            }

            if (hor > 0)
            {
                InclineZ(angle: 60);
            }
        }
        else
        {
            Quaternion target = Quaternion.identity;
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.fixedDeltaTime * smooth);
        }

        Vector3 inputJugador = new Vector3(hor, 0, ver);
        rb.AddForce(inputJugador * (speed));
    }

    private void InclineZ(float angle)
    {
        float tiltAroundZ = Input.GetAxis("Horizontal") * angle * -1;
        Quaternion target = Quaternion.Euler(0, 0, tiltAroundZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.fixedDeltaTime * smooth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.gameObject.CompareTag("Obstacle") ||
            collision.collider.transform.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Spawn());
            GameManager.LoseLife();
        }

        if (collision.collider.transform.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.collider.transform.gameObject);
            ReceiveDamage(25);
        }
    }

    public IEnumerator Spawn()
    {
        life = initialLife;
        var efecto = Instantiate(explotionEffect, transform.position, transform.rotation);
        yield return new WaitForSeconds(2f);
        Destroy(efecto);
        transform.position = spawnPoint.transform.position;
    }

    void ReceiveDamage(float points)
    {
        life -= points;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Portal"))
        {
            GameManager.AddScore(hud.GetTimeRemaining());
            hud.Finish();
        }
    }
}