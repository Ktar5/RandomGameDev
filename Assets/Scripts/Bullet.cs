using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public const float speed = 25f;
    public const float sizeDiff = .01f;

    void Start() {
        Destroy(gameObject, 2.0f);
    }

    void Update() {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        float diff = sizeDiff * (1 + Time.deltaTime);
        Vector3 scale = transform.localScale - new Vector3(diff, diff, diff);
        if (scale.x > 0 && scale.y > 0) {
            transform.localScale = scale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}