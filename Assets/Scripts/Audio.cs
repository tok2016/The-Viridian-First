using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio instance;
    public AudioSource levelMusic, mainMenuMusic;

    public AudioSource[] effects;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayMainMenuMusic()
    {
            levelMusic.Stop();
            mainMenuMusic.Play();
    }
    public void PlayEffects(int effectToPlay)
    {
        effects[effectToPlay].Stop();
        effects[effectToPlay].Play();
    }
}
