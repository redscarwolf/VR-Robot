using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

using UnityEngine;

using IBM.Watson.DeveloperCloud.Widgets;
using IBM.Watson.DeveloperCloud.DataTypes;
using IBM.Watson.DeveloperCloud.Utilities; // necessary?
using IBM.Watson.DeveloperCloud.Services.SpeechToText.v1;
using IBM.Watson.DeveloperCloud.Services.ToneAnalyzer.v3;


public class StateController : Widget {

	[SerializeField]
	private Input m_SpeechInput = new Input("SpeechInput", typeof(SpeechToTextData), "OnSpeechInput");

	[SerializeField]
	private float emotionThreshold = 0.5f;

	ToneAnalyzer m_ToneAnalyzer = new ToneAnalyzer();
	private Animator animator;
    private int terrifiedTrigger = Animator.StringToHash("TERRIFIEDTRIGGER");
    private int happyTrigger = Animator.StringToHash("HAPPYTRIGGER");
    private int boredTrigger = Animator.StringToHash("BOREDTRIGGER");
    private int sadTrigger = Animator.StringToHash("SADTRIGGER");

    #region InitAndLifecycle

    void Start()
	{
		// auto connect with other widgets
		base.Start();

		animator = GetComponent<Animator>();
	}

	// abstract method of Widget has to be overridden
	protected override string GetName()
	{
		return "StateController";
	}

	#endregion


	#region EventHandlers

	private void OnSpeechInput(Data data)
	{
		SpeechRecognitionEvent result = ((SpeechToTextData)data).Results;
		if (result != null && result.results.Length > 0)
		{
			foreach (var res in result.results)
			{
				foreach (var alt in res.alternatives)
				{
					if (res.final && alt.confidence > 0)
					{
						string text = alt.transcript;
						// Debug.Log("Speech result: " + text + " Confidence: " + alt.confidence);

						// use recognized speech as input for ToneAnalyzer service
						m_ToneAnalyzer.GetToneAnalyze(OnGetToneAnalyze, text, "TEST");
					}
				}
			}
		}
	}

	// callback method after ToneAnalyzer has done its job
	private void OnGetToneAnalyze(ToneAnalyzerResponse resp, string data)
	{    
		// Debug.Log("ToneAnalyzer Response: " + resp + " - " + data);

		// dismiss other categories for now
		ToneCategory emotions = resp.document_tone.tone_categories.First (category => category.category_id == "emotion_tone");

		// find strongest one of the five emotions
		Tone maxEmotion = emotions.tones.OrderByDescending (tone => tone.score).FirstOrDefault ();
		// Debug.Log ("Strongest detected emotion: " + maxEmotion.tone_name + " Score: " + maxEmotion.score);

		// TODO: Adjust value so that it feels right
		if (maxEmotion.score > emotionThreshold)
		{
            triggerAnimation(maxEmotion.tone_name);
            Debug.Log(">>>Emotion (" + maxEmotion.tone_name + "): " + maxEmotion.score);
        }
        else
        {
            Debug.Log("???Unshure Emotion (" + maxEmotion.tone_name + "): " + emotionThreshold + ">" + maxEmotion.score);
        }
	}

    private void triggerAnimation(string tone_name)
    {
        switch (tone_name)
        {
            case "Joy":
                animator.SetTrigger(happyTrigger);
                break;
            case "Sadness":
                animator.SetTrigger(sadTrigger);
                break;
            case "Anger":
                animator.SetTrigger(terrifiedTrigger);
                break;
            case "Disgust":
                // TODO
                break;
            case "Fear":
                // TODO
                break;
            default:
                break;
        }
    }

    #endregion
}