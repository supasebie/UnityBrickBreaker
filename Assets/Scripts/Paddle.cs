using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //configuration param
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;

    [SerializeField] float screenWidthInUnits = 16f;
    // Update is called once per frame
    void Update()
    {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y);
        paddlePosition.x = Mathf.Clamp(mousePosInUnits, minX, maxX);
        transform.position = paddlePosition;
    }
}
