using UnityEngine;

public abstract class AEntity : MonoBehaviour
{
    protected System.Guid entityId;
    protected string entityName;
    protected EntityState state;
    protected Mesh entityMesh;
    protected AudioSource effectSource;

    public static Vector3 FRONT_DIRECTION = new Vector3(0, 90, 0);
    public static Vector3 BACK_DIRECTION = new Vector3(0, 270, 0);

    public System.Guid GetEntityId()
    {
        return entityId;
    }

    public string GetName()
    {
        return entityName;
    }

    public EntityState GetState()
    {
        return state;
    }

    public Mesh GetMesh()
    {
        return entityMesh;
    }

    public AudioSource GetAudioSource()
    {
        return effectSource;
    }

    public void NullParameterCheck()
    {
        if (entityId == null || entityName == null || state == null )//|| entityMesh == null)
        {
            throw new UnassignedReferenceException(GetType() + "class is not initialized correctly");
        }
    }

    protected void SetRotationToEntity(AEntity entity)
    {
        gameObject.transform.GetChild(0).rotation = entity.transform.GetChild(0).rotation;
    }

    public float GetYDirectionInRadian()
    {
        return transform.GetChild(0).rotation.eulerAngles.y * Mathf.PI / 180;
    }

    public void SetPositionToEnitty(AEntity entity)
    {
        gameObject.transform.position = entity.transform.position;
    }
}
