using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManSoundManager : MonoBehaviour
{
    public GameObject BGM_Obj;
    public GameObject SFX_Obj;

    public AudioClip[] BGM_Clips;
    public AudioClip[] SFX_Clips;

    AudioSource bgmSource;
    AudioSource sfxSource;

    void Start()
    {
        bgmSource = BGM_Obj.GetComponent<AudioSource>();
        sfxSource = SFX_Obj.GetComponent<AudioSource>();
    }

    public bool PlayBGM( int index )
    {
        if( index >= BGM_Clips.Length )
        {
            return false;
        }
        PlaySound( bgmSource, BGM_Clips[index] );
        return true;
    }
    public bool PlayBGM( string name )
    {
        foreach ( var clip in BGM_Clips )
        {
            if(clip.name.Equals(name))
            {
                PlaySound( bgmSource, clip );
                return true;
            }
        }
        return false;
    }
    public bool PlaySFX( int index )
    {
        if ( index >= SFX_Clips.Length )
        {
            return false;
        }
        PlaySound( sfxSource, SFX_Clips[index] );
        return true;
    }
    public bool PlaySFX( string name )
    {
        foreach ( var clip in SFX_Clips )
        {
            if ( clip.name.Equals( name ) )
            {
                PlaySound( sfxSource, clip );
                return true;
            }
        }
        return false;
    }

    void PlaySound( AudioSource targetSrc, AudioClip clip )
    {
        targetSrc.Stop();
        targetSrc.clip = clip;
        targetSrc.Play();
    }

}
