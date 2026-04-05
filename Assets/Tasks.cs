using NUnit.Framework;
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
            // Task 1
            case 1:
                // Fill initial queue
                for (int i = 0; i < Random.Range(4, 7); i++)
                    loginQueue.Enqueue(GetRandomPlayerName());

                // Convert to list
                List<string> loginList = loginQueue.ToList();

                // Use list to log all players in queue
                string initQueue = "Initial login queue created. There are " + loginList.Count + " players in the queue: ";
                for (int i = 0; i < loginList.Count; i++)
                {
                    if (i == loginList.Count - 1)
                        initQueue += loginList[i];
                    else
                        initQueue += (loginList[i] + ", ");
                }
                Debug.Log(initQueue);

                // Begin adding and logging in players
                InvokeRepeating(nameof(AddPlayer), 0, Random.Range(1f, 10f));
                InvokeRepeating(nameof(LoginPlayer), 0, Random.Range(1f, 10f));

                break;
            
            // Task 2
            case 2:
                // Create and fill list
                string[] names = new string[15];
                for (int i = 0; i < 15; i++)
                    names[i] = firstNames[Random.Range(0, firstNames.Length)];

                // Log the list
                Debug.Log("Created the name array: " + string.Join(", ", names));

                // Create HashSets
                HashSet<string> seen = new HashSet<string>();
                HashSet<string> duplicates = new HashSet<string>();

                // Iterate through the array and find duplicates
                foreach (string name in names)
                {
                    if (!seen.Add(name))
                        duplicates.Add(name);
                }

                // Log the duplicates
                if (duplicates.Count == 0)
                    Debug.Log("This array has no duplicate names.");
                else
                    Debug.Log("This array has duplicate names: " + string.Join(", ", duplicates));
                
                break;

            // Task 3
            case 3:
                // Create arrays for values and suits
                string[] suits = { "\u2660", "\u2663", "\u2665", "\u2666"};
                char[] vals = { 'J', 'Q', 'K', 'A' };

                // Create and fill deck
                List<string> deck = new List<string>();
                foreach (char val in vals)
                {
                    foreach (string suit in suits)
                        deck.Add(val + suit);
                }

                // Shuffle deck
                deck = deck.OrderBy(card => Random.Range(1, 20)).ToList();    

                // Draw and log starting hand
                List<string> hand = new List<string>();
                for (int i = 0; i < 4; i++)
                {
                    string draw = deck[0];
                    hand.Add(draw);
                    deck.Remove(draw);
                }
                Debug.Log("I made the initial deck and draw. My hand is: " + string.Join(", ", hand));

                // Check if the opening hand wins
                bool win = false;
                foreach (string suit in suits)
                {
                    List<string> suitCheck = hand.Where(card => card.Contains(suit)).ToList();
                    if (suitCheck.Count >= 3)
                    {
                        win = true;
                        break;
                    }
                }
                if (win)
                {
                    Debug.Log("The game is WON.");
                    return;
                }

                // Start the gameplay loop
                while (true)
                {
                    // Check if the deck is empty
                    if (!deck.Any())
                    {
                        Debug.Log("The deck is empty.The game is LOST.");
                        break;
                    }

                    // Discard a card
                    string discard = hand[Random.Range(0, 4)];
                    hand.Remove(discard);

                    // Draw a card
                    string draw = deck[0];
                    hand.Add(draw);
                    deck.Remove(draw);

                    // Check if the hand wins
                    foreach (string suit in suits)
                    {
                        List<string> suitCheck = hand.Where(card => card.Contains(suit)).ToList();
                        if (suitCheck.Count >= 3)
                        {
                            win = true;
                            break;
                        }
                    }
                    if (win)
                    {
                        Debug.Log("I discarded " + discard + " and drew " + draw + ". My hand is: " + string.Join(", ", hand) + ". The game is WON.");
                        break;
                    }

                    // Log the hand
                    Debug.Log("I discarded " + discard + " and drew " + draw + ". My hand is: " + string.Join(", ", hand) + ". This is not a winning hand. I will attempt to play another round.");
                }
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
