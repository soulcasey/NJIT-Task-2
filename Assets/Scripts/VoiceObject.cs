using System;
using System.Collections.Generic;
using UnityEngine;

public enum VoiceObjectType
{
    Dog,
}

public enum VoiceSoundType
{
    Bark,
}

public abstract class VoiceObject : MonoBehaviour, IOutlineObject
{
    public abstract VoiceObjectType voiceObjectType { get; }
    public VoiceAction currentVoiceAction { get; protected set; }

    [SerializeField]
    protected AudioSource voiceSource;

    [SerializeField]
    protected Animator animator;
    protected float speed = 0;

    [SerializeField]
    protected Renderer objectRenderer;
    protected virtual float outlineWidth { get; set; } = 0.002f;
    

    public void SetVoiceAction(VoiceAction newVoiceAction)
    {
        currentVoiceAction = newVoiceAction;

        currentVoiceAction.PerformVoiceAction(this);
    }

    public void SetRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void PlaySound()
    {
        voiceSource?.Play();
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetAnimator(int animatorInt)
    {
        animator.SetInteger("movement", animatorInt);
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;

        if (transform.position.y < -5) // If the object falls off
        {
            transform.position = new Vector3(0, 5, 0);
        }
    }

    public void HandleOnOutlineStart()
    {
        objectRenderer.material.SetFloat("_OutlineEnabled", 1);
    }

    public void HandleOnOutlineEnd()
    {
        objectRenderer.material.SetFloat("_OutlineEnabled", 0);
    }
}
