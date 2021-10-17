using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody rigidBodyBall;

    public float impulseForce = 3f;

    private float secondsPerCollision = 0.2f;

    private float secondsOfCollision = 0f;

    private float decreaseFactor = 1f;

    private bool ignoreNextCollision = false;

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
        }
    }

    private void Update()
    {
        if (ignoreNextCollision)
        {
            AllowNextCollision();
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
}
