using UnityEngine;
using System.Collections;

public class SmallLightBomb : ProjectileItem
{
    public GameObject origin;

    private Rigidbody rb;

    private int lineNum = 0;

    private LineRenderer lineRenderer;

    private int bombNumber = 0;

    [SerializeField]
    private float disappearTime = 2.5f;

    private Vector3 lastFrameVelocity;

    [SerializeField]
    private float force = 200f;

    private Vector3 movement;

    [SerializeField]
    private float longestDistance = 8;

    public override void ProjectileFinish()
    {
        RemoveItem();
    }

    public override void ProjectileStart()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce((transform.position - origin.transform.position) * force);

        Vector3 direction = transform.position - origin.transform.position;

        GetComponentInChildren<MeshRenderer>().enabled = false;

        //movement = new Vector3(speed * Mathf.Cos(DirectionToAngle(direction.y, direction.x)), speed * Mathf.Sin(DirectionToAngle(direction.y, direction.x)), 0);

        //Ignore other small lightbombs collision
        SmallLightBomb[] bombs = GameObject.FindObjectsOfType<SmallLightBomb>();
        bombNumber = bombs.Length;
        foreach(SmallLightBomb bomb in bombs)
        {
            if(bomb != this)
                IgnoreEntityCollision(bomb);
        }

        lineRenderer = gameObject.GetComponent<LineRenderer>();

        //lineNum++;
        if(lineRenderer)
            lineRenderer.positionCount = lineNum + 1;

        //lineRenderer.SetPosition(lineNum - 1, transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<AActor>() && collision.gameObject.GetComponent<AActor>() != owner)
        {
            //Take damage here
            AActor hitActor = collision.gameObject.GetComponent<AActor>();
            //hitActor.FreezeTimer = 0f;
            hitActor.TakeDamage(owner.GetActorStat().AttackPower / bombNumber * 2, owner);
            hitActor.AttackCode = System.Guid.NewGuid();
            hitActor.ClearForceOnActor();
            IgnoreGameobjectCollision(collision.gameObject);
            return;
        }
        else
        {
            if (lineRenderer)
            {
                Vector3 currentPosition = transform.position;
                lineRenderer.SetPosition(lineNum, currentPosition);
                lineNum++;
                lineRenderer.positionCount = lineNum + 1;
                lineRenderer.SetPosition(lineNum, transform.position);
                //Bounce(collision.contacts[0].normal);

                longestDistance += 1.5f;
                //force += 15f;
            }
        }
    }

    private void Update()
    {
        //transform.Translate(movement * Time.deltaTime);

        if(lineRenderer)
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
