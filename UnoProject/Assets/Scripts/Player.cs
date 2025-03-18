using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoTerminal
{
    public class Player {

        private string? name = "N/A";
        private List<Card> hand = new List<Card>();
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public List<Card> Hand
        {   get { return this.hand; } 
            set { this.hand = value; }
        }

        public Player() : this(new List<Card>())
        {

        }
        public Player(string aName)
        {
            Name = aName;
        }

        public Player(List<Card> aHand)
        {
            Hand = aHand;
        }

        public Player(List<Card> aHand, string aName)
        {
            Name = aName;
            Hand = aHand;
        }

        public void RemoveFromHand(Card aCard)
        {
            Hand.Remove(aCard);
        }
        public void AddToHand(Card aCard)
        {
            Hand.Add(aCard);
        }

        public List<Card> GetHand()
        {
            return this.Hand;
        }
        public void ViewHand()
        {
            Console.WriteLine("--Your Hand--\n");
            foreach (Card card in Hand)
            {
                
                Console.WriteLine("---------------");
                Console.WriteLine(card);
                
            }
        }

        public override string ToString()
        {
            string message = $"Player Name: {Name}";
            return message;
        }
        // Can make a class that is the game manager basically 
        // It uses random to add cards to a list.
        // Example: 
        // public void CreateHand(List<Card>) 
        // Takes in a list of cards, then it randomly takes from the Game Deck. And adds it to the players hand. 
        // This can be done using "Random" class/method. 
        // Work on this after you get home. 


    }
}
