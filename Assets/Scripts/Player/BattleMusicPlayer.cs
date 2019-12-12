using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BattleMusicPlayer : MonoBehaviour
{
    private int triggerNum = 0;
    private AudioClip currentClip;

    public AudioMixer master;
    public AudioClip battleClip;
    public AudioSource BGMSource;

    private void Start()
    {
        currentClip = BGMSource.clip;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "TrialOfStrength") //|| other.gameObject.tag == "Combat")
        {
            Debug.Log("Entered Trial of Strength");

            StartCoroutine(StartFadeOut(master, "MasterVolume", 1.0f, -80.0f, battleClip));

            //BGMSource.clip = battleClip;
            //BGMSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "TrialOfStrength") //|| other.gameObject.tag == "Combat")
        {
            Debug.Log("Left Trial of Strength");

            StartCoroutine(StartFadeOut(master, "MasterVolume", 1.0f, -80.0f, currentClip));

            //BGMSource.Stop();
            //BGMSource.clip = currentClip;
            //BGMSource.Play();

            //StartCoroutine(StartFade(master, "MasterVolume", 5.0f, 0.0f));
        }
    }

    public IEnumerator StartFadeOut(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume, AudioClip clip)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);

            yield return null;
        }

        SwitchTracks(clip);
        yield break;
    }

    public IEnumerator StartFadeIn(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        yield break;
    }

    public void SwitchTracks(AudioClip clip)
    {
        Debug.Log("Switching Tracks");

        BGMSource.Stop();
        BGMSource.clip = clip;
        BGMSource.Play();

        StartCoroutine(StartFadeIn(master, "MasterVolume", 1.0f, 80.0f));
    }
}
