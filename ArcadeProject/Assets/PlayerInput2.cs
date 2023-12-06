using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput2 : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(0, 1 * speed);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(0, 0);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(0, 0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(0, -1 * speed);
        }
    }
}
