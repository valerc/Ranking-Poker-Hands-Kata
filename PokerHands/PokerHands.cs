using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHands
{
    public enum Result
    {
        Win,
        Loss,
        Tie
    }

    enum Values
    {
        T = 10,
        J,
        Q,
        K,
        A
    }

    struct Card : IComparable<Card>
    {
        public double Value { get; }
        public char Suit { get; }

        public Card(string card)
        {
            Value = (int)Enum.Parse(typeof(Values), card.First().ToString());
            Suit = card.Last();
        }

        public int CompareTo(Card card)
        {
            return Value.CompareTo(card.Value);
        }
    }

    public class PokerHand : IComparable<PokerHand>
    {
        List<Card> list = new List<Card>(5);
        public double Rank { get; set; }
        string hand;

        public PokerHand(string hand)
        {
            this.hand = hand;
            var cards = hand.Split(' ');
            foreach (var item in cards)
                list.Add(new Card(item));
            list.Sort();
            FindRank();
        }

        private void FindRank()
        {
            // ROYAL FLUSH
            if (list.GroupBy(x => x.Value).Count() == 5 && list.Last().Value == 14 &&
                list.Last().Value - list.First().Value == 4 && list.GroupBy(x => x.Suit).Count() == 1)
            {
                foreach (var item in list)
                    Rank += item.Value * 1e9;
            }

            // STRAIGHT FLUSH
            else if (list.GroupBy(x => x.Suit).Count() == 1 && list.GroupBy(x => x.Value).Count() == 5 &&
                (list.Last().Value - list.First().Value == 4 || (list.Last().Value == 14 && list[3].Value == 5)))
            {
                if (list.Last().Value - list.First().Value != 4)
                    Rank -= 13 * 1e8;
                foreach (var item in list)
                    Rank += item.Value * 1e8;
            }

            // FOUR OF A KIND
            else if (list.GroupBy(x => x.Value).SingleOrDefault(x => x.Count() == 4) != null)
            {
                foreach (var item in list)
                    Rank += item.Value * 1e7;
            }

            // FULL HOUSE
            else if (list.GroupBy(x => x.Value).SingleOrDefault(x => x.Count() == 3) != null &&
                list.GroupBy(x => x.Value).SingleOrDefault(x => x.Count() == 2) != null)
            {
                foreach (var item in list)
                    Rank += item.Value * 1e6;
            }

            // FLUSH
            else if (list.GroupBy(x => x.Suit).Count() == 1)
            {
                foreach (var item in list)
                    Rank += item.Value * 1e5;
            }

            // STRAIGHT
            else if (list.GroupBy(x => x.Value).Count() == 5 && (list.Last().Value - list.First().Value == 4 ||
                (list.Last().Value == 14 && list[3].Value == 5)))
            {
                if (list.Last().Value - list.First().Value != 4)
                    Rank -= 13 * 1e4;
                foreach (var item in list)
                    Rank += item.Value * 1e4;
            }

            // THREE OF A KIND
            else if (list.GroupBy(x => x.Value).SingleOrDefault(x => x.Count() == 3) != null)
            {
                foreach (var item in list)
                    Rank += item.Value * 1e3;
            }

            // TWO PAIRS
            else if (list.GroupBy(x => x.Value).Where(x => x.Count() == 2).Count() == 2)
            {
                foreach (var item in list)
                    Rank += item.Value * 1e2;
            }

            // ONE PAIR
            else if (list.GroupBy(x => x.Value).Where(x => x.Count() == 2).Count() == 1)
            {
                foreach (var item in list)
                    Rank += item.Value == list.GroupBy(x => x.Value).SingleOrDefault(x => x.Count() == 2).Key ?
                        item.Value * 10 : item.Value;
            }

            // HIGH CARD
            else
            {
                for (int i = 0; i < list.Count; i++)
                    Rank += list[i].Value * Math.Pow(0.1, list.Count - i - 1);
            }
        }

        public Result CompareWith(PokerHand hand)
        {
            if (Rank > hand.Rank) return Result.Win;
            else if (Rank < hand.Rank) return Result.Loss;
            else return Result.Tie;
        }

        public int CompareTo(PokerHand other)
        {
            return other.Rank.CompareTo(Rank);
        }

        public override string ToString()
        {
            return hand;
        }
    }
}
