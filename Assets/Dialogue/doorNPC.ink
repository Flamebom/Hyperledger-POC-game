INCLUDE Globals.ink
EXTERNAL TradePlayerKey()
Would you like to buy a key from me?
    *[Yes]
        {gemCount>=1:  ->GiveKey|Come back to me when you have more money .}
    *[No]
        Alright, it's your loss.
=== GiveKey ===
Have a key.
~ TradePlayerKey()
-> END

    
