using UnityEngine;
using UnityEngine.UI;


public class bullet : MonoBehaviour
{
    public int score = 0;

    public Text scoreText;
    
    void Start()
    {
        

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("car")){
            score++;
            scoreText.text = "Score: " + score;
            Debug.Log("colidiu com o objeto");
            Destroy(collision.gameObject);//destroi o objeto q colidiu
            Destroy(gameObject);//destroi ela mesma

        }


    }
}
