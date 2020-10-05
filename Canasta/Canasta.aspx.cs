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
            List<Card> cardList = new List<Card>();

            // 4 players play with 5 decks of cards including 2 jokers in each
            for (int x = 0; x < 5; x++)
            {
                // Arbitrarily decided red joker is diamonds and black joker is spades
                Card jokerCard = new Card(Card.Value.Joker, Card.Suit.Diamonds);
                cardList.Add(jokerCard);
                jokerCard = new Card(Card.Value.Joker, Card.Suit.Spades);
                cardList.Add(jokerCard);

                foreach (Card.Suit cardSuit in Enum.GetValues(typeof(Card.Suit)))
                {
                    foreach (Card.Value cardValue in Enum.GetValues(typeof(Card.Value)))
                    {
                        if (cardValue == Card.Value.Joker)
                        {
                            continue;
                        }

                        Card card = new Card(cardValue, cardSuit);
                        cardList.Add(card);
                    }
                }
            }

            foreach (Card card in cardList)
            {
                Image image = new Image();
                image.ImageUrl = "Images/" + card.CardValue + card.CardSuit + ".png";
                image.Height = 100;
                TestImages.Controls.Add(image);
            }
        }
    }
}