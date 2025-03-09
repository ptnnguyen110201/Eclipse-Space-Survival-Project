using UnityEngine;

[CreateAssetMenu(fileName = "EnergyData", menuName = "Currency/EnergyData")]
public class EnergyData : ScriptableObject
{
    public Sprite EnergySprite;
    public CurrencyType CurrencyType = CurrencyType.Energy;
}
