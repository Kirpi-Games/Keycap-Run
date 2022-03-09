using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Scripts.Managers.StateMachine;
using DG.Tweening;
using UnityEngine;

public class Keycaps : MonoBehaviour
{
    public static Keycaps instance;
    public bool isCollect,finalMove;
    public GameObject parentPlayer;
    private Rigidbody _rigidbody;
    [SerializeField] private float swerveClamp;
    [SerializeField] private float forceToPull;

    private void Awake()
    {
        instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        GameStateManager.Instance.GameStatePlaying.OnExecute += ParentFollow;
        GameStateManager.Instance.GameStatePlaying.OnExecute += FinalMove;
        //GameStateManager.Instance.GameStateComplete.OnExecute += ParentFollow;
        //GameStateManager.Instance.GameStateComplete.OnExecute += FinalMove;
    }

    private void Start()
    {
        parentPlayer = ParentPlayer.instance.gameObject; 
    }

    private void ParentFollow()
    {
        if (isCollect)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
            if (!_rigidbody.isKinematic)
            {
                Vector3 playerPos = parentPlayer.transform.position - transform.position;
                playerPos.y = 0;
                playerPos.x = Mathf.Clamp(playerPos.x, -swerveClamp, swerveClamp);
                _rigidbody.velocity = playerPos.normalized * forceToPull;
                 
                
            }    
        }
    }

    public void LevelFailed()
    {
        GameStateManager.Instance.GameStateFail.OnExecute += ParentFollow;
        GameStateManager.Instance.GameStateFail.OnExecute += FinalMove;
    }
    
    public void DisableParentFollow()
    { 
        GameStateManager.Instance.GameStatePlaying.OnExecute -= ParentFollow;
        GameStateManager.Instance.GameStatePlaying.OnExecute -= FinalMove;
    }

    void FinalMove()
    {
        if (finalMove)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,Vector3.zero, 1 * Time.deltaTime);
            if (transform.localPosition == Vector3.zero)
            {
                GameStateManager.Instance.GameStatePlaying.OnExecute += SetPosZero;
                GameStateManager.Instance.GameStateComplete.OnExecute += SetPosZero;
                finalMove = false;
            }
        }
    }

    void SetPosZero()
    {
        transform.localPosition = Vector3.zero;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            if (isCollect)
            {
                other.GetComponent<CapsuleCollider>().isTrigger = false;
                other.gameObject.layer = 7;
                other.GetComponent<Keycaps>().isCollect = true;
                other.transform.SetParent(transform.parent);
                ParentPlayer.instance.DummyAdd(other.gameObject);
                other.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        if (other.gameObject.layer == 10)
        {
            Color door = new Color(other.GetComponent<ColorDoors>().doorColor.r, other.GetComponent<ColorDoors>().doorColor.g, other.GetComponent<ColorDoors>().doorColor.b, 255);
            GetComponent<MeshRenderer>().materials[0].DOColor(door, 0.2f);
        }
        if (other.gameObject.layer == 11)
        {
            GetComponent<MeshRenderer>().materials[1].color = other.GetComponent<RGBDoor>().rgbMaterial.color;
            GetComponent<MeshRenderer>().materials[1].SetColor("_EmissionColor", other.GetComponent<RGBDoor>().rgbMaterial.GetColor("_EmissionColor"));
        }
    }
}
