using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    // Configuration params
    [SerializeField] Paddle paddle1;
    [SerializeField] float BallLaunchXVelocity = 2f;
    [SerializeField] float BallLaunchYVelocity = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;


    // State
    Vector2 paddleToBallVector;
    bool gameHasStarted = false;

    // Cached component references
    AudioSource audioSource;
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Use this for initialization
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameHasStarted)
        {
            LockBallToPaddle();
        }

        LaunchBallOnClick();
    }

    private void LaunchBallOnClick()
    {
        if (Input.GetMouseButtonDown(0) && GetComponent<Rigidbody2D>().velocity.x == 0 && GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            gameHasStarted = true;

            myRigidBody2D.velocity = new Vector2(BallLaunchXVelocity, BallLaunchYVelocity);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(UnityEngine.Random.Range(0f, randomFactor), UnityEngine.Random.Range(0f, randomFactor));

        if (gameHasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)]; 
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }
    }
}
