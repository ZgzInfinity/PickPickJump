using UnityEngine;

public class Trail : MonoBehaviour
{

    public Ball ball;

    public TrailRenderer trail;

    // Start is called before the first frame update
    void Start()
    {
        Color ballColor = ball.GetComponent<Renderer>().material.color;
        trail.startColor = new Color(ballColor.r, ballColor.g, ballColor.b, 1f);
        trail.endColor = new Color(ballColor.r, ballColor.g, ballColor.b, 0f);
    }
}
