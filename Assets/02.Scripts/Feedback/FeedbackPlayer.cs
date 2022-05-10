using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    [SerializeField]
    private List<Feedback> _feedbackToPlay = null;

    public void PlayFeedBack()
    {
        FinishFeedBack();
        foreach(Feedback f in _feedbackToPlay)
        {
            f.CreateFeedback();
        }
    }
    public void FinishFeedBack()
    {
        foreach(Feedback f in _feedbackToPlay)
        {
            f.CompleteFeedback();
        }
    }
}
