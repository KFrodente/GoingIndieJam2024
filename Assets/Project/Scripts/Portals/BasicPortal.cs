using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPortal : Portal
{
    AudioSource shopMusicSource;
    AudioSource mainMusicSource;
    private void Start()
    {
        shopMusicSource = GameObject.FindWithTag("ShopMusicPlayer").GetComponent<AudioSource>();
        mainMusicSource = GameObject.FindWithTag("MainThemePlayer").GetComponent<AudioSource>();
    }
    public override void OnInteract(BaseCharacter character)
    {
        base.OnInteract(character);

        if(connectedPortal is ShopPortal)
        {
            mainMusicSource.Play();
            shopMusicSource.Stop();
        }
        //character.transform.position = connectedPortal.transform.position;
    }


}
