using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossFadeAudioSource : MonoBehaviour
    {
    public void Fade(AudioClip clip, float volume)
    {
        StartCoroutine(FadeIt(clip, volume));
    }
    IEnumerator FadeIt(AudioClip clip, float volume)
    {
        AudioSource fadeOutSource = gameObject.AddComponent<AudioSource>();
        fadeOutSource.clip = GetComponent<AudioSource>().clip;
        fadeOutSource.time = GetComponent<AudioSource>().time;
        fadeOutSource.volume = GetComponent<AudioSource>().volume;
        fadeOutSource.outputAudioMixerGroup = GetComponent<AudioSource>().outputAudioMixerGroup;

        fadeOutSource.Play();

        GetComponent<AudioSource>().volume = 0f;
        GetComponent<AudioSource>().clip = clip;
        float t = 0;
        float v = fadeOutSource.volume;
        GetComponent<AudioSource>().Play();
        while (t < 0.98f)
        {
            t = Mathf.Lerp(t, 1f, Time.deltaTime * 0.2f);
            fadeOutSource.volume = Mathf.Lerp(v, 0f, t);
            GetComponent<AudioSource>().volume = Mathf.Lerp(0f, volume, t);
            yield return null;
        }
        GetComponent<AudioSource>().volume = volume;
        Destroy(fadeOutSource);
        yield break;
    }
}