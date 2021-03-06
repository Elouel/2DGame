﻿using UnityEngine;

public class SizeControler : MonoBehaviour
{
    public float time;

    public CircleCollider2D obj;

    private float timeLeft;
    private bool isEffected;

    private void Update()
    {
        if (this.isEffected)
        {
            if (this.timeLeft <= 0)
            {
                this.ChangeSize(0.8f);

                Destroy(this.gameObject);
            }

            this.timeLeft -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEffected)
        {
            this.ChangeSize(0.3f);
        }
    }

    private void ChangeSize(float value)
    {
        this.timeLeft = time;

        var radius = this.obj.radius;
        radius = value;
        this.obj.radius = radius;

        this.isEffected = !isEffected;
    }
}