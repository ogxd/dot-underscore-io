using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ogx
{
    [Serializable]
    public class Person
    {
        private List<CreditCard> cards = new List<CreditCard>();

        public string Name { get; private set; }
        public DateTime BirthDate { get; private set; }
        public IEnumerable<CreditCard> Cards 
        {
            get { return cards; }
        }


        public Person(string name, DateTime birthDate)
        {
            Name = name;
            BirthDate = birthDate;
        }

        public Person(string name, DateTime birthDate, IEnumerable<CreditCard> someCards):this(name, birthDate)
        {
            cards.AddRange(someCards);
        }

        public void AddNewCard(CreditCard aCard)
        {
            cards.Add(aCard);
        }

        public static void WriteToStream(Person toWrite, Stream where)
        {
            BinaryWriter writer = new BinaryWriter(where);
            writer.Write(toWrite.Name);
            writer.Write(toWrite.BirthDate.Ticks);
            List<CreditCard> cards = toWrite.Cards.ToList();
            writer.Write(cards.Count);
            foreach (var next in cards)
            {
                CreditCard.WriteToStream(next, writer);
            }
        }

        public static Person ReadFromStream(Stream from)
        {
            BinaryReader reader = new BinaryReader(from);
            string name = reader.ReadString();
            long ticks = reader.ReadInt64();
            DateTime birthDate = new DateTime(ticks);
            int cardsCount = reader.ReadInt32();
            List<CreditCard> cards = new List<CreditCard>();
            for (int i = 0; i < cardsCount; ++i)
            {
                cards.Add(CreditCard.ReadFromStream(reader));
            }
            return new Person(name, birthDate, cards);
        }


    }

    [Serializable]
    public class CreditCard
    {
        public string Number { get; private set; }
        public DateTime ExpirationDate { get; private set; }

        public CreditCard(string cardNumber, DateTime expirationDate)
        {
            Number = cardNumber;
            ExpirationDate = expirationDate;
        }

        public static void WriteToStream(CreditCard toWrite, Stream where)
        {
            BinaryWriter writer = new BinaryWriter(where);
            WriteToStream(toWrite, writer);
        }

        public static void WriteToStream(CreditCard toWrite, BinaryWriter writer)
        {
            writer.Write(toWrite.Number);
            writer.Write(toWrite.ExpirationDate.Ticks);
        }

        public static CreditCard ReadFromStream(Stream from)
        {
            BinaryReader reader = new BinaryReader(from);
            return ReadFromStream(reader);
        }

        public static CreditCard ReadFromStream(BinaryReader reader)
        {
            string number = reader.ReadString();
            long ticks = reader.ReadInt64();
            DateTime expDate = new DateTime(ticks);
            return new CreditCard(number, expDate);
        }
        
    }
}
