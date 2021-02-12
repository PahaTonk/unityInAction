using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class FPSInput : MonoBehaviour
{
  private CharacterController _charController;
  public float speed = 6f;
  public float gravity = -9.8f;

  void Start()
  {
    _charController = GetComponent<CharacterController>();
  }

  void Update()
  {
    float deltaX = Input.GetAxis("Horizontal") * speed;
    float deltaZ = Input.GetAxis("Vertical") * speed;
    Vector3 movement = new Vector3(deltaX, 0, deltaZ);

    movement = Vector3.ClampMagnitude(movement, speed) * Time.deltaTime;
    movement = transform.TransformDirection(movement);
    movement.y = gravity;

    _charController.Move(movement);

    // transform.Translate(deltaX, 0, deltaZ);
  }
}
