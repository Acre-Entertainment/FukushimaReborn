using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Camera")]
    public Camera cam;

    [Header("Sensitivity")]
    public float _sensitivityX;
    public float _sensitivityY;

    [Header("Vision Angles")]
    public float angleX;
    public float angleY;

    private float _rotationX = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Vision(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        _rotationX -= (mouseY * Time.deltaTime) * _sensitivityY;
        _rotationX = Mathf.Clamp(_rotationX, angleX, angleY);

        cam.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * _sensitivityX);
    }
}
