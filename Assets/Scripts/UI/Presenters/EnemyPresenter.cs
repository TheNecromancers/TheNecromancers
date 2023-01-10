using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheNecromancers.StateMachine.Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPresenter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ExclamationMark;
    [SerializeField] TextMeshProUGUI QuestionMark;

    private void OnEnable()
    {
    }

    private void Start()
    {
        QuestionMark.enabled = false;
        ExclamationMark.enabled = false;
    }

    public async void ShowQuestionMark(int duration = 1)
    {
        QuestionMark.enabled = true;
        await Task.Delay(duration * 1000);
        QuestionMark.enabled = false;
    }

    public async void ShowExclamationMark(int duration = 1)
    {
        ExclamationMark.enabled = true;
        await Task.Delay(duration * 1000);
        ExclamationMark.enabled = false;
    }
 
}
