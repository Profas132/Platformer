using UnityEngine;
using UnityEditor;

public class StraightMovingPlatform2D : KinematicPlatform2D
{

    public Vector2 endPoint = new Vector2(5, 0);
    Vector2 origin;

    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    protected override void Update()
    {
        // Needed since it manages the cycle time.
        base.Update();

        // Since the cycle time is there, we can just use Lerp.
        transform.position = Vector2.Lerp(origin, origin + endPoint, currentCycleTime / cycleRunTime);
    }

    protected override void Reset()
    {
        base.Reset();

        // Set the endpoint to always be a straight line forward by default. For convenience.
        endPoint = transform.position + transform.right * 8;

        // Doesn't make sense for straight moving platforms to repeat.
        cycleType = CycleType.pingPong;
    }

    private void OnDrawGizmosSelected()
    {

        Vector2 start = EditorApplication.isPlaying ? origin : (Vector2)transform.position,
                end = start + endPoint;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(start, end);
        Gizmos.DrawWireSphere(start, 0.25f);
        Gizmos.DrawWireSphere(end, 0.25f);
    }
}