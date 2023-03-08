using System;
using System.Collections.Generic;
using UnityEngine;

public class CooldownManager : MonoBehaviour
{
    class CooldownRecord
    {
        public float timer;
        public System.Action callback;

        public bool Tick(float deltaTime)
        {
            timer -= deltaTime;
            if (timer <= 0)
            {
                callback?.Invoke();
                return true;
            }
            return false;
        }
    }

    private Dictionary<string, CooldownRecord> cooldownRecords = new Dictionary<string, CooldownRecord>();

    public void BeginCooldown(string token, float duration, System.Action completionCallback = null)
    {
        CooldownRecord record = new CooldownRecord() { timer = duration, callback = completionCallback };
        cooldownRecords[token] = record;
    }

    private void Update()
    {
        List<string> tokensToRemove = new List<string>();
        foreach (KeyValuePair<string, CooldownRecord> pair in cooldownRecords)
        {
            if (pair.Value.Tick(Time.deltaTime))
            {
                tokensToRemove.Add(pair.Key);
            }
        }

        foreach (string token in tokensToRemove)
        {
            cooldownRecords.Remove(token);
        }
    }

    public float CooldownTimeRemaining(string token)
    {
        if (cooldownRecords.ContainsKey(token)) return cooldownRecords[token].timer;
        return 0;
    }

    public bool CooldownActive(string token) => cooldownRecords.ContainsKey(token);
}