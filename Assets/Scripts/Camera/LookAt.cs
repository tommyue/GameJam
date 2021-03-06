using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] string _playerTag;

    CinemachineVirtualCamera _virtualCam;
    GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        _virtualCam = GetComponent<CinemachineVirtualCamera>();
        //transform.position = _player.transform.position;
        _virtualCam.ForceCameraPosition(_player.transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(_virtualCam.Follow == null)
        {
            _virtualCam.Follow = _player.transform;
            _virtualCam.ForceCameraPosition(new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z), transform.rotation);
        }
    }
}
