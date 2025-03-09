using UnityEngine;

[CreateAssetMenu(fileName = "GoldsData", menuName = "Currency/GoldsData")]
public class GoldsData : ScriptableObject
{
    public Sprite GoldsSprite;
    public CurrencyType CurrencyType = CurrencyType.Golds;
}
