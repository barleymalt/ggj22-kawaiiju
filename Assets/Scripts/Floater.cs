// Floater v0.0.2
// by Donovan Keith
//
// [MIT License](https://opensource.org/licenses/MIT)

using UnityEngine;

// Makes objects float up & down while gently spinning.
public class Floater : MonoBehaviour
{
    // User Inputs
    public float amplitude = 0.5f;
    public float frequency = 1f;

    // Position Storage Variables
    Vector3 posOffset;
    Vector3 tempPos;

    // Use this for initialization
    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.x += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}