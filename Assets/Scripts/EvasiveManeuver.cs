using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public float dodge;
    public float smoothing;
    public float tilt;
    public Boundary boundary;

    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody body;
    
    void Start()
    {
        body = GetComponent<Rigidbody>();
        currentSpeed = body.velocity.z;
        StartCoroutine(Evade());
    }
    
    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));

            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }
    
    void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(body.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        body.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        body.position = new Vector3(
            Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(body.position.z, boundary.zMin, boundary.zMax)
        );
        body.rotation = Quaternion.Euler(0.0f, 0.0f, body.velocity.x * -tilt);
    }
}
