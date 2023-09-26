using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.UnityRoboticsDemo;

/// <summary>
///
/// </summary>
public class RosPublisherEx : MonoBehaviour
{
    ROSConnection ros;
    public string topicName = "pos_rot";

    // The game object
    public GameObject cube;
    // Publish the cube's position and rotation every N seconds
    public float publishMessageFrequency = 0.5f;

    // Used to determine how much time has elapsed since the last message was published
    private float t=270;
    private float c=270;
    private float timeElapsed;
    private Vector3 eulerAngle;
    void Start()
    {
        // start the ROS connection
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<PosRotMsg>(topicName);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > publishMessageFrequency)
        {
            if (c == 370)
                c = 0;
            if (c == 100)
                c = 270;
            if (t == 370)
                t = 0;
            if (t == 100)
                t = 270;
            Vector3 rotationVector = new Vector3(t, c, 0);
            Quaternion rotation = Quaternion.Euler(rotationVector);
            cube.transform.rotation = rotation;
            Vector3 eulerAngle = cube.transform.rotation.eulerAngles;
            
            Debug.Log("X: " + eulerAngle.x + ", Y: " + eulerAngle.y + ", Z: " + eulerAngle.z);
            //Debug.Log("X: " + cube.transform.rotation.x + ", Y: " + cube.transform.rotation.y + ", Z: " + cube.transform.rotation.z);

            PosRotMsg cubePos = new PosRotMsg(
                cube.transform.position.x,
                cube.transform.position.y,
                cube.transform.position.z,
                // cube.transform.rotation.x,
                // cube.transform.rotation.y,
                // cube.transform.rotation.z,
                eulerAngle.x,
                eulerAngle.y,
                eulerAngle.z,
                cube.transform.rotation.w
            );

            //Finally send the message to server_endpoint.py running in ROS
            ros.Publish(topicName, cubePos);

            timeElapsed = 0;
            t = t + 10;
            c = c + 10;
        }
    }
}