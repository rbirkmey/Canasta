using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Canasta
{
    public class Card
    {
        public enum Value
        {
            Joker,
            Ace,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        }

        public enum Suit
        {
            Clubs,
            Spades,
            Hearts,
            Diamonds
        }

        private Value cardValue;
        private Suit cardSuit;

        public Card(Value cardValue, Suit cardSuit)
        {
            this.cardValue = cardValue;
            this.CardSuit = cardSuit;
        }

        public Value CardValue
        {
            get
            {
                return this.cardValue;
            }
            set
            {
                this.cardValue = value;
            }
        }

        public Suit CardSuit
        {
            get
            {
                return this.cardSuit;
            }
            set
            {
                this.cardSuit = value;
            }
        }
    }
}