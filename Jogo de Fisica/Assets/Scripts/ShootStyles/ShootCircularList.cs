using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
[Serializable]
public class ShootCircularList
{
    public Item first;
    public Item last;
    public Item second;
    public Item third;
    public Item currentShooter;
    public int size { get; private set; }

    public ShootCircularList()
    {
        first = null;
        last = null;
        size = 0;
    }

    public void Add(ShootStyle style)
    {
        Item aux = new Item(style);
        if (size == 0)
        {
            first = aux;
            last = aux;
            last.next = aux;
            currentShooter = first;
        }
        else
        {
            last.next = aux;
            last = aux;
            last.next = first;
        }
        size++;
    }

    public void AddAt(ShootStyle style, int index)
    {
        if (index < 0 ||  index > size)
        { throw new Exception("Invalid index"); }
        Item aux = new Item(style);
        if (index == 0)
        {
            aux.next = first;
            last.next = aux;
            first = aux;
            currentShooter = first;
        }
        else if (index == size)
        {
            last.next = aux;
            last = aux;
            aux.next = first;
        }
        else
        {
            int i = 1;
            Item current = first;
            for (; i < index - 1; i++, current = current.next) { };
            aux.next = current.next;
            current.next = aux;
        }
        size++;
    }

    public void Remove(ShootStyle style)
    {
        if (size == 0)
        {
            throw new Exception("Empty list");
        }
        if (Equals(first.style, style)) //caso de ser o primeiro elemento
        {
            if (first == last) //caso seja o unico elemento
            {
                first = null;
                last = null;
            }
            else //caso tenham outros elementos
            {
                if (currentShooter == first) //medida de proteção do current shooter
                {
                    currentShooter = first.next;
                }
                first = first.next;
                last.next = first;
            }            
            size--;
            return;
        }
        Item aux;
        Item ant;
        for (aux = first.next, ant = first ; aux != first; aux = aux.next, ant = ant.next)
        {
            if (Equals(aux.style, style))
            {
                if (currentShooter == aux) //medida de proteção do current shooter
                {
                    currentShooter = aux.next;
                }
                ant.next = aux.next;
                if (ant.next == first) //se for o novo ultimo elemento
                {
                    last = ant;
                }
                size--;
                return;
            }
        }
        throw new Exception("Element not found");
    }

    public void RemoveAt(int index)
    {
        if (size == 0)
        { throw new Exception("Empty List"); }    
        if (index < 0 || index >= size)
        { throw new Exception("Invalid index"); }
        if (index == 0)
        {
            if (first == last) //caso seja o unico elemento
            {
                first = null;
                last = null;
            }
            else //caso tenham outros elementos
            {
                if (currentShooter == first) //medida de proteção do current shooter
                {
                    currentShooter = first.next;
                }
                first = first.next;
                last.next = first;
            }
            size--;
            return;
        }
        else
        {
            int i = 1;
            Item aux = first.next;
            Item ant = first;
            for (; i < index; i++, aux = aux.next, ant = ant.next) { };

            if (currentShooter == aux) //medida de proteção do current shooter
            {
                currentShooter = aux.next;
            }

            ant.next = aux.next;
            if (ant.next == first)
            {
                last = ant;
            }
            size--;
            return;
        }        
    }

    public bool ReplaceAt(ShootStyle style, int index)
    {
        // Validating the index
        if (index < 0 || size < index)
        { throw new IndexOutOfRangeException(); }

        // Finding the item at the index
        Item item = first;
        for (int i = 0; i != index; i++)
        { item = item.next; }

        // Validating the value
        if (item.style == style)
        { return false; }

        // Changing the item
        item.style = style;
        return true;
    }

    public ShootStyle ElementAt(int index)
    {
        if (index < 0 || size <= index)
        { Debug.LogError("Tentando acessar um índice inválido"); }

        Item current = first;
        for (int i = 0; i != index; i++)
        { current = current.next; }

        return current.style;
    }

    public void Shoot(Vector3 direction, Vector3 spawnPosition, Quaternion spawnRotation, float weaponPower)
    {
        if (currentShooter == null)
            throw new Exception("Null shooter");
        currentShooter.style.Shoot(direction, spawnPosition, spawnRotation, weaponPower);
        currentShooter = currentShooter.next;
    }
}

[Serializable]
public class Item
{
    public ShootStyle style;
    public Item next;

    public Item(ShootStyle style) 
    {
        this.style = style;
    }
}
