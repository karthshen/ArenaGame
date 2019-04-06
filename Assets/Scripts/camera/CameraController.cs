using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float CAMERA_SIZE_FACTOR = 1.5f;

    Camera arenaCamera;

    float minX;
    float maxX;
    float minY;
    float maxY;

    Vector3 desiredPos;

    float arenaCameraeraDistance;

    public AActor[] actors;
    public float cameraSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        arenaCamera = GetComponent<Camera>();
        arenaCameraeraDistance = transform.position.z;

        //Temp for constants
        Physics.gravity *= 1.54f * 1.54f;
    }

    // Update is called once per frame
    void Update()
    {
        if(actors.Length < 2)
        {
            FindActors();
        }

        CalculateCameraBounds();
        UpdateCameraPosition();
        MoveCamera();
    }

    private void FindActors()
    {
        actors = FindObjectsOfType<AActor>();
    }

    private void MoveCamera()
    {
        transform.position = Vector3.MoveTowards(transform.position, desiredPos, cameraSpeed);
    }

    private void UpdateCameraPosition()
    {
        var CONSTANT_MULTIPLIER = 0.3f;
        var CONSTANT_CLOSE = 3f;

        if (actors.Length <= 0)//early out if no actors have been found
            return;
        desiredPos = Vector3.zero;
        float distance = 0f;
        var mHeight = maxY - minY;
        var mWidth = maxX - minX;
        var distanceH = -(mHeight + CONSTANT_CLOSE) * CONSTANT_MULTIPLIER / Mathf.Tan(arenaCamera.fieldOfView * CONSTANT_MULTIPLIER * Mathf.Deg2Rad);
        var distanceW = -(mWidth / arenaCamera.aspect + CONSTANT_CLOSE) * CONSTANT_MULTIPLIER / Mathf.Tan(arenaCamera.fieldOfView * CONSTANT_MULTIPLIER * Mathf.Deg2Rad);
        distance = distanceH < distanceW ? distanceH : distanceW;

        for (int i = 0; i < actors.Length; i++)
        {
            desiredPos += actors[i].transform.position;
        }
        if (distance > -CONSTANT_CLOSE) distance = -CONSTANT_CLOSE;
        //Debug.Log("Distance: "+distance);
        desiredPos /= actors.Length;
        desiredPos.z = distance;
    }

    private void CalculateCameraBounds()
    {
        minX = Mathf.Infinity;
        maxX = -Mathf.Infinity;

        minY = Mathf.Infinity;
        maxY = -Mathf.Infinity;

        foreach(AActor actor in actors){
            Vector3 tempActor = actor.transform.position;

            float tempX = tempActor.x + CAMERA_SIZE_FACTOR;
            float tempY = tempActor.y + CAMERA_SIZE_FACTOR;

            minX = tempActor.x < minX ? tempActor.x : minX;
            //maxX = tempActor.x > maxX ? tempActor.x : maxX;

            minY = tempActor.y < minY ? tempActor.y : minY;
            //maxY = tempActor.y > maxY ? tempActor.y : maxY;

            //minX = tempX < minX ? tempX : minX;
            maxX = tempX > maxX ? tempX : maxX;

            //minY = tempY < minY ? tempY : minY;
            maxY = tempY > maxY ? tempY : maxY;
        }

        //maxX *= 1.5f;
        //maxY *= 1.5f;
    }

    private void CalculateCmaeraPosAndSize()
    {
        Vector3 arenaCameraeraCenter = Vector3.zero;
        Vector3 finalLookat = Vector3.zero;

        foreach(AActor actor in actors)
        {
            arenaCameraeraCenter += actor.transform.position;
        }

        arenaCameraeraCenter = arenaCameraeraCenter / actors.Length;

        //Positions arenaCameraera around a center point
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + arenaCameraeraCenter;

        transform.position = Vector3.Lerp(transform.position, position, cameraSpeed * Time.deltaTime);

        finalLookat = Vector3.Lerp(finalLookat, arenaCameraeraCenter, cameraSpeed * Time.deltaTime);
        //Look at
        transform.LookAt(finalLookat);
    }
}
