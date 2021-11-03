
/*
 * ----------------------------------------
 * -- Project: Pick-Pick Jump -------------
 * -- Author: Rubén Rodríguez Estebban ----
 * -- Date: 31/10/2021 --------------------
 * ----------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Script that controls the behaviour of the ball
 */

public class Ball : MonoBehaviour
{
    // Number of helix platforms covered without collision
    private int perfectParts = 0;

    // Number of helix platforms to cover to get super speed
    private int perfectPassCount = 3;

    // Impulse force of the ball to bound
    private float impulseForce = 5f;

    // Super speed of the ball
    private float superSpeed = 8f;

    // Controls if a collision with a helix platform must be ignored
    private bool ignoreNextCollision = false;

    // Controls if the ball has super speed
    private bool isSuperSpeedEnabled = false;

    // Controls if the ball is in game over
    private bool inGameOver;

    // Starting position of the ball
    private Vector3 startingPosition;

    // Reference to the rigidbody
    public Rigidbody rigidBodyBall;

    // Reference to the helix
    public Helix helix;

    // Reference to the bouncing sound
    public Sound bouncingBall;

    // Reference to the game over sound
    public Sound gameOver;

    // Reference to the victory sound
    public Sound victory;

    // Awake is called one time when the scene is loaded
    private void Awake()
    {
        // Set the rigidbody component
        rigidBodyBall = GetComponent<Rigidbody>();

        // Set the starting position and that is not in game over
        startingPosition = transform.position;
        inGameOver = false;
    }

    // Set the collision of the ball to be not ignored
    private void AllowNextCollision()
    {
        // Start coroutine to set the collision of the ball to be not ignored
        StartCoroutine(AllowNextCollisionCoroutine());
    }

    // Coroutine that sets the collision of the ball to be not ignored
    private IEnumerator AllowNextCollisionCoroutine()
    {
        // Wait an a half second and set the collision of the ball to be not ignored
        yield return new WaitForSeconds(0.2f);
        ignoreNextCollision = false;
    }

    // Sent when an incoming collider makes contact with this object's collider
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision has to be ignored or if the ball is in game over
        if (!ignoreNextCollision && !inGameOver)
        {
            // Check if the ball has super speed 
            if (!isSuperSpeedEnabled && perfectParts >= perfectPassCount)
            {
                // Super speed increased
                isSuperSpeedEnabled = true;
                rigidBodyBall.AddForce(Vector3.down * superSpeed, ForceMode.Impulse);
            }

            // Check if the ball collides with the goal helix platform
            if (!collision.gameObject.CompareTag(GameTags.HelixGoal))
            {
                // Check if the ball collides with the death part of a helix level platform
                if (collision.gameObject.CompareTag(GameTags.HelixLevelDeath))
                {
                    // Check if ball has super speed
                    if (isSuperSpeedEnabled)
                    {
                        // Reset the super speed and destroy the helix platform
                        perfectParts = 0;
                        rigidBodyBall.velocity = Vector3.zero;
                        rigidBodyBall.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

                        isSuperSpeedEnabled = false;
                        Destroy(collision.transform.parent.gameObject, 0.2f);
                        AudioManager.Instance.PlaySound(bouncingBall, false);
                    }
                    else
                    {
                        // Ball has collided with a death part and game finish
                        inGameOver = true;

                        // Get the current soundtrack storing its duration
                        Sound currentSoundtrack = AudioManager.Instance.GetCurrentSoundtrack();
                        AudioManager.Instance.StopSound(currentSoundtrack, true);

                        // Reproduce the game over sound
                        AudioManager.Instance.PlaySound(gameOver, false);
                        UiManager.Instance.LevelGameOver();

                        // Display the splat
                        Splat.Instance.MakeSplat(collision.gameObject);
                    }
                }
                else if (isSuperSpeedEnabled)
                {
                    // Ball collides with normal helix platform 
                    perfectParts = 0;
                    rigidBodyBall.velocity = Vector3.zero;
                    rigidBodyBall.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

                    // Destroy it because ball has super speed
                    isSuperSpeedEnabled = false;
                    Destroy(collision.transform.parent.gameObject, 0.2f);

                    // Reproduce the sound of bouncing
                    AudioManager.Instance.PlaySound(bouncingBall, false);
                }
                else
                {
                    // Normal collision of ball without super speed
                    perfectParts = 0;
                    rigidBodyBall.velocity = Vector3.zero;
                    rigidBodyBall.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

                    // Reproduce the bouncing sound
                    AudioManager.Instance.PlaySound(bouncingBall, false);

                    // Display the splat
                    Splat.Instance.MakeSplat(collision.gameObject);
                }

                // Ignore next collision
                ignoreNextCollision = true;
                AllowNextCollision();
            }
            else
            {
                // Reproduce the sound of victory
                AudioManager.Instance.PlaySound(victory, false);

                // Ball collides with goal helix platform
                Splat.Instance.MakeSplat(collision.gameObject);
                UiManager.Instance.LevelCompleted();
                GameLevelManager.Instance.SetLevelCompleted(true);

                // Get the current soundtrack storing its duration
                Sound currentSoundtrack = AudioManager.Instance.GetCurrentSoundtrack();
                AudioManager.Instance.StopSound(currentSoundtrack, true);
            }
        }
    }

    // Sent when another object enters a trigger collider attached to this object 
    private void OnTriggerEnter(Collider other)
    {
        // Check the number of platforms without collide
        if (perfectParts == 0)
        {
            // Add a score of five
            UiManager.Instance.UpdateScore(5);
        }
        else
        {
            // Add a score of five
            UiManager.Instance.UpdateScore(perfectParts * 5);
        }

        // Increment the number of parts covered
        perfectParts++;
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if the ball has to be set with super speed
        if (!isSuperSpeedEnabled && perfectParts >= perfectPassCount)
        {
            // Set the super speed and more force to collide
            isSuperSpeedEnabled = true;
            rigidBodyBall.AddForce(Vector3.down * superSpeed, ForceMode.Impulse);
        }
    }

    // Reset the ball configuration
    public void Reset()
    {
        // Set the ball as in not game over
        inGameOver = false;

        // Set the starting position
        transform.position = startingPosition;

        // Set the configuration
        perfectParts = 0;
        isSuperSpeedEnabled = false;
        ignoreNextCollision = false;

        // Set the color of the trail and the splat
        Trail.Instance.SetTrailColor();
        Splat.Instance.SetSplatColor();

        // Clear all the splats of the previous level attempt
        Splat.Instance.ClearSplats();
    }

    // Get if the ball is in game over
    public bool GetInGameOver()
    {
        return inGameOver;
    }
}
