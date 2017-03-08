using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Textures {
    //left -> right : clockwise
    public Dictionary<int, List<Vector2>> textures = new Dictionary<int, List<Vector2>>();

    //^> 0
    //^< 1
    //v> 2
    //v< 3

    public Textures() {
        textures.Add(0000, new List<Vector2>() {v2(.2f, 1), v2(.1f, 1), v2(.2f, .75f), v2(.1f, .75f)});
        textures.Add(0001, new List<Vector2>() { v2(.4f, 1), v2(.3f, 1), v2(.4f, .75f), v2(.3f, .75f) });
        textures.Add(1000, new List<Vector2>() { v2(.6f, 1), v2(.5f, 1), v2(.6f, .75f), v2(.5f, .75f) });
        textures.Add(0100, new List<Vector2>() { v2(.8f, 1), v2(.7f, 1), v2(.8f, .75f), v2(.7f, .75f) });
        textures.Add(0010, new List<Vector2>() { v2(1, 1), v2(.9f, 1), v2(1, .75f), v2(.9f, .75f) });

        textures.Add(1100, new List<Vector2>() { v2(.1f, .75f), v2(0f, .75f), v2(.1f, .5f), v2(0f, .5f) });
        textures.Add(0110, new List<Vector2>() { v2(.3f, .75f), v2(.2f, .75f), v2(.3f, .5f), v2(.2f, .5f) });
        textures.Add(0011, new List<Vector2>() { v2(.5f, .75f), v2(.4f, .75f), v2(.5f, .5f), v2(.4f, .5f) });
        textures.Add(1001, new List<Vector2>() { v2(.7f, .75f), v2(.6f, .75f), v2(.7f, .5f), v2(.6f, .5f) });
        textures.Add(1010, new List<Vector2>() { v2(.9f, .75f), v2(.8f, .75f), v2(.9f, .5f), v2(.8f, .5f) });

        textures.Add(1110, new List<Vector2>() { v2(.2f, .5f), v2(.1f, .5f), v2(.2f, .25f), v2(.1f, .25f) });
        textures.Add(0111, new List<Vector2>() { v2(.4f, .5f), v2(.3f, .5f), v2(.4f, .25f), v2(.3f, .25f) });
        textures.Add(1011, new List<Vector2>() { v2(.6f, .5f), v2(.5f, .5f), v2(.6f, .25f), v2(.5f, .25f) });
        textures.Add(1101, new List<Vector2>() { v2(.8f, .5f), v2(.7f, .5f), v2(.8f, .25f), v2(.7f, .25f) });
        textures.Add(1111, new List<Vector2>() { v2(1, .5f), v2(.9f, .5f), v2(1, .25f), v2(.9f, .25f) });

        textures.Add(0101, new List<Vector2>() { v2(.1f, .25f), v2(0f, .25f), v2(.1f, 0), v2(0f, 0) });
    }

    public Vector2 v2(float x, float y) {
        return new Vector2(x, y);
    }
}