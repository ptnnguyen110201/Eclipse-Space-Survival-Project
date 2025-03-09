using System;
using UnityEngine;
[Serializable]
public class PlayerEnergy
{
    public EnergyData energyData;
    public int Amount;
    public int MaxAmount = 30;

    public string lastUpdateTime;
    public string nextUpdateTime;
    public int recoveryInterval = 5;



    public bool RecoverEnergyByTime()
    {
        if (Amount >= MaxAmount) return false;

        if (string.IsNullOrEmpty(lastUpdateTime))
        {
            lastUpdateTime = DateTime.Now.ToString();
            nextUpdateTime = CalculateNextUpdateTime(lastUpdateTime);
            return false;
        }

        DateTime lastUpdate, nextUpdate;
        if (!DateTime.TryParse(lastUpdateTime, out lastUpdate) || !DateTime.TryParse(nextUpdateTime, out nextUpdate))
        {
            lastUpdateTime = DateTime.Now.ToString();
            nextUpdateTime = CalculateNextUpdateTime(lastUpdateTime);
            return false;
        }

        if (DateTime.Now >= nextUpdate)
        {
            int minutesPassed = (int)(DateTime.Now - lastUpdate).TotalMinutes;
            int energyToRecover = minutesPassed / recoveryInterval;

            if (energyToRecover > 0)
            {
                Amount = Mathf.Min(Amount + energyToRecover, MaxAmount);
                lastUpdateTime = DateTime.Now.ToString();
                nextUpdateTime = CalculateNextUpdateTime(lastUpdateTime);
                return true;
            }
        }
        return false;
    }
    private string CalculateNextUpdateTime(string lastUpdateTime)
    {
        DateTime lastUpdate;
        if (DateTime.TryParse(lastUpdateTime, out lastUpdate))
        {
            return lastUpdate.AddMinutes(recoveryInterval).ToString();
        }
        return DateTime.Now.AddMinutes(recoveryInterval).ToString();
    }
}
