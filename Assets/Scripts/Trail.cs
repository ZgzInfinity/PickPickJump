using UnityEngine;
using System.Collections;


public class Trail : MonoBehaviour
{
    public static Trail Instance;

    public Ball ball;

    public TrailRenderer trail;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    public void setTrailColor()
    {
        StartCoroutine(trailSetColorCoroutine());
    }

    private IEnumerator trailSetColorCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        Color ballColor = ball.GetComponent<Renderer>().material.color;
        trail.startColor = new Color(ballColor.r, ballColor.g, ballColor.b, 1f);
        trail.endColor = new Color(ballColor.r, ballColor.g, ballColor.b, 0f);
    }
}
