using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
  public float speed = 10f;
  public int damage = 1;

  void Update()
  {
    transform.Translate(0, 0, speed * Time.deltaTime);
  }

  // Event срабатывающий при соприкосновении с другим объектом
  void OnTriggerEnter(Collider collider)
  {
    string tag = collider.tag;

    switch (tag)
    {
      case "Player":
        PlayerCharacter player = collider.GetComponent<PlayerCharacter>();

        player.Hurt(1);
        Destroy(gameObject);
        break;

      default:
        Destroy(gameObject);
        break;
    }
  }

  // задаём точку спавна и направление полета шара
  public void SetDirection(Vector3 spawnPos, Quaternion rotationToTarget)
  {
    transform.position = spawnPos;
    transform.rotation = rotationToTarget;
  }
}
