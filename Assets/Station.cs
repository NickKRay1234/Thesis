using System;
using UnityEngine;

public sealed class Station : MonoBehaviour
{
    [SerializeField]
    private Transform _passangerContainer;
    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < _passangerContainer.childCount; i++)
        {
            _passangerContainer.GetChild(i).gameObject.SetActive(false);
        }
    }
}
