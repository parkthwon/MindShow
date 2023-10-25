using UnityEngine;

public class PlayerMove_LHS : MonoBehaviour
{
    public float speed = 5;
    Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Move();

        Animation();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x, 0, y);

        transform.position += dir * speed * Time.deltaTime;
    }

    void Animation()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //anim.SetTrigger("Jump");

            anim.SetBool("isJump", true);
        }
    }
}
