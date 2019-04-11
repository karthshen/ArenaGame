using UnityEngine;
using System.Collections;

public class MageTeleportBolt : ProjectileItem
{
    enum TeleportBoltState
    {
        Flying = 1,
        Landed = 2,
        PortalOpened = 3,
        Teleporting = 4
    }

    [SerializeField]
    private float throwingForce = 200f;
    private float velocity = 8.0f;
    private float moveConstant = 1.0f;
    private float positionHorizontal = 0.05f;
    private float angleJoystick = 0f;
    private bool hasPlayed = false;
    private Vector3 throwForce;

    private TeleportBoltState tpState;

    private AudioSource audioSource;

    private const float BOLT_STAY_TIME = 10f;
    private const float PORTAL_CAST_TIME = 0.5f;

    private float timer = 0f;

    public override void ProjectileStart()
    {
        IgnoreOwnerCollision(owner);

        audioSource = GetComponent<AudioSource>();

        //Set bolt position to owner
        gameObject.transform.position = new Vector3(owner.transform.position.x, //+ positionHorizontal * Mathf.Sin(yDirectionInRadian),
            owner.transform.position.y + owner.transform.lossyScale.y / 2, owner.transform.position.z);

        throwForce = new Vector3(throwingForce * Mathf.Cos(angleJoystick), throwingForce * Mathf.Cos(angleJoystick), 0f);

        Rigidbody rb = GetComponent<Rigidbody>();

        rb.AddForce(throwForce);

        tpState = TeleportBoltState.Flying;
    }

    public override void ProjectileFinish()
    {
        tpState = TeleportBoltState.Landed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && tpState == TeleportBoltState.Flying)
        {
            ProjectileFinish();
        }

        else if (tpState == TeleportBoltState.Landed)
        {
            timer += Time.deltaTime;
            if (timer >= BOLT_STAY_TIME)
            {
                RemoveItem();
            }
        }

        else if (tpState == TeleportBoltState.PortalOpened)
        {
            timer += Time.deltaTime;
            if(timer>= PORTAL_CAST_TIME)
            {
                //Teleport the player to position of the bolt
                tpState = TeleportBoltState.Teleporting;
                OpenTeleport();
            }
        }
    }

    public void OpenTeleport()
    {
        //Opens teleport gate at player
        if (tpState == TeleportBoltState.Landed)
        {
            tpState = TeleportBoltState.PortalOpened;
            //Open the portal at player
            timer = 0;
        }

        //Opens teleport gate at the bolt
        else if(tpState == TeleportBoltState.Teleporting)
        {
            //Teleport player to the position of the bolt
            owner.transform.position = transform.position;
            //Opens new teleport at the position of the bolt



            //After all
            RemoveItem();
        }
    }

    public bool CanShootTeleportBolt(float x, float y)
    {
        if (x != 0f || y != 0f)
        {
            angleJoystick = Mathf.Atan2(y, x);
            return true;
        }
        else
            return false;
    }
}
