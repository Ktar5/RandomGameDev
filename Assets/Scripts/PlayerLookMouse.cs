using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookMouse : MonoBehaviour {
    private SpriteRenderer renderer;

    private Vector2 mouse_pos;
    private Vector2 object_pos;
    private float angle;

    private void Start() {
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update() {
        mouse_pos = Input.mousePosition;
        object_pos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        //print(Vector2.Distance(mouse_pos, object_pos));
        if (Vector2.Distance(mouse_pos, object_pos) > 30) {
            mouse_pos.x = mouse_pos.x - object_pos.x;
            mouse_pos.y = mouse_pos.y - object_pos.y;
            angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            if (angle >= 90 || angle < -90) {
                //transform.localRotation = Quaternion.Euler(0, 180, 0);
                renderer.flipY = true;
            }
            else {
                renderer.flipY = false;
            }
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}