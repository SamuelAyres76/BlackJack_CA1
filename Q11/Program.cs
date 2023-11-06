/*
 *  Name:               Samuel Ayres
 *  Student Number:     S00237258
 *  
 *  Beginning Date:     19/10/2023
 *  Completion Date:    06/11/2023
 *  
 *  BlackJack CA
 *  
 *  Additional Features:
 *                      - Player is asked for their name.
 *                      - Player is told the card type + number.
 *                      - 5 Achievements to unlock!
 *                      
*/
using BlackJack;

namespace BlackJack
{
    // The User Class Stores their name and achievements.

    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * #################################################################################################################
             *       - Introduction
             * #################################################################################################################
             */

                // Introduction
                    Console.Write("- Please enter your username: ");
                    string usernameInput = Console.ReadLine();

                // Initialise this users data.
                    User user = new User();
                    user.Username = usernameInput;
                    user.WinGame = false;
                    user.LoseGame = false;
                    user.Lose3InRow = false;
                    user.Win3InRow = false;
                    user.ScoreBlackJack = false;
                    user.WinStreak = 0;
                    user.LossStreak = 0;

                    Console.WriteLine($"\n- Hello, {user.Username}! Welcome to BlackJack!");

            /*
             * #################################################################################################################
             *       - Main Menu
             * #################################################################################################################
             */

