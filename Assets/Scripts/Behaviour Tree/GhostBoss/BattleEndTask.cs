using BehaviourTree;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEndTask : Node
{
    private CinemachineVirtualCamera _virtualCamera;
    private float _camSpeed;
    private Transform _camPosition;
    private Animator _anim;
    private Collider2D _collider;
    private GameObject _bt;
    private Transform _player;

    private bool _camBackToNormal = false;

    public BattleEndTask(CinemachineVirtualCamera virCam, float camSpeed, Transform camPos, Transform _boss, GameObject bt, Transform player)
    {
        _virtualCamera = virCam;
        _camSpeed = camSpeed;
        _camPosition = camPos;
        _anim = _boss.GetComponent<Animator>();
        _collider = _boss.GetComponent<Collider2D>();
        _bt = bt;
        _player = player;
    }

    public override NodeState Evaluate()
    {
        bool battleEnded = GetData<bool>(GhostBossBT.BattleEndTag);
        if (battleEnded)
        {
            if (!_camBackToNormal)
            {
                _anim.SetTrigger("vanished");
                _collider.enabled = false;
                BossBullet[] bullets = GameObject.FindObjectsOfType<BossBullet>();
                foreach (var bullet in bullets)
                {
                    bullet.gameObject.SetActive(false);
                }

                if (Vector2.Distance(_virtualCamera.transform.position, _player.transform.position) > 0.5)
                {
                    Vector3 target = _player.transform.position;
                    target.z = -10;
                    _virtualCamera.transform.position = Vector3.Lerp(_virtualCamera.transform.position, target, _camSpeed * Time.deltaTime);
                }
                else
                {
                    _virtualCamera.GetComponent<LookAt>().enabled = true;

                    _bt.SetActive(false);
                }
            }
            else
            {
                _bt.SetActive(false);
                _virtualCamera.GetComponent<LookAt>().enabled = true;
            }
            return NodeState.SUCCESS;
        }
        else
        {
            _virtualCamera.transform.position = Vector3.Lerp(_virtualCamera.transform.position, _camPosition.position, _camSpeed * Time.deltaTime);
            return NodeState.FAILURE;
        }
    }
}
