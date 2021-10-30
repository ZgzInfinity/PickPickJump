using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rigidBodyBall;

    public float impulseForce = 3f;

    private bool ignoreNextCollision = false;

    private int perfectParts = 0;

    public float superSpeed = 8f;

    private bool isSuperSpeedEnabled = false;

    private int perfectPassCount = 3;

    private Vector3 startingPosition;

    public Helix helix;

    public Sound bouncingBall;

    public Sound gameOver;

    private bool inGameOver;

    // Start is called before the first frame update
    void Awake()
    {
        rigidBodyBall = GetComponent<Rigidbody>();
        startingPosition = transform.position;
        inGameOver = false;
    }

    public void Reset()
    {
        inGameOver = false;
        transform.position = startingPosition;
        perfectParts = 0;
        isSuperSpeedEnabled = false;
        ignoreNextCollision = false;
        Trail.Instance.setTrailColor();
        Splat.Instance.setSplatColor();
        Splat.Instance.clearSplats();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision || inGameOver)
        {
            return;
        }

        if (!isSuperSpeedEnabled && perfectParts >= perfectPassCount)
        {
            isSuperSpeedEnabled = true;
            rigidBodyBall.AddForce(Vector3.down * superSpeed, ForceMode.Impulse);
        }

        if (!collision.gameObject.CompareTag(GameTags.HelixGoal))
        {
            if (collision.gameObject.CompareTag(GameTags.HelixLevelDeath))
            {
                if (isSuperSpeedEnabled)
                {
                    perfectParts = 0;
                    rigidBodyBall.velocity = Vector3.zero;
                    rigidBodyBall.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

                    isSuperSpeedEnabled = false;
                    Destroy(collision.transform.parent.gameObject, 0.2f);
                    AudioManager.Instance.PlaySound(bouncingBall, false);
                }
                else
                {
                    inGameOver = true;
                    AudioManager.Instance.storeMusicStatus();
                    AudioManager.Instance.StopSound(AudioManager.Instance.soundtracks[AudioManager.Instance.currentSoundtrack]);
                    AudioManager.Instance.PlaySound(gameOver, false);
                    UiManager.Instance.LevelGameOver();
                    Splat.Instance.MakeSplat(collision.gameObject);
                }
            }
            else if (isSuperSpeedEnabled)
            {
                perfectParts = 0;
                rigidBodyBall.velocity = Vector3.zero;
                rigidBodyBall.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);
                isSuperSpeedEnabled = false;
                Destroy(collision.transform.parent.gameObject, 0.2f);
            }
            else
            {
                perfectParts = 0;
                rigidBodyBall.velocity = Vector3.zero;
                rigidBodyBall.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);
                AudioManager.Instance.PlaySound(bouncingBall, false);
                Splat.Instance.MakeSplat(collision.gameObject);
            }
            ignoreNextCollision = true;
            Invoke("AllowNextCollision", 0.2f);
        }
        else
        {
            Splat.Instance.MakeSplat(collision.gameObject);
            UiManager.Instance.LevelCompleted();
            GameLevelManager.Instance.levelCompleted = true;
            AudioManager.Instance.storeMusicStatus();
            AudioManager.Instance.StopSound(AudioManager.Instance.soundtracks[AudioManager.Instance.currentSoundtrack]);
        }
    }

    private void AllowNextCollision()
    {
        ignoreNextCollision = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (perfectParts == 0)
        {
            UiManager.Instance.updateScore(5);
        }
        else
        {
            UiManager.Instance.updateScore(perfectParts * 5);
        }
        perfectParts++;
    }


    private void Update()
    {
        if (!isSuperSpeedEnabled && perfectParts >= perfectPassCount)
        {
            isSuperSpeedEnabled = true;
            rigidBodyBall.AddForce(Vector3.down * superSpeed, ForceMode.Impulse);
        }
    }
}
