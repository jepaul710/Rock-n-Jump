using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    // Variables to get position and width of the background to repeat it when it goes out of the camera view
    private Vector3 startPos;
    private float repeatWidth;
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    void Update()
    {
        // Conditional to repeat the background when it goes out of the camera view

        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
