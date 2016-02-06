using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {

    private Rigidbody rb;
    public float force;
    public float lifespan = 5.0f; // How long the bullet lives if it doesn't hit anything.
    public float time; // The current time of the bullet.
    private Vector3 parentTransform;

    public bool active;

    //public bool active; // Not used. This check is for bullets that might be on the ground. We don't want it to register when it's not being shot at us.

	void Start () {
        rb = GetComponent<Rigidbody>();
        parentTransform = transform.position;
        StartCoroutine(DestroyTimer());
	}
	
	void Update () {
        if (active) {
            StartCoroutine(DestroyTimer());
        }

        //rb.AddRelativeForce(Vector3.forward * force);
        rb.velocity = transform.TransformDirection(new Vector3(0f, 0f, force));
	}

    private IEnumerator DestroyTimer()
    {
        while (lifespan >= 0)
        {
            time = Time.deltaTime;
            lifespan -= time;
            active = false;
            yield return null;
        }

        if (lifespan <= 0)
        {
            Debug.Log("Destroy Bullet Obj");
            Deactivate();
        }
        yield return null;
    }


    void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                Utilities.AdjustHealth(col.gameObject, -1f);
                Debug.Log("Hit Player with Bullet");
                Deactivate();
                break;
            case "Wall":
                //rb.useGravity = true;
                Deactivate();
                break;
        }
    }

    private void Deactivate() {
        gameObject.SetActive(false);
        transform.position = parentTransform;
        lifespan = 5f;
        active = true;
    }
}
