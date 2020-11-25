using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Sort_Search
{
    class Card : IComparable<Card>
    {
        public string Suit { get; set; }
        public int Value { get; set; }
        public string StringVal { get; set; }
        public Card(string suit, int value, string faceVal)
        {
            Suit = suit;
            Value = value;
            StringVal = faceVal;
        }
        public int CompareTo(Card other)
        {
            if (other == null) return 1;
            return Value.CompareTo(other.Value);
            throw new NotImplementedException();
        }
     
    }
  
    class Deck
    {
        public List<Card> deck = new List<Card>();
        private static readonly Random rand = new Random();
        private static readonly object synLock = new object();
        bool isSorted;
        string[] stringCards = new string[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
        string[] suits = new string[] {"Clubs","Spades","Hearts","Diamonds" };
        public Deck()
        {
            foreach(string suit in suits)
            {
                int numVal = 1;
                foreach(var stringval in stringCards)
                {
                    Card newCard = new Card(suit, numVal, stringval);
                    deck.Add(newCard);
                    numVal++;
                }
            }
        }
        /*------Print the cards------*/
        public void Print()
        {
            foreach(Card card in deck)
            {
                Console.WriteLine($"\n{card.StringVal} of {card.Suit}");
            }
        }
        /*------Shuffle------*/
        public void Shuffle()
        {
            isSorted = false;
            for (int i =0; i < deck.Count; i++)
            {
                var temp = deck[i];
                var index = RandomNumber(0, deck.Count);
                deck[i] = deck[index];
                deck[index] = temp;
            }
        }
        public int RandomNumber(int min, int max)
        {
            lock (synLock)
            {
                return rand.Next(min, max);
            }
        }

        /*------Sorting------*/
        public void Sort()//with insertion sort
        {
            isSorted = true;
            for(int i =1; i < deck.Count; i++)
            {
                Card nextCard = deck[i];
                int j = i;
                while(j > 0 && deck[j-1].CompareTo(nextCard) > 0 )
                {
                    deck[j] = deck[j - 1];
                    j--;
                }
                deck[j] = nextCard;
            }
 
        }
       
        /*------isSorted or not------*/
        public void SortedOrNot()
        {
            if (!isSorted)
            {
                Console.Write("\nCards not been sorted.");
            }
            else
            {
                Console.Write("\nCards been sorted.");
            }
        }
        /*------Pick card------*/
        public void Pick(int val, string face)
        {
            int pos = 0;
            for(int i= 0; i < deck.Count; i++)
            {
                if(deck[i].Value == val && deck[i].Suit == face)
                {
                    pos = i;
                    Console.Write("\nThe card you picked is located at " + pos);
                }
            }
            
        }

 

        static void Main(string[] args)
        {
            Deck test = new Deck();
            Console.Write("------Shuffling------");
            test.Shuffle();
            test.Print();
            test.SortedOrNot();
            Console.Write("\n*************************");
            Console.Write("\n------Sorting------");
            test.Sort();
            test.Print();
            test.SortedOrNot();
            Console.Write("\n*************************");
            Console.Write("\n------Pick a card------");
            Console.Write("\nEnter a number:");
            int val = Convert.ToInt32(Console.ReadLine());
            Console.Write("\n*************************");
            Console.Write("\nEnter a face:");
            string face = Console.ReadLine();
            Console.Write("\n*************************");
            test.Pick(val, face);
        }

       
    }

 
}
