using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class Tasks : MonoBehaviour
{
    static readonly string[] firstNames = { "Carol", "Adam", "Maria", "John", "Leila", "Michael", "Alex", "Joseph", "Frog", "Amy", "Anthony", "Avery", "Sarah", "Clay", "Cameron", "Shelby", "Nico", "Abby", "Liana", "Sam", "Eli", "Mark" };
    static readonly char[] lastInitials = { 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M' };

    Queue<string> loginQueue = new Queue<string>();

    [SerializeField] int task;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (task)
        {
            case 1:
                for (int i = 0; i < Random.Range(4, 7); i++)
                    loginQueue.Enqueue(GetRandomPlayerName());

                List<string> loginList = loginQueue.ToList();

                string initQueue = "Initial login queue created. There are " + loginList.Count + " players in the queue: ";
                for (int i = 0; i < loginList.Count; i++)
                {
                    if (i == loginList.Count - 1)
                        initQueue += loginList[i];
                    else
                        initQueue += (loginList[i] + ", ");
                }
                Debug.Log(initQueue);

                InvokeRepeating(nameof(AddPlayer), 0, Random.Range(1f, 10f));
                InvokeRepeating(nameof(LoginPlayer), 0, Random.Range(1f, 10f));

                break;
            
            case 2:

                break;
        }
    }

    string GetRandomPlayerName()
    {
        return (firstNames[Random.Range(0, firstNames.Length)] + " " + lastInitials[Random.Range(0, 26)] + ".");
    }

    void AddPlayer()
    {
        string name = GetRandomPlayerName();
        loginQueue.Enqueue(name);
        Debug.Log(name + " is trying to login and was added to the login queue.");
    }

    void LoginPlayer()
    {
        if (!loginQueue.Any())
            Debug.Log("Login server is idle.No players are waiting.");
        else
            Debug.Log(loginQueue.Dequeue() + " is now inside the game.");
    }
}
