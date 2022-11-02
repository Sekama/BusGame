using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptScript : MonoBehaviour
{
    [SerializeField] private EnergySystem _es;
    [SerializeField] private Player_Controller _pc;
    private TextMeshProUGUI _text;
    private bool _bHasMessage;
    private bool _bOffTickMessage;

    private void Awake()
    {
        _bHasMessage = false;
        _bOffTickMessage = false;
        _text = GetComponent<TextMeshProUGUI>();
        _es.EnergyChanged += ToggleEnergyPrompt;
        _pc.AtStation += ToggleStationPrompt;
    }

    public void SetMessage(string InMessage, Color InColor)
    {
        _text.enabled = true;
        _text.text = InMessage;
        _text.color = InColor;
    }

    public void ToggleEnergyPrompt()
    {
        if (!_bHasMessage)
        {
            if (_es.Energy < 40f)
            {
                SetMessage("Battery Low", new Color(1f, 0f, 0f));
                EnablePrompt(true);
            }
        }
        else if(!_bOffTickMessage)
        {
            if (_es.Energy > 40f)
            {
                DisablePrompt();
            }
        }
    }

    public void ToggleStationPrompt(bool InAtStation)
    {
        if (InAtStation)
        {
            SetMessage("At Station", new Color(1f, 1f, 1f));
            _bOffTickMessage = true;
            EnablePrompt(false);
        }
        else
        {
            _bOffTickMessage = false;
            DisablePrompt();
        }
    }

    private void EnablePrompt(bool bShouldFlipFlop)
    {
        _bHasMessage = true;
        if(bShouldFlipFlop)
        {
            FlipFlopMessage();
        }
        
    }

    private void DisablePrompt()
    {
        _bHasMessage = false;
        _text.enabled = false;
    }

    private void FlipFlopMessage()
    {
        if(_bHasMessage)
        {
            _text.enabled = !_text.enabled;
            Invoke("FlipFlopMessage", 0.25f);
        }
    }
}
