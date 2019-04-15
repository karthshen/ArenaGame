using UnityEngine;
using System.Collections;

public class SmallLightBomb : ProjectileItem
{
    public GameObject origin;

    private Rigidbody rb;

    private int lineNum = 0;

    private LineRenderer lineRenderer;

    [SerializeField]
    private float disappearTime = 2.5f;

    private Vector3 lastFrameVelocity;

    [SerializeField]
    private float force = 200f;

    private Vector3 movement;

    [SerializeField]
    private float longestDistance = 3;

    public override void ProjectileFinish()
    {
        RemoveItem();
    }

    public override void ProjectileStart()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce((transform.position - origin.transform.position) * force);

        Vector3 direction = transform.position - origin.transform.position;

        //movement = new Vector3(speed * Mathf.Cos(DirectionToAngle(direction.y, direction.x)), speed * Mathf.Sin(DirectionToAngle(direction.y, direction.x)), 0);

        lineRenderer = gameObject.GetComponent<LineRenderer>();

        //lineNum++;

        lineRenderer.positionCount = lineNum + 1;

        //lineRenderer.SetPosition(lineNum - 1, transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<AActor>())
        {
            //Take damage here
            IgnoreGameobjectCollision(collision.gameObject);
            return;
        }
        else
        {
            Vector3 currentPosition = transform.position;
            lineRenderer.SetPosition(lineNum, currentPosition);
            lineNum++;
            lineRenderer.positionCount = lineNum + 1;
            lineRenderer.SetPosition(lineNum, transform.position);
            //Bounce(collision.contacts[0].normal);

            longestDistance += 1.5f;
            force += 15f;
        }
    }

    private void Update()
    {
        //transform.Translate(movement * Time.deltaTime);

        lineRenderer.SetPosition(lineNum, transform.position);

        disappearTime -= Time.deltaTime;

        if(Vector3.Distance(transform.position, origin.transform.position) > longestDistance)
        {
            ProjectileFinish();
        }


        if (disappearTime <= 0)
        {
            ProjectileFinish();
        }

        //lastFrameVelocity = rb.velocity;
    }

    private void Bounce(Vector3 collisionNormal)
    {
        //var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(movement.normalized, collisionNormal);

        //Debug.Log("Out Direction: " + direction);
        //rb.velocity = direction * Mathf.Max(speed, 0);

        //movement = new Vector3(speed * Mathf.Cos(DirectionToAngle(direction.y, direction.x)), speed * Mathf.Sin(DirectionToAngle(direction.y, direction.x)), 0);
    }

    private float DirectionToAngle(float x, float y)
    {
        return Mathf.Atan2(y, x);
    }
}
