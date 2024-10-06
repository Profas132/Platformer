using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public abstract class KinematicPlatform2D : MonoBehaviour
{

    public float cycleRunTime = 3f; // Time it takes to complete 1 cycle of movement.
    public float cycleWaitTime = 2f; // How long to wait before starting another cycle.
    protected float currentCycleTime = 0; // For subclasses to check what the current cycle time is.
    protected float currentWaitTime = 0; // For subclasses to check what the current wait time is.

    // After a cycle completes, does the time start from 0 again? Or does it go in the opposite direction?
    public enum CycleType
    {
        repeat, pingPong
    }
    public CycleType cycleType;

    protected sbyte cycleDirection = 1; // Only for ping pong mode.

    protected Rigidbody2D rb; // Rigidbody for child classes, so they don't have to call it again.

    // We use Awake() here so we don't have to override start in child classes.
    protected virtual void Awake()
    {
        // We require a Rigidbody as this makes collider movement more efficient.
        // See https://forum.unity.com/threads/non-static-colliders-without-rigidbody-still-cost-in-unity-5.452177/#post-4343569
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Reset()
    {
        // Force the Rigidbody to kinematic mode when this component is added.
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    // This is protected because it needs to be overriden.
    protected virtual void Update()
    {
        if (currentWaitTime <= 0)
        {
            currentCycleTime += Time.deltaTime * cycleDirection;
            HandleCycleEnd();
        }
        else
        {
            currentWaitTime -= Time.deltaTime;
            HandleCycleRestart();
        }
    }

    void HandleCycleRestart()
    {
        if (currentWaitTime < 0)
        {
            currentCycleTime -= currentWaitTime;
            currentWaitTime = 0;
        }
    }

    // Checks if the cycle has ended. If it has, set the object
    // to wait mode. Algorithm works if cycleWaitTime is 0.
    void HandleCycleEnd()
    {
        // If the cycle is finished, set the Wait time and the currentCycleTime.
        // Works if cycleWaitTime is 0.
        switch (cycleType)
        {

            case CycleType.repeat:

                if (currentCycleTime > cycleRunTime)
                {
                    // We also subtract the wait time by the time we already exceeded.
                    currentWaitTime = cycleWaitTime - (currentCycleTime - cycleRunTime);
                    currentCycleTime = 0; // The next cycle time to start from.
                }

                break;

            case CycleType.pingPong:

                // Invert the cycle direction if we exceed the run time.
                if (currentCycleTime > cycleRunTime)
                {
                    // We account for if the currentCycleTime exceeds the cycleRunTime.
                    currentWaitTime = cycleWaitTime - (currentCycleTime - cycleRunTime);
                    cycleDirection = -1;

                    // Set the next cycle time to start from.
                    currentCycleTime = cycleRunTime;
                }
                else if (currentCycleTime < 0)
                {
                    currentWaitTime = cycleWaitTime - currentCycleTime;
                    cycleDirection = 1;

                    // Set the next cycle time to start from.
                    currentCycleTime = 0;
                }

                break;
        }
    }

    // Parents any Rigidbody when they step on the platform,
    // so it moves along with the platform.
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<attack>())
            collision.rigidbody.transform.SetParent(transform);
    }

    // Unparents any Rigidbody when they leave the platform,
    // so it stops moving along with the platform.
    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<attack>()) 
            collision.rigidbody.transform.SetParent(null);
    }
}