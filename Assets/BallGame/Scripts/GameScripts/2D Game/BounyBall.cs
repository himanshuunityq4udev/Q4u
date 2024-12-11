using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BouncyBall : MonoBehaviour
{
    public float minY = -6.5f;
    private Rigidbody2D rb;
    public float maxVelocity = 15f;

    public GameObject button;
    public GameObject collectbutton;
    int count;
    [SerializeField] GameObject coinParent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.down * 10f;
        button.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Restart);
        button.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(LoadGameScene);
        count = coinParent.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < minY)
        {
           // transform.position = Vector3.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            button.SetActive(true);
        }
        
        if(rb.linearVelocity.magnitude > maxVelocity)
        {
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxVelocity);
        }
        if(count == 0)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            collectbutton.SetActive(true);
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            count--;
//            Debug.Log(count);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("base"))
        {
            float randomX = Random.Range(-0.5f, 0.5f); // Random small variation
            Vector2 randomDirection = new Vector2(randomX, 1).normalized; // Ensuring it goes up
            rb.linearVelocity = randomDirection * rb.linearVelocity.magnitude;
        }
    }


    public void Restart()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector2.down * 10f;
        button.SetActive(false);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Touch", LoadSceneMode.Single);
    }
}
