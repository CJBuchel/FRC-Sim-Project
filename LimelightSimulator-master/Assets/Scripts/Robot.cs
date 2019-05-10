using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {

    private float kSTRAFE = 500;
    private float kTURN = 25;
    private float kFORWARD = 200;
    private float maxArea = 15;
    private Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
        // Target is Visible
        if (tv() > 0)
        {
            // Torque Force
            rb.AddRelativeTorque(Vector3.up * kTURN * (tx() / 4));
        }
        else // Target is not Visible
        {
            
        }

        if(ta() > 1)
        {
            // Forward Force
            rb.AddRelativeForce(Vector3.forward * Time.fixedDeltaTime * kFORWARD * maxArea / ta());
        }
        else
        {
            // No Target Forward Force
            rb.AddRelativeForce(Vector3.forward * Time.fixedDeltaTime * kFORWARD * 4);
        }

        // Limit Velocity
        if (rb.velocity.magnitude > 1)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 1);
        }

        print("tv: " + tv() + " tx: " + tx() + " ta: " + ta());
    }

    float ta ()
    {
        float result = 0;

        GameObject[] possibleTargets = new GameObject[0];
        possibleTargets = GameObject.FindGameObjectsWithTag("Target");

        for (int i = 0; i < possibleTargets.Length; i++)
        {
            Vector3 screenPoint = transform.Find("Main Camera").GetComponent<Camera>().WorldToViewportPoint(possibleTargets[i].transform.position);
            bool isVisible = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (isVisible)
            {
                result = Mathf.Pow(Mathf.Exp(1), -(Vector3.Distance(possibleTargets[i].transform.position, transform.position) - 8) / 2);
            }
            else
            {
                result = 0;
            }
        }

        return result;
    }

    float tv ()
    {
        float result = 0;

        GameObject[] possibleTargets = new GameObject[0];
        possibleTargets = GameObject.FindGameObjectsWithTag("Target");

        for (int i = 0; i < possibleTargets.Length; i++)
        {
            Vector3 screenPoint = transform.Find("Main Camera").GetComponent<Camera>().WorldToViewportPoint(possibleTargets[i].transform.position);
            bool isVisible = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (isVisible)
            {
                result = 1;
                //Make Targets Green when Visible
                possibleTargets[i].GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                result = 0;
                //Make Targets White when not Visible
                possibleTargets[i].GetComponent<Renderer>().material.color = Color.white;
            }
        }

        return result;
    }

    float tx ()
    {
        float result = 0;

        GameObject[] possibleTargets = new GameObject[0];
        possibleTargets = GameObject.FindGameObjectsWithTag("Target");

        for (int i = 0; i < possibleTargets.Length; i++)
        {
            Vector3 screenPoint = transform.Find("Main Camera").GetComponent<Camera>().WorldToViewportPoint(possibleTargets[i].transform.position);
            bool isVisible = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

            float x = screenPoint.x - 0.5f;
            if (x < 0)
            {
                x = -Mathf.Pow(Mathf.Exp(1), 11 * Mathf.Abs(x) - 2);
            }
            else
            {
                x = Mathf.Pow(Mathf.Exp(1), 11 * x - 2);
            }

            if (isVisible)
            {
                result = x;
            }
            else
            {
                result = 0;
            }
        }

        return result;
    }
	
    float ty ()
    {
        float result = 0;

        GameObject[] possibleTargets = new GameObject[0];
        possibleTargets = GameObject.FindGameObjectsWithTag("Target");

        for (int i = 0; i < possibleTargets.Length; i++)
        {
            Vector3 screenPoint = transform.Find("Main Camera").GetComponent<Camera>().WorldToViewportPoint(possibleTargets[i].transform.position);
            bool isVisible = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

            float y = screenPoint.y - 0.5f;
            if (y < 0)
            {
                y = -Mathf.Pow(Mathf.Exp(1), 11 * Mathf.Abs(y) - 2);
            }
            else
            {
                y = Mathf.Pow(Mathf.Exp(1), 11 * y - 2);
            }

            if (isVisible)
            {
                result = y;
            }
            else
            {
                result = 0;
            }
        }

        return result;
    }
}
