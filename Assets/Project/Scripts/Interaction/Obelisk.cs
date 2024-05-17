using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Obelisk : Interactable
{
    
    [SerializeField] private PieceSeparation dragonMaker;
    [SerializeField] private GameObject particleActivation;
    private bool activated;
    [SerializeField] private CinemachineVirtualCamera bossRoomCam;
    
    public override void OnInteract(BaseCharacter character)
    {
        if(activated) return;
        Transform fp = character.possessingSpirit.transform;
        Activate(fp);

    }

    private void Activate(Transform followPoint)
    {
        activated = true;
        dragonMaker.SpawnDragon();
        if (particleActivation) particleActivation.SetActive(true);
        bossRoomCam.Priority = 100;
        bossRoomCam.Follow = followPoint;
    }

    
}
