using UnityEngine;
using System.Collections;

public class FadeTimer : MonoBehaviour
{
    private float start = 0;    // Start time (time when created)
    private float duration = 0;     // Amount of time to last
    private SpriteRenderer sr;
    private Color c;

    public bool setup(float d)
    {
        start = Time.time;
        duration = d;
        sr = this.GetComponentInChildren<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError(name + " FadeTimer did not find a SpriteRenderer!");
            return false;
        }
        c = sr.material.color;
        return true;
    }

	void Update ()
    {
        if (duration == 0)
            Destroy(gameObject);

        c.a = 1 - (Time.time - start) / duration;

        if (c.a <= 0)
            Destroy(gameObject);

        if (sr != null || setup(duration))
            sr.color = c;
    }
}
