using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody rigidBodyBall;

    public float impulseForce = 3f;

    private float secondsPerCollision = 0.2f;

    private float secondsOfCollision = 0f;

    private float decreaseFactor = 1f;

    private bool ignoreNextCollision = false;

    private int perfectParts = 0;

    public float superSpeed = 8f;

    private bool isSuperSpeedEnabled = false;

    private int perfectPassCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyBall = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!ignoreNextCollision)
        {
            ignoreNextCollision = true;
            rigidBodyBall.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);
            secondsOfCollision = secondsPerCollision;

            if (isSuperSpeedEnabled)
            {
                perfectParts = 0;
                isSuperSpeedEnabled = false;
                Destroy(collision.transform.parent.gameObject, 0.2f);
            }
        }
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
        perfectParts++;
    }
}