                while (true)
                {
                    Console.WriteLine("\n-----------------------------------------------\n");
                    Console.WriteLine("Menu:");
                    Console.WriteLine("1. Play BlackJack");
                    Console.WriteLine("2. View Achievements");
                    Console.WriteLine("3. Exit Game");
                    Console.Write("Please select an option (1-3): ");

                    string menuOption = Console.ReadLine();

                    switch (menuOption)
                    {
                        case "1": // BlackJack
                            Console.WriteLine($"\n-----------------------------------------------");
                            Console.WriteLine($"Let's Play BlackJack!");
                            Console.WriteLine($"-----------------------------------------------");
                            PlayBlackJack(user);
                            break;

                        case "2": // Achievements
                            ViewAchievements(user);
                            break;

                        case "3": // Exit the Program.
                            Console.Write($"\n- Thank you for playing!\n- See you soon!\n");
                            Console.ReadLine(); // User can press enter which will then exit the program.
                            return;

                        default: // Do nothing (invalid input)
                            break;
                    }
                }
        }

        // The Game.
        static void PlayBlackJack(User user)
        {
            /*
             * #################################################################################################################
             *       - Creating and Shuffling the cards1
             * #################################################################################################################
             */

                // Creating the Deck to hold the cards.
                List<(int, string)> deck = new List<(int, string)>();

                // Array of strings for each card type.
                string[] suits = new string[] { "Spades", "Clubs", "Hearts", "Diamonds" };

                // Creating all 52 cards.
                foreach (string suit in suits)
                {
                    // Cards from 2 to 10.
                    for (int i = 2; i <= 10; i++)
                    {
                        deck.Add((i, suit));
                    }

                    // Cards for the Jack, Queen and King.
                    for (int i = 0; i < 3; i++)
                    {
                        deck.Add((10, suit));
                    }

                    // The 4 Ace's.
                    deck.Add((11, suit));
                }

                // Shuffling the Deck.
                Random rand = new Random();

            /*
             * #################################################################################################################
             *       - Dealing the First Cards
             * #################################################################################################################
             */

                // Players First + Second Card
                    var playerCard1 = deck[rand.Next(deck.Count)];
                    var playerCard2 = deck[rand.Next(deck.Count)];

                    // Add to Deck
                        int playerTotal = playerCard1.Item1 + playerCard2.Item1;
            
                        Console.WriteLine($"\n- To start off you received: {playerCard1.Item1} of {playerCard1.Item2} and {playerCard2.Item1} of {playerCard2.Item2}!\n");

                // Dealers First + Second Card
                    var dealerCard1 = deck[rand.Next(deck.Count)];
                    var dealerCard2 = deck[rand.Next(deck.Count)];

                    // Add to Deck
                        int dealerTotal = dealerCard1.Item1 + dealerCard2.Item1;

                        Console.WriteLine($"- The dealer's first card is: {dealerCard1.Item1} of {dealerCard1.Item2}\n");

            /*
             * #################################################################################################################
             *       - User's Gameplay
             * #################################################################################################################
             */

                // Section repeats until the player sticks, goes bust, or gets a Blackjack.
                    while (playerTotal <= 21)
                    {
                        // Stick or Twist Prompt
                            Console.Write($"- {user.Username} your total is currently {playerTotal}. Would you like to stick or twist? (s / t): ");
                            string decision = Console.ReadLine().ToLower();

                            if (decision == "s")
                            {
                                // Do nothing (user is happy with their cards)
                                    break;
                            }
                            else if (decision == "t")
                            {
                                // Player receiving another card.
                                    var newCard = deck[rand.Next(deck.Count)];
                                    playerTotal += newCard.Item1;
                                    Console.WriteLine($"\n- You received the card: {newCard.Item1} of {newCard.Item2}");

                                // Ace Logic - If the new card is an Ace and the total is over 21, count it as a 1 instead.
                                    if (newCard.Item1 == 11 && playerTotal > 21)
                                    {
                                        playerTotal -= 10;
                                    }
                            }
                            else
                            {
                                // Do nothing (invalid input)
                            }
                    }

            /*
             * #################################################################################################################
             *       - Dealer's Gameplay
             * #################################################################################################################
             */

                // Dealers Second Card
                    Console.WriteLine($"\n- The dealer's second card is: {dealerCard2.Item1} of {dealerCard2.Item2}!");

                    // Section repeats until the dealer sticks or goes bust. 
                    // The Dealer will only pick a new card if their total is 16 or higher.
                        while (dealerTotal <= 16 && playerTotal <= 21)
                        {
                            // Dealer's Next Card
                                var newCard = deck[rand.Next(deck.Count)];
                                dealerTotal += newCard.Item1;
                                Console.WriteLine($"\n- The dealer took another card and got: {newCard.Item1} of {newCard.Item2}!");

                            // Ace Logic
                                if (newCard.Item1 == 11 && dealerTotal > 21)
                                {
                                    dealerTotal -= 10;
                                }
                        }

            /*
             * #################################################################################################################
             *       - Who Won?
             * #################################################################################################################
             */
                
                Console.WriteLine("\n<--- THE GAME IS OVER! --->");

                // Dealer Wins (User Went Bust)
                    if (playerTotal > 21)
                        {
                            Console.WriteLine("\n- You went bust! Better luck next time!");
                            user.LossStreak++;
                            user.WinStreak = 0;
                    
                            // Rewarding Achievements!
                                if (user.LossStreak == 3 && !user.Lose3InRowUnlocked)
                                {
                                    Console.WriteLine("\n- Achievement Unlocked: Lose 3 times in a row!");
                                    user.Lose3InRow = true;
                                    user.Lose3InRowUnlocked = true;
                                }
                                if (!user.LoseGameUnlocked) // Check if the achievement has not been unlocked yet
                                {
                                    user.LoseGame = true; // Unlocks the "lose a game" achievement.
                                    user.LoseGameUnlocked = true; // Mark the achievement as unlocked.
                                    Console.WriteLine("\n- Achievement Unlocked: Lose a game!"); // Notify the user they unlocked it!
                                }
                        }

                // Dealer Wins (Had the Higher Number)
                    else if (dealerTotal > playerTotal)
                        {
                            Console.WriteLine("\n- The dealer wins...");
                            user.LossStreak++;
                            user.WinStreak = 0;

                            // Rewarding Achievements!
                                if (user.LossStreak == 3 && !user.Lose3InRowUnlocked)
                                {
                                    Console.WriteLine("\n- Achievement Unlocked: Lose 3 times in a row!");
                                    user.Lose3InRow = true;
                                    user.Lose3InRowUnlocked = true;
                                }
                                if (!user.LoseGameUnlocked)
                                {
                                    user.LoseGame = true;
                                    user.LoseGameUnlocked = true;
                                    Console.WriteLine("\n- Achievement Unlocked: Lose a game!");
                                }
                        }

                // User Wins (Dealer Went Bust)
                    else if (dealerTotal > 21)
                        {
                            Console.WriteLine("\n- Dealer went bust! You win!");
                            user.WinStreak++;
                            user.LossStreak = 0;

                            // Rewarding Achievements!
                                if (user.WinStreak == 3 && !user.Win3InRowUnlocked)
                                {
                                    Console.WriteLine("\n- Achievement Unlocked: Win 3 times in a row!");
                                    user.Win3InRow = true;
                                    user.Win3InRowUnlocked = true;

                                }
                                if (!user.WinGameUnlocked)
                                {
                                    user.WinGame = true; 
                                    user.WinGameUnlocked = true; 
                                    Console.WriteLine("\n- Achievement Unlocked: Win a game!");
                                }
                        }

                // User Wins (Had the Higher Number)
                    else if (dealerTotal < playerTotal)
                        {
                            // BlackJack?
                                if (playerTotal == 21)
                                {
                                    Console.WriteLine("\n- You win by getting a BlackJack!");
                            
                                    if (!user.BlackJackUnlocked)
                                        {
                                            Console.WriteLine("\n- Achievement Unlocked: Score a BlackJack!");
                                            user.ScoreBlackJack = true;
                                        }
                                }
                                else
                                {
                                    Console.WriteLine("\n- You win!");
                                }

                                user.WinStreak++;
                                user.LossStreak = 0;

                            // Rewarding Achievements!
                                if (user.WinStreak == 3 && !user.Win3InRowUnlocked)
                                {
                                    Console.WriteLine("\n- Achievement Unlocked: Win 3 times in a row!");
                                    user.Win3InRow = true;
                                    user.Lose3InRowUnlocked = true;

                                }
                                if (!user.WinGameUnlocked)
                                {
                                    user.WinGame = true; 
                                    user.WinGameUnlocked = true; 
                                    Console.WriteLine("\n- Achievement Unlocked: Win a game!");
                                }
                        }

                // Neither Win (It's a Draw)
                    else
                    {
                        Console.WriteLine("\n- It's a draw!");
                    }

                    Console.Write("\n<--- Press enter to return to menu. ---> ");
                    Console.ReadLine();
            }

        /*
         * #################################################################################################################
         *       - The Achievements
         * #################################################################################################################
         */

            static void ViewAchievements(User user)
            {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine("Achievements:");
            Console.WriteLine("-----------------------------------------------");

            // Update the Achievement Display Based on the Achievement Variables.
            // Initially I used if else statements but that was super long and inefficient.
            // I decided to use "conditional operators".
                Console.WriteLine($"1. Win a game: {(user.WinGame ? "Unlocked!" : "Locked.")}");
                Console.WriteLine($"2. Lose a game: {(user.LoseGame ? "Unlocked!" : "Locked.")}");
                Console.WriteLine($"3. Lose 3 times in a row: {(user.Lose3InRow ? "Unlocked!" : "Locked.")}");
                Console.WriteLine($"4. Win 3 times in a row: {(user.Win3InRow ? "Unlocked!" : "Locked.")}");
                Console.WriteLine($"5. Score a BlackJack: {(user.ScoreBlackJack ? "Unlocked!" : "Locked.")}");
            }
    }
}
// END OF PROGRAM!!