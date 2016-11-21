using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public static void PlayAudio(AudioSource sound)
    {
        sound.Play();
    }

    public static void StopAudio(AudioSource sound)
    {
        sound.Stop();
    }
}
