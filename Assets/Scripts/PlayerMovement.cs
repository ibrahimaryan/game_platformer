using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;

    private bool grounded;
    [SerializeField] private float speed;
    [SerializeField] private float jump_speed;

    private void Awake()
    {
        //Get reference for rigidbidy from object
        body = GetComponent<Rigidbody2D>();

        //Get reference for animator from object
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.W))
        {
            Jump();
        }

        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jump_speed);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}