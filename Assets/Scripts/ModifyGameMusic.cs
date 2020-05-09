using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyGameMusic : MonoBehaviour
{
    public AudioSource gameMusic;
    public List<AudioClip> musicLibrary;
    private int lengthOfLibrary;
    public AudioClip currentMusic;
    private bool isMute = false;

    void Start() {
        gameMusic = GetComponent<AudioSource>();
        currentMusic = musicLibrary[0];
        lengthOfLibrary = musicLibrary.Count;   
        StartCoroutine(MusicDelay());
    }

    IEnumerator MusicDelay() {
        yield return new WaitForSeconds(3f);
        gameMusic.Stop();
        gameMusic.PlayOneShot(currentMusic);
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.K)) stopOrRestartGameMusic(!isMute);
        if(Input.GetKeyDown(KeyCode.M)) toggleMute();
        if(Input.GetKeyDown(KeyCode.N)) switchGameMusic();
    }

    void stopOrRestartGameMusic(bool shouldStop) {
        if(shouldStop) {
            gameMusic.Stop();
            isMute = true;
        } else {
            gameMusic.Stop();
            gameMusic.PlayOneShot(currentMusic);
            isMute = false;
        }
    }

    void toggleMute() {
        gameMusic.mute = !gameMusic.mute;
        isMute = !isMute;
    }

    void switchGameMusic() {
        for(int index = 0; index < lengthOfLibrary; index++) {
            
            Debug.Log("Index:" + index);
            Debug.Log("Len of Lib:" + lengthOfLibrary);
            Debug.Log("Current music:" + currentMusic);

            if(musicLibrary[index].Equals(currentMusic)) {
                if(index <= lengthOfLibrary-2) {
                    Debug.Log("Match found. index is LoL-2. Current music:" + currentMusic);
                    currentMusic = musicLibrary[index+1];
                    gameMusic.Stop();
                    gameMusic.PlayOneShot(currentMusic);
                    Debug.Log("New Music:" + currentMusic);
                    isMute = false;
                } else {
                    currentMusic = musicLibrary[index-(lengthOfLibrary-1)];
                    gameMusic.Stop();
                    gameMusic.PlayOneShot(currentMusic);
                    isMute = false;
                }
            }
        }
    }
}
