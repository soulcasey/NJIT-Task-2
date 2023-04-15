using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class VoiceService : MonoBehaviour
{
    public static VoiceService Instance = null;

    public List<VoiceAction> voiceActions = new List<VoiceAction>()
    {
        new StopAction(),
        new WalkAction(),
        new BounceAction(),
        new RollAction(),
        new JumpAction(),
        new SpinAction(),
        new LeftAction(),
        new RightAction(),
        new BackwardAction(),
        new ForwardAction(),
        new BarkAction(),
    };
    public UnityEvent<VoiceAction> VoiceActionEvent = new UnityEvent<VoiceAction>();

    private KeywordRecognizer keywordRecognizer;

    private void Awake()
    {
        Instance = this;
        SetVoiceService();
    }

    public void SetVoiceService()
    {

        string[] voiceActionCommands = voiceActions.Select(voiceAction => voiceAction.GetVoiceActionCommand()).ToArray();

        keywordRecognizer = new KeywordRecognizer(voiceActionCommands);
        keywordRecognizer.OnPhraseRecognized += HandleOnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void HandleOnPhraseRecognized(PhraseRecognizedEventArgs phraseRecognizedEventArgs)
    {
        string speech = phraseRecognizedEventArgs.text;

        VoiceAction recognizedVoiceAction = voiceActions.First(voiceAction => voiceAction.GetVoiceActionCommand() == speech);

        if (recognizedVoiceAction != null)
        {
            VoiceActionEvent?.Invoke(recognizedVoiceAction);
        }
    }
}
