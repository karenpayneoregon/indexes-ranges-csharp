using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RangeUnitTest.Base;
using RangeUnitTest.Classes;
using RangeUnitTest.Extensions;

namespace RangeUnitTest
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.RangesIndices)]
        public void BetweenDates()
        {
            var dates = DateRange.BetweenDates(StartDate, EndDate);
            Assert.IsTrue(dates.SequenceEqual(ExpectedDates));
            
        }
        [TestMethod]
        [TestTraits(Trait.RangesIndices)]
        public void BetweenInts()
        {
            List<int> list = new() { 1, 2, 3, 4, 5 };

            var items = list.BetweenElements(2, 4).ToArray();

            int[] expected = { 2, 3, 4 };
            
            Assert.IsTrue(items.SequenceEqual(expected));
            
        }

        [TestMethod]
        [TestTraits(Trait.RangesIndices)]
        public void BetweenContacts_1()
        {
            /*
             * First contact for between
             */
            var firstContact = new ContactName() { FirstName = "Frédérique", LastName = "Citeaux" };

            /*
             * Last contact for between
             */
            var lastContact = new ContactName() { FirstName = "Elizabeth", LastName = "Brown" };

            var contactsArray = MockedData.ReadContacts().ToArray();
            var contacts = contactsArray.ContactsListIndices();

            var (startIndex, endIndex) = contacts.BetweenContacts(firstContact, lastContact);

            var citiesBetweenTwoCities = contactsArray[startIndex..endIndex];
            
            Assert.IsTrue(citiesBetweenTwoCities.SequenceEqual(ExpectedContacts, 
                new ContactIdFirstNameLastNameEqualityComparer()));

        }

        [TestMethod]
        [TestTraits(Trait.RangesIndices)]
        public void BetweenContacts_2()
        {
            /*
             * First contact for between
             */
            var firstContact = new ContactName() { FirstName = "Frédérique", LastName = "Citeaux" };

            /*
             * Last contact for between
             */
            var lastContact = new ContactName() { FirstName = "Elizabeth", LastName = "Brown" };

            var contactsArray = MockedData.ReadContacts().ToArray();
            var contacts = contactsArray.ContactsListIndices();

            var range = contacts.BetweenContacts(firstContact, lastContact);

            var citiesBetweenTwoCities = contactsArray[range.startIndex..range.endIndex];
            
            Assert.IsTrue(citiesBetweenTwoCities.SequenceEqual(ExpectedContacts, 
                new ContactIdFirstNameLastNameEqualityComparer()));

        }

        [TestMethod]
        [TestTraits(Trait.RangesIndices)]
        public void BetweenCities()
        {
            string startCity = "Aloha";
            string endCity = "Ashland";

            string[] oregonCities = FileOperations.OregonCities();

            var cities = oregonCities.CityListIndices();
            var (startIndex1, endIndex1) = cities.BetweenCities(startCity, endCity);

            var citiesBetweenTwoCities = oregonCities[startIndex1..endIndex1];
            
            Assert.IsTrue(citiesBetweenTwoCities.SequenceEqual(ExpectedCities));

        }

        [TestMethod]
        [TestTraits(Trait.RangesIndices)]
        public void BetweenPeriod()
        {

            var result = Periods.BetweenElements("2010 FYA", "3Q 2014A");
            
            Assert.IsTrue(result.SequenceEqual(ExpectedPeriod));
        }

    }
}
