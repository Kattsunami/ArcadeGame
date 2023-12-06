using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
    [SerializeField] private float minRandomVelocity, maxRandomVelocity;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private TrailRenderer trail;

    [Space(15)]
    [SerializeField] private Color blueColour;
    [SerializeField] private Color redColour;
    [SerializeField] private Gradient neutralGradient;
    [SerializeField] private Gradient blueGradient;
    [SerializeField] private Gradient redGradient;

    private int state = 0;

    private void Start()
    {
        RandomizeVelocity();
        UpdateBall(0);
    }

    public void RandomizeVelocity()
    {
        Vector2 _randDirection = Random.Range(0, 1) == 0 ? new Vector2(-1, Random.Range(-.25f, .25f)) : new Vector2(1, Random.Range(-.25f, .25f));
        rb.velocity = _randDirection * Random.Range(minRandomVelocity, maxRandomVelocity);
    }

    public void OnGUI()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        Vector2 _velocity = rb.velocity;

        if (_collision.transform.tag == "Player1") UpdateBall(1);
        if (_collision.transform.tag == "Player2") UpdateBall(2);
        if (_collision.transform.tag == "Nullifier") UpdateBall(0);
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.transform.tag == "Tile")
        {
            _collision.transform.GetComponent<TileBehaviour>().UpdateTile(state);
        }
    }

    private void UpdateBall(int _newState)
    {
        state = _newState;

        switch (state)
        {
            case 0:
                sprite.color = new Color(1, 1, 1);
                trail.colorGradient = neutralGradient;
                break;

            case 1:
                sprite.color = blueColour;
                trail.colorGradient = blueGradient;
                break;

            case 2:
                sprite.color = redColour;
                trail.colorGradient = redGradient;
                break;
            default:

                break;
        }
    }
}

[CustomEditor(typeof(Ball))]
public class BallEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Randomise Velocity"))
        {
            (target as Ball).RandomizeVelocity();
        }
    }
}