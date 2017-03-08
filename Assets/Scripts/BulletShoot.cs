using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour {
    private bool cooldown = false;
    public Bullet bullet;
    private SpriteRenderer parentRenderer;

    private void Start() {
        parentRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && cooldown == false) {
            Invoke("ResetCooldown", .2f);
            Bullet instance = Instantiate(bullet, transform.position, transform.rotation);
            instance.gameObject.GetComponent<SpriteRenderer>().flipY = parentRenderer.flipY;
            //cooldown = true;
        }
    }

    void ResetCooldown() {
        cooldown = false;
    }
}