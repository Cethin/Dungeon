using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public float moveSpeed = 3f;
    public GameObject[] abilities;// = new GameObject[5];
    private float mana = 100;
    private float maxMana = 100;
    private float manaPerSec = 1;

    // Use this for initialization
    void Start()
    {
        resetAbilities();
    }
	
	// Update is called once per frame
	void Update ()
    {
        move();
        rotate();
        doAbilities();
	}

    private void move()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        input.Normalize();
        input *= moveSpeed * Time.deltaTime;
        transform.position = transform.position + input;
    }

    private void rotate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.back, transform.position);
        float rayHit;
        if (plane.Raycast(ray, out rayHit))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 tarPoint = ray.GetPoint(rayHit);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion tarRot = Quaternion.LookRotation(tarPoint - transform.position);

            // Instantly rotate towards the target point
            tarPoint.z = 0;
            transform.LookAt(tarPoint);
            transform.Rotate(Vector3.back, tarRot.eulerAngles.y);
            transform.Rotate(Vector3.right, 90);    //Correct rotation
        }
    }

    private void resetAbilities()
    {
        foreach (GameObject ab in abilities)
            ab.GetComponent<Ability>().Reset();
    }

    private void doAbilities()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            string axis = "Ab"+i;   // Axis to check
            if (Input.GetAxisRaw(axis) > 0)
            {
                Ability ab = abilities[i].GetComponent<Ability>();
                if (ab.activate())
                    mana -= ab.Cost;
            }
        }

        mana += manaPerSec * Time.deltaTime;
        mana = Mathf.Clamp(mana, 0, maxMana);
    }
}
