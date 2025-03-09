using System;
using UnityEngine;

[System.Serializable]
public class CurrencyData
{
    public PlayerDiamonds playerDiamonds;
    public PlayerGolds playerGolds;
    public PlayerEnergy playerEnergy;
    public Action OnCurrencyUpdated;
    public bool IsEnoughtDiamonds() => this.playerDiamonds.Amount > 0;
    public bool CanConvertCurrencyToEnergy(PlayerData playerData,int amount, CurrencyType currencyType)
    {
        if(this.IsEnoughtDiamonds())
        if (amount <= 0)
        {
            Debug.LogWarning("Amount must be greater than 0.");
            return false;
        }

        switch (currencyType)
        {
            case CurrencyType.Energy:
                int energyRequired = amount * 1;
                this.SpendCurrency(playerData, amount, CurrencyType.Diamonds);
                return this.AddCurrency(playerData, energyRequired, CurrencyType.Energy);

            case CurrencyType.Golds:
                int goldsRequired = amount * 200;
                this.SpendCurrency(playerData, amount, CurrencyType.Diamonds);
                return this.AddCurrency(playerData, goldsRequired, CurrencyType.Golds);

            default:
                Debug.LogWarning("Invalid currency type for conversion.");
                return false;
        }
    }

    public bool HasEnoughCurrency(int amount, CurrencyType currencyType)
    {
        switch (currencyType)
        {
            case CurrencyType.Golds:
                return this.playerGolds.Amount >= amount;

            case CurrencyType.Diamonds:
                return this.playerDiamonds.Amount >= amount;
            case CurrencyType.Energy:
                return this.playerEnergy.Amount >= amount;
            default:
                Debug.LogWarning("Currency does not exist: " + currencyType);
                return false;
        }
    }
    public bool SpendCurrency(PlayerData playerData,int amount, CurrencyType currencyType)
    {
        switch (currencyType)
        {
            case CurrencyType.Golds:
                if (this.playerGolds.Amount >= amount)
                {
                    this.playerGolds.Amount -= amount;
                    this.OnCurrencyUpdated?.Invoke();
                    playerData.questDatas.UpdateQuestProgress(QuestType.SpeenGolds,amount);
                    return true;
                   
                }
                break;

            case CurrencyType.Diamonds:
                if (this.playerDiamonds.Amount >= amount)
                {
                    this.playerDiamonds.Amount -= amount;
                    this.OnCurrencyUpdated?.Invoke();
                    return true;
                }
                break;
            case CurrencyType.Energy:
                if (this.playerEnergy.Amount >= amount)
                {
                    this.playerEnergy.Amount -= amount;
                    this.OnCurrencyUpdated?.Invoke();
                    return true;
                }
                break;
            default:
                Debug.LogWarning("Currency does not exist: " + currencyType);
                break;
        }

        return false;
    }

    public bool AddCurrency(PlayerData playerData, int amount, CurrencyType currencyType)
    {
        if (amount <= 0) return false;

        switch (currencyType)
        {
            case CurrencyType.Golds:
                this.playerGolds.Amount += amount;
                this.OnCurrencyUpdated?.Invoke();
                playerData.questDatas.UpdateQuestProgress(QuestType.EearningGolds, amount);
                return true;

            case CurrencyType.Diamonds:
                this.playerDiamonds.Amount += amount;
                this.OnCurrencyUpdated?.Invoke();
                return true;
            case CurrencyType.Energy:
                this.playerEnergy.Amount += amount;
                this.OnCurrencyUpdated?.Invoke();
                return true;

            default:
                Debug.LogWarning("Currency does not exist: " + currencyType);
                return false;
        }
    }
   
    public int GetCurrencyAmount(CurrencyType currencyType)
    {
        return currencyType switch
        {
            CurrencyType.Golds => this.playerGolds.Amount,
            CurrencyType.Diamonds => this.playerDiamonds.Amount,
            _ => 0
        };
    }
    
}
