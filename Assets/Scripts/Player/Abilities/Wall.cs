using UnityEngine;

public class Wall : Ability
{
    public GameObject prefab;   // Prefab to instantiate

    protected float time;
    protected float length;
    protected float dist;
    private LayerMask lm;
    
    protected override bool setup()
    {
        lm = LayerMask.NameToLayer("Player");
        time = 5;       // How long will it last in seconds
        length = 5;     // Length of the wall
        dist = 10;      // Max distance from the player to the target
        cooldown = 1;   // Time between uses
        cost = 30;      // Mana cost
        Active = true;  // If this can be used

        ready = true;   // If setup has been run
        return true;
    }

    public override bool activate()
    {
        if (!ready)
            if (!setup())
                return false;
        
        if (!canUse())
            return false;


        //Debug.Log(ToString());

        Vector2 target;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.back, transform.position);
        float rayHit;
        if (plane.Raycast(ray, out rayHit))
        {
            // Get the point along the ray that hits the calculated distance.
            target = ray.GetPoint(rayHit);

            if (Vector2.Distance(transform.position, target) > dist)
            {
                // Determine the target rotation.  This is the rotation if the transform looks at the target point.
                Quaternion tarRot = Quaternion.LookRotation(target - new Vector2(transform.position.x, transform.position.y));

                Debug.Log("Dist too far");
            }

            GameObject wall = (GameObject) Instantiate(prefab, target, player.transform.rotation);
            wall.GetComponent<FadeTimer>().setup(time);
            wall.transform.localScale = new Vector3(length, 1, 1);

            used();
            return true;
        }

        return false;
    }

    public override string ToString()
    {
        return base.ToString() + " Duration: " + time + " Length: " + length;
    }
}
