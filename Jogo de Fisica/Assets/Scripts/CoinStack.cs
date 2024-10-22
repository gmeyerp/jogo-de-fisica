using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinStack : MonoBehaviour
{
    private Coin topCoin;

    private void Awake()
    {
        topCoin = null;
    }

    public void Push(Coin stackable)
    {
        // Pushing the node to the top of the stack
        if (topCoin == null)
        { stackable.Stack(transform); }
        else
        { stackable.Stack(topCoin.StackPivot); }

        // Updating the top node
        topCoin = stackable;
    }

    public Coin Pop()
    {
        // Storing the node that will be popped
        Coin popped = topCoin;

        // Updating the top node
        if (topCoin != null)
        { topCoin = topCoin.StackedOn.parent.GetComponent<Coin>(); }

        // Returning the popped node
        return popped;
    }

    public void Collect()
    {
        const float collectTime = 0.3f;

        for (int i = 0; topCoin != null; i++)
        {
            Coin coin = Pop();
            coin.Invoke(nameof(coin.Collect), i * collectTime);
        }
    }

    public Coin TopCoin => topCoin;

    public class EmptyException : Exception
    {
        public EmptyException() : base("A pilha não possui elementos.")
        { }
    }
}
