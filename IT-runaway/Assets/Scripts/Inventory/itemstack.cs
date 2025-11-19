using UnityEngine;


public class itemstack
{
    
    public ItemData item;

    public int amount;

    public itemstack(ItemData item = null, int amount = 0 )
    {
       this.amount = amount;
       this.item = item; 
    }

}