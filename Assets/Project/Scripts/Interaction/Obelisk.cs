using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Obelisk : Interactable
{
    
    [SerializeField] private PieceSeparation dragonMaker;
    [SerializeField] private GameObject particleActivation;
    private bool activated;
    [SerializeField] private CinemachineVirtualCamera bossRoomCam;
    [SerializeField] private AudioClip sound;
    [SerializeField] private GameObject healthBar;

    public Slider overSlider;



    [SerializeField] private Room room;

    private void Update()
    {
        overSlider.value = dragonMaker.GetHealthPercent();
    }

    public override void OnInteract(BaseCharacter character)
    {
        if(activated) return;
        Transform fp = character.possessingSpirit.transform;

        if (room.topPortal != null) { room.topPortal.gameObject.SetActive(false);; }
        if (room.bottomPortal != null) { room.bottomPortal.gameObject.SetActive(false); }
        if (room.rightPortal != null) { room.rightPortal.gameObject.SetActive(false); }
        if (room.leftPortal != null) { room.leftPortal.gameObject.SetActive(false); }
        healthBar.SetActive(true);

        Activate(fp);

    }

    private void Activate(Transform followPoint)
    {
        activated = true;
        AudioManager.instance.Play(sound);
        dragonMaker.SpawnDragon();
        if (particleActivation) particleActivation.SetActive(true);
        bossRoomCam.Priority = 100;
        bossRoomCam.m_Lens.OrthographicSize = 14;
        bossRoomCam.Follow = followPoint;
    }

    
}
