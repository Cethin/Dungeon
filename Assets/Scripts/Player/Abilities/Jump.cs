using UnityEngine;

public class Jump : Ability
{
    private float dist;
    private LayerMask lm = 8;

    protected override bool setup()
    {
        lm = 1 << LayerMask.NameToLayer("Default");
        dist = 3f;     // Distance to move on use
        cooldown = 1;   // Time between uses
        cost = 10;      // Mana cost
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


        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        input.Normalize();

        Vector2 target = player.transform.position + input * dist;
        RaycastHit2D rayHit;


        /*rayHit = Physics2D.Linecast(player.transform.position, target, lm);
        Debug.Log(LayerMask.LayerToName(lm) + " " + lm.value);
        if (rayHit)
            target = rayHit.point;*/

        rayHit = Physics2D.Raycast(player.transform.position, input, dist, lm);
        if (rayHit)
            target = rayHit.point;
        else
            target = player.transform.position + input * dist;

        Debug.DrawLine(player.transform.position, target, Color.red, 5);
        player.transform.position = target;

        used();
        return true;
    }

    public override string ToString()
    {
        return base.ToString() + " Dist: " + dist;
    }
}
