using UnityEngine;

[CreateAssetMenu(fileName = "DiamondsData", menuName = "Currency/DiamondsData")]
public class DiamondsData : ScriptableObject
{
    public Sprite DiamondsSprite;
    public CurrencyType CurrencyType = CurrencyType.Diamonds;
}
