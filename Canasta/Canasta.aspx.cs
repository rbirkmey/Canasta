using System;
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
            List<Card> deck = new List<Card>();
            List<Card> yourHand = new List<Card>();
            int playerScore = 0;

            // 4 players play with 5 decks of cards including 2 jokers in each
            for (int x = 0; x < 5; x++)
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

                // Transfers the last 15 cards from the shuffled deck to your hand
                for (int x = 0; x < 15; x++)
                {
                    yourHand.Add(shuffledDeck[shuffledDeck.Count - 1]);
                    shuffledDeck.RemoveAt(shuffledDeck.Count - 1);
                }

                Session.Add("deck", shuffledDeck);
                Session.Add("hand", yourHand);
            }
            else
            {
                shuffledDeck = (List<Card>)System.Web.HttpContext.Current.Session["deck"];
                yourHand = (List<Card>)System.Web.HttpContext.Current.Session["hand"];
            }

            // Displays shuffled deck

            /*foreach (Card card in shuffledDeck)
            {
                Image image = new Image();
                image.ImageUrl = "Images/" + card.CardValue + card.CardSuit + ".png";
                image.Height = 100;
                TestImages.Controls.Add(image);
            }*/

            // Displays your hand
            foreach (Card card in yourHand)
            {
                ImageButton image = new ImageButton();
                
                image.ImageUrl = "Images/" + card.CardValue + card.CardSuit + ".png";
                image.Height = 100;
                TestImages.Controls.Add(image);                
                playerScore = playerScore + card.PointValue;
                TextBox1.Text = playerScore.ToString();
            }
        }
    }
}