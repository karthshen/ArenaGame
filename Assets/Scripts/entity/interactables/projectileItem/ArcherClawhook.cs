using UnityEngine;
using UnityEditor;

public class ArcherClawhook : ProjectileItem
{
    enum ClawhookState
    {
        Shooting = 1,
        RetractingActor = 2,
        RetractingOthers = 3,
        Staying = 4,
        Retracted = 5,
    }

    [SerializeField]
    private float retractionForce = 100f;
    private float velocity = 15.0f;
    private float moveConstant = 1f;
    private float positionHorizontal = 0.05f;
    private float angleJoystick = 0f;
    private Vector3 rootPosition;

    private AActor hitActor;

    private Vector3 movement;

    private const float SHOOT_OUT_DURATION = 0.8f;
    private const float CLAW_STAY_TIME = 1.0f;

    private float timer = 0f;

    private ClawhookState clawhookState;

    private LineRenderer lineRenderer;

    public override void ProjectileStart()
    {
        IgnoreOwnerCollision(owner);

        lineRenderer = GetComponent<LineRenderer>();

        SetClawPositionToOwner();

        UpdateRootPosition();

        movement = new Vector3(moveConstant * velocity * Mathf.Cos(angleJoystick), moveConstant * velocity * Mathf.Sin(angleJoystick), 0f);

        clawhookState = ClawhookState.Shooting;
    }

    public override void ProjectileFinish()
    {
        RemoveItem();
    }

    private void Update()
    {
        UpdateRootPosition();

        DrawLine();

        float distance = Vector3.Distance(transform.position, owner.transform.position);

        if (clawhookState == ClawhookState.Shooting)
        {
            if (timer > SHOOT_OUT_DURATION)
            {
                ProjectileFinish();
            }

            transform.Translate(movement * Time.deltaTime);
            timer += Time.deltaTime;
        }
        else if (clawhookState == ClawhookState.RetractingActor)
        {
            SetClawPositionToActor(hitActor);
            //push actor toward this actor
            owner.GetRigidbody().AddForce((transform.position - owner.transform.position) * retractionForce / distance);
        }
        else if (clawhookState == ClawhookState.RetractingOthers)
        {
            //push actor toward the wall
            owner.GetRigidbody().AddForce((transform.position - owner.transform.position) * retractionForce / distance);
        }

        if(clawhookState == ClawhookState.RetractingActor || clawhookState == ClawhookState.RetractingOthers)
        {
            timer += Time.deltaTime;
            if(timer >= CLAW_STAY_TIME)
            {
                ProjectileFinish();
            }
        }

        if (CheckIfRetracted())
            ProjectileFinish();
    }

    private bool CheckIfRetracted()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, owner.transform.position)) < 1f && (clawhookState == ClawhookState.RetractingActor || clawhookState == ClawhookState.RetractingOthers))
        {
            owner.ClearForceOnActor();
            return true;
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<AEntity>() && !collision.gameObject.GetComponent<AActor>())
        {
            IgnoreEntityCollision(collision.gameObject.GetComponent<AEntity>());
            return;
        }
        else if (collision.gameObject.GetComponent<AActor>())
        {
            clawhookState = ClawhookState.RetractingActor;
            hitActor = collision.gameObject.GetComponent<AActor>();
            timer = 0f;
        }
        else
        {
            clawhookState = ClawhookState.RetractingOthers;
            timer = 0f;
        }
    }

    private void UpdateRootPosition()
    {
        rootPosition = new Vector3(owner.transform.position.x, //+ positionHorizontal * Mathf.Sin(yDirectionInRadian),
            owner.transform.position.y + owner.transform.lossyScale.y / 2, owner.transform.position.z);
    }

    private void DrawLine()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, rootPosition);
    }

    private void SetClawPositionToOwner()
    {
        //float yDirectionInRadian = owner.GetYDirectionInRadian();

        gameObject.transform.position = new Vector3(owner.transform.position.x, //+ positionHorizontal * Mathf.Sin(yDirectionInRadian),
            owner.transform.position.y + owner.transform.lossyScale.y / 2, owner.transform.position.z);
    }

    private void SetClawPositionToActor(AActor hitActor)
    {
        gameObject.transform.position = new Vector3(hitActor.transform.position.x, //+ positionHorizontal * Mathf.Sin(yDirectionInRadian),
            hitActor.transform.position.y + hitActor.transform.lossyScale.y / 2, hitActor.transform.position.z);
    }

    public bool CanShootClaw(float x, float y)
    {
        if (x != 0.0f || y != 0.0f)
        {
            angleJoystick = Mathf.Atan2(y, x);
            Debug.Log("Joystick Angle in Radian:" + angleJoystick + " with X: " + x + " Y: " + y);
            return true;
        }
        else
            return false;
    }
}