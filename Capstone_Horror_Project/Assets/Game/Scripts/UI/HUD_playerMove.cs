using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_playerMove : MonoBehaviour
{
    [SerializeField] PlayerInput input = null;
    [SerializeField] Text moveText = null;

    private void OnEnable()
    {
        input.Moved += OnMoved;
    }

    private void OnMoved(Vector3 movement)
    {
        moveText.text = "" + movement;
    }
}
