using System;
public static class CurrencyEvent
{
    public static event Action OnCurrencyUpdated;

    public static void TriggerCurrencyUpdated()
    {
        OnCurrencyUpdated?.Invoke();
    }
}
