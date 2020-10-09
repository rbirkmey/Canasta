﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Canasta
{
    public partial class Canasta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int numberOfHands = 4;
            List<Card> deck = new List<Card>();
            List<Card>[] hands = new List<Card>[numberOfHands];
            int[] playerScore = new int[numberOfHands];

            // 4 players play with 5 decks of cards including 2 jokers in each
            for (int x = 0; x < hands.Length+1; x++)
            {
                // Arbitrarily decided red joker is diamonds and black joker is spades
                Card jokerCard = new Card(Card.Value.Joker, Card.Suit.Diamonds);
                deck.Add(jokerCard);
                jokerCard = new Card(Card.Value.Joker, Card.Suit.Spades);
                deck.Add(jokerCard);

                foreach (Card.Suit cardSuit in Enum.GetValues(typeof(Card.Suit)))
                {
                    foreach (Card.Value cardValue in Enum.GetValues(typeof(Card.Value)))
                    {
                        if (cardValue == Card.Value.Joker)
                        {
                            continue;
                        }

                        Card card = new Card(cardValue, cardSuit);
                        deck.Add(card);
                    }
                }
            }
            List<Card> shuffledDeck = new List<Card>();

            if (System.Web.HttpContext.Current.Session["deck"] == null)
            {
                // Shuffles deck
                Random rand = new Random();
                shuffledDeck = deck.OrderBy(card => rand.Next()).ToList();

                for (int x = 0; x < hands.Length; x++)
                {
                    hands[x] = new List<Card>();
                    // Transfers the last 15 cards from the shuffled deck to your hand
                    for (int y = 0; y < 15; y++)
                    {
                        hands[x].Add(shuffledDeck[shuffledDeck.Count - 1]);
                        shuffledDeck.RemoveAt(shuffledDeck.Count - 1);
                    }

                    Session.Add("deck", shuffledDeck);
                    Session.Add("hand" + x+1, hands[x]);
                }
            }
            else
            {
                shuffledDeck = (List<Card>)System.Web.HttpContext.Current.Session["deck"];
                for (int x = 0; x < hands.Length; x++)
                {
                    hands[x] = (List<Card>)System.Web.HttpContext.Current.Session["hand" + x+1];
                }
            }

            // Displays hands
            for (int x = 0; x < hands.Length; x++)
            {
                playerScore[x] = 0;

                foreach (Card card in hands[x])
                {
                    ImageButton image = new ImageButton
                    {
                        ImageUrl = "Images/" + card.CardValue + card.CardSuit + ".png",
                        Height = 100
                    };

                    TestImages.Controls.Add(image);
                    playerScore[x] = playerScore[x] + card.PointValue;
                }

                TextBox textbox = new TextBox
                {
                    ID = "Player" + (x + 1) + "Score",
                    Text = playerScore[x].ToString()
                };

                TestImages.Controls.Add(textbox);
                TestImages.Controls.Add(new LiteralControl("<br />"));
            }
        }
    }
}