using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class kontrol : MonoBehaviour {

    public Sprite []KusSprite;
    SpriteRenderer spriteRenderer;
    bool kanatKontrol=true;
    int kanatSayac=0;
    float kusAnimasyonZaman=0;
    Rigidbody2D fizik;
    int score = 0;
    public Text scoreTxt;
    bool gameOver = true;
    oyunKontrol oyunkontrol;
    AudioSource []sesler;
    int highScore = 0;



    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        oyunkontrol = GameObject.FindGameObjectWithTag("oyunkontrol").GetComponent<oyunKontrol>();
        sesler = GetComponents<AudioSource>();
        highScore= PlayerPrefs.GetInt("highScore");
	}
	
	
	void Update ()
    {
       
        if(Input.GetMouseButtonDown(0) && gameOver)
        {
            fizik.velocity = new Vector2(0, 0);//hızı 0 yaptı
            fizik.AddForce(new Vector2(0,200));//sonra kuvvet uyguladı
            sesler[0].Play();
        }
        if(fizik.velocity.y>0)
        {
            transform.eulerAngles = new Vector3(0, 0, 30);
        }
        else
            transform.eulerAngles = new Vector3(0, 0, -30);
        Animasyon();
    }



    void Animasyon()
    {
        kusAnimasyonZaman += Time.deltaTime;
        if (kusAnimasyonZaman > 0.2f)
        {
            kusAnimasyonZaman = 0;
            if (kanatKontrol)
            {
                spriteRenderer.sprite = KusSprite[kanatSayac];
                kanatSayac++;
                if (kanatSayac == KusSprite.Length)
                {
                    kanatSayac--;
                    kanatKontrol = false;
                }

            }

            else
            {
                kanatSayac--;
                spriteRenderer.sprite = KusSprite[kanatSayac];
                if (kanatSayac == 0)
                {
                    kanatSayac++;
                    kanatKontrol = true;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="puan")
        {
            score++;
            scoreTxt.text = "Score=" + score;
            sesler[1].Play();
        }
        if (collision.gameObject.tag == "engel")
        {
            gameOver = false;
            oyunkontrol.gameOver();
            sesler[2].Play();
            GetComponent<CircleCollider2D>().enabled = false;

            if(score>highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("highScore", highScore);
            }
            Invoke("anaMenuyeDon",2);//oyun bitince 3 sn sonra ana menuye dön
        }
    }
    void anaMenuyeDon()
    {
        SceneManager.LoadScene("anaMenu");
    }
}
