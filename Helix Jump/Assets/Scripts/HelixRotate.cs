using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixRotate : MonoBehaviour
{
    public float rotationSpeed;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxisRaw("Mouse X");
            transform.Rotate(0f, -mouseX * rotationSpeed * Time.deltaTime, 0f);
        }
    }
}
