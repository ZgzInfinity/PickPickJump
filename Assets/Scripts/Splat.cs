using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Splat : MonoBehaviour
{
    public static Splat Instance;

    public GameObject splatPrefab;

    public Ball ball;

    private List<GameObject> spawnedSplats = new List<GameObject>();


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void setSplatColor()
    {
        StartCoroutine(splatSetColorCoroutine());
    }

    private IEnumerator splatSetColorCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        splatPrefab.GetComponent<SpriteRenderer>().color = ball.GetComponent<Renderer>().material.color;
    }

    public void MakeSplat(GameObject helix)
    {
        GameObject splat = Instantiate(splatPrefab, transform.position, Quaternion.Euler(90, 0, 0));
        splat.transform.SetParent(helix.transform);
        spawnedSplats.Add(splat);
    }

    public void clearSplats()
    {
        if (spawnedSplats.Count > 0)
        {
            foreach (GameObject go in spawnedSplats)
            {
                Destroy(go);
            }
        }
    }
}
