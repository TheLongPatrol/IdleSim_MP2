using UnityEngine;

public class TrophyFloat : MonoBehaviour
{
    public float floatHeight = 0.03f;
    public float floatSpeed = 3f;
    public float rotateSpeed = 50f;
    
    [Header("Entrance")]
    public float k = 5f;
    private Vector3 startPos;
    private bool hasLanded = false;

    void Start()
    {
        // set starting position and move up for the drop
        startPos = transform.localPosition;
        transform.localPosition = new Vector3(startPos.x, startPos.y + 6f, startPos.z);
    }

    void Update()
    {
        // drop from sky using eased movement
        if (!hasLanded) {
            Vector3 velocity = k * (startPos - transform.localPosition);
            transform.localPosition += velocity * Time.deltaTime;
            
            // check if it is close enough to land
            if (Vector3.Distance(transform.localPosition, startPos) < 0.01f)
            {
                transform.localPosition = startPos;
                hasLanded = true;
            }
        } else {
            // bob using sine wave
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
        }

        // spin
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
