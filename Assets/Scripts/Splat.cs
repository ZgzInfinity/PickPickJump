using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat : MonoBehaviour
{
    public static Splat Instance;

    public GameObject[] splatPrefabs;

    public Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        foreach (GameObject splat in splatPrefabs)
        {
            splat.GetComponent<SpriteRenderer>().color = ball.GetComponent<Renderer>().material.color;
        }
    }

    public void MakeSplat()
    {
        GameObject splat = Instantiate(splatPrefabs[(Random.Range(0, splatPrefabs.Length))], transform.position, Quaternion.Euler(90, 0, 0));
        splat.transform.SetParent(GameObject.Find("HelixTop").transform);
    }
}
