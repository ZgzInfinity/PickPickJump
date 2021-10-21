using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rigidBodyBall;

    public float impulseForce = 3f;

    private float secondsPerCollision = 0.2f;

    private float secondsOfCollision = 0f;

    private float decreaseFactor = 0.2f;

    private bool ignoreNextCollision = false;

    private int perfectParts = 0;

    public float superSpeed = 8f;

    private bool isSuperSpeedEnabled = false;

    private int perfectPassCount = 3;

    private Vector3 startingPosition;

    public Helix helix;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyBall = GetComponent<Rigidbody>();
        startingPosition = transform.position;
        Trail.Instance.setTrailColor();
        Splat.Instance.setSplatColor();
    }

    private void Reset()
    {
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
        if (!ignoreNextCollision)
        {
            if (!collision.gameObject.CompareTag(GameTags.HelixGoal))
            {
                perfectParts = 0;
                ignoreNextCollision = true;
                rigidBodyBall.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);
                secondsOfCollision = secondsPerCollision;

                if (collision.gameObject.CompareTag(GameTags.HelixLevelDeath))
                {
                    if (isSuperSpeedEnabled)
                    {
                        isSuperSpeedEnabled = false;
                        Destroy(collision.transform.parent.gameObject, 0.2f);
                    }
                    else
                    {
                        Reset();
                        helix.Reset();
                        UiManager.Instance.resetScore();
                        GameLevelManager.Instance.LoadLevel();
                    }
                }
                else if (isSuperSpeedEnabled)
                {
                    isSuperSpeedEnabled = false;
                    Destroy(collision.transform.parent.gameObject, 0.2f);
                }
            }
            else
            {
                if (GameLevelManager.Instance.IncrementLevel())
                {
                    Reset();
                    GameLevelManager.Instance.LoadLevel();
                    UiManager.Instance.resetScore();
                }
            }
        }
        Splat.Instance.MakeSplat(collision.gameObject);
    }

    private void Update()
    {
        if (ignoreNextCollision)
        {
            AllowNextCollision();
        }

        if (!isSuperSpeedEnabled && perfectParts >= perfectPassCount)
        {
            isSuperSpeedEnabled = true;
            rigidBodyBall.AddForce(Vector3.down * superSpeed, ForceMode.Impulse);
        }
    }


    private void AllowNextCollision()
    {
        if (secondsOfCollision > 0f)
        {
            secondsOfCollision -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            ignoreNextCollision = false;
        }
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
}
