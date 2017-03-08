using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float playerSpeed;
    private Rigidbody2D rigidbody;

    private void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float distance = playerSpeed * (1 + Time.deltaTime);
        rigidbody.velocity = new Vector2(distance * Input.GetAxis("Horizontal"), distance * Input.GetAxis("Vertical"));
    }

}