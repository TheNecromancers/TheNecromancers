using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum eComicIcon
{
    BackToMark,
    QuestionMark
}

public class ComicBubblePresenter : MonoBehaviour
{
    [SerializeField] GameObject QuestionMark;
    [SerializeField] GameObject BackToMark;

    async public void ShowComicBubble(float durationTime, eComicIcon icon)
    {
        switch (icon)
        {
            case eComicIcon.QuestionMark:
                QuestionMark.SetActive(true);
                break;

            case eComicIcon.BackToMark:
                BackToMark.SetActive(true);
                break;
        }
        await Task.Delay((int)durationTime * 1000);
        QuestionMark.SetActive(false);
        BackToMark.SetActive(false);
    }

    public void ShowComicBubble(eComicIcon icon)
    {
        switch (icon)
        {
            case eComicIcon.QuestionMark:
                QuestionMark.SetActive(true);
                break;

            case eComicIcon.BackToMark:
                BackToMark.SetActive(true);
                break;
        }
    }

    public void HideComicBubble()
    {
        QuestionMark.SetActive(false);
        BackToMark.SetActive(false);
    }
}
