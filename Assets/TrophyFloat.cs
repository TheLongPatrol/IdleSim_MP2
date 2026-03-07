using UnityEngine;

public class TrophyFloat : MonoBehaviour
{
    public float floatHeight = 0.03f;
    public float floatSpeed = 3f;
    public float rotateSpeed = 50f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        // bob using sine wave
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);

        // spin
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
