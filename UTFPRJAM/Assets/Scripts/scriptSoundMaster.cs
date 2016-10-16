using UnityEngine;
using System.Collections;

public class scriptSoundMaster : MonoBehaviour
{
    public static scriptSoundMaster Instance
    {
        get { return FindObjectOfType<scriptSoundMaster>(); }
    }

    private AudioSource _source;
    private AudioSource source
    {
        get
        {
            if (!_source) _source = GetComponent<AudioSource>();
            return _source;
        }
    }

    public void PlaySound(string audio)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/SoundEffects/" + audio);
        source.PlayOneShot(clip, 1);
    }

}
