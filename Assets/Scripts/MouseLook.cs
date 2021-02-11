using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
  public enum RotationAxes
  {
    MouseXAndY = 0,
    MouseX = 1,
    MouseY = 2
  }
  public RotationAxes axes = RotationAxes.MouseXAndY;

  public float sensitivityHor = 3f;
  public float sensitivityVer = 3f;

  public float minimumVert = -90f;
  public float maximumVert = 90f;

  private float _rotationX = 0f;
  private float _rotationY = 0f;

  void Start()
  {
    Rigidbody body = GetComponent<Rigidbody>();

    if (body != null)
    {
      print("asda");
      body.freezeRotation = true;
    }
  }

  void Update()
  {
    switch (axes)
    {
      case RotationAxes.MouseX:
        countRotationY();
        break;

      case RotationAxes.MouseY:
        countRotationX();
        break;

      case RotationAxes.MouseXAndY:
        countRotationX();
        countRotationY();
        break;

    }

    transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
  }

  //верх/низ
  void countRotationX()
  {
    _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVer;
    _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
  }

  //лево/право
  void countRotationY()
  {
    _rotationY += Input.GetAxis("Mouse X") * sensitivityHor;
  }
}
