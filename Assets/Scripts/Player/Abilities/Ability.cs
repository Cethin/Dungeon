using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected float cost = 0f;
    public float Cost
    {
        get
        {
            return cost;
        }
    }

    public bool Active
    {
        get
        {
            return active;
        }

        set
        {
            active = value;
        }
    }

    protected float cooldown = 0f;
    protected float lastUsed = 0f;
    private bool active = true;
    protected bool ready = false;
    
    protected GameObject player;    // Reference to the player

    public void Start()
    {
        Reset();

        ready = false;
        setup();
    }

    public void Reset()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        ready = false;
        Active = true;
        lastUsed = Time.time;
    }

    protected void used()
    {
        Active = false;
        lastUsed = Time.time;
    }

    protected abstract bool setup();

    public bool canUse()
    {
        if (!Active && cooldownLeft() == 0)
            Active = true;
        return Active;
    }

    public float cooldownLeft()
    {
        float left = 1 - (Time.time - lastUsed) / cooldown;
        if (left <= 0)
            return 0;
        return left;
    }

    public abstract bool activate();

    public override string ToString()
    {
        return name + " Cooldown: " + cooldown + " Cost: " + cost;
    }
}
