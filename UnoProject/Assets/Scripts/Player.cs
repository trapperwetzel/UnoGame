using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnoTerminal
{
    public class Player {

        private string? name = "N/A";
        private List<Card> hand = new List<Card>();
        //Pool object
        public List<GameObject> SpawnedCardObjects { get; } = new List<GameObject>();
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
        // Used for stacking draw two cards
        public void DrawCards(int count, GamePlay gamePlay)
        {
            for (int i = 0; i < count; i++)
            {
                gamePlay.DrawCard(this);
            }
        }


        public bool HasDrawTwoCard()
        {
            return Hand.Any(card => card.TypeOfCard == CardType.DrawTwo);
        }

        public void PlayDrawTwoCard()
        {
            var drawTwoCard = Hand.FirstOrDefault(card => card.TypeOfCard == CardType.DrawTwo);
            if (drawTwoCard != null)
            {
                Hand.Remove(drawTwoCard);
                Debug.Log($"{Name} played a DrawTwo!");
            }
        }


    }
}
