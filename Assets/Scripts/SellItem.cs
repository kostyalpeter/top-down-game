using UnityEngine;

public class SellItem : MonoBehaviour, IItem
{
    CoinManager coinManager;
    public int SellAmount = 10;

    public void Use()
    {
        coinManager.AddCoin(SellAmount); //i use this so it doesent use this item if i click on it, only the items i want, so its not a bug, its because i want it like this and you know if it works dont touch it so yeah please dont look at this as a bug or smth thank yall  
    }
}
