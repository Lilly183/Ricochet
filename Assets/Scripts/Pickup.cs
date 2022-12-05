using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [HideInInspector]
    public AudioSource audioSource;
    [HideInInspector]
    public LifeTime audioLifeTime;
    [HideInInspector]
    public ParticleSystem ps;

    public AudioClip pickupSFX;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        audioLifeTime = audioSource.GetComponent<LifeTime>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    public void Equip()
    {
        if (audioSource && audioLifeTime && ps)
        {
            audioSource.transform.parent = null;
            ps.transform.parent = null;

            audioSource.PlayOneShot(pickupSFX);
            audioLifeTime.StartTimer();

            ps.Play();
        }
    }
}
