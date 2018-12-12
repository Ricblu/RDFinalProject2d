using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private float inactivatedRotationSpeed = 100, activatedRotationSpeed = 300;

    [SerializeField]
    private float inacvtivatedScale = 1, activatedScale = 1.5f;

    [SerializeField]
    private Color inactivatedColor, activatedColor;

    private bool isActived = false;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        UpdateColor();
    }

    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateColor()
    {
        Color color = inactivatedColor;

        if (isActived)
            color = activatedColor;

        spriteRenderer.color = color;
    }

    private void UpdateScale()
    {
        float scale = inacvtivatedScale;

        if (isActived)
            scale = activatedScale;

        transform.localScale = Vector3.one * scale;
    }

    private void UpdateRotation()
    {
        float rotationSpeed = inactivatedRotationSpeed;

        if (isActived)
            rotationSpeed = activatedRotationSpeed;
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void SetIsActivated(bool value)
    {
        isActived = value;
        UpdateScale();
        UpdateColor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isActived)
        {
            Debug.Log("Player entered Checkpoint.");
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            player.SetCurrentCheckpoint(this);
            audioSource.Play();
        }
    }
}
