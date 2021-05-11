using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
        [TestTraits(Trait.Dates)]
        public void BetweenDates()
        {
            var dates = DateRange.BetweenDates(StartDate, EndDate);
            Assert.IsTrue(
                dates
                    .SequenceEqual(ExpectedDates));
            
        }
        
        [TestMethod]
        [TestTraits(Trait.Numbers)]
        public void BetweenInts()
        {
            List<int> list = new() { 1, 2, 3, 4, 5 };

            var items = list.BetweenElements(2, 4).ToArray();

            int[] expected = { 2, 3, 4 };
            
            Assert.IsTrue(
                items.
                    SequenceEqual(expected));
            
        }

        [TestMethod]
        [TestTraits(Trait.Contacts)]
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

            var contactsBetweenTwo = contactsArray[startIndex..endIndex];
            
            Assert.IsTrue(
                contactsBetweenTwo
                    .SequenceEqual(
                        ExpectedContacts, 
                            new ContactIdFirstNameLastNameEqualityComparer()));

        }

        [TestMethod]
        [TestTraits(Trait.Contacts)]
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

            var (startIndex, endIndex) = contacts.BetweenContacts(firstContact, lastContact);
            
            var citiesBetweenTwoCities = contactsArray[startIndex..endIndex];
            
            Assert.IsTrue(
                citiesBetweenTwoCities
                    .SequenceEqual(
                        ExpectedContacts, 
                            new ContactIdFirstNameLastNameEqualityComparer()));

        }
        #region Helpers

        /// <summary>
        /// Creates a visual for indices for Oregon Cities which can be found under the [Dump] folder
        /// </summary>
        [TestMethod]
        [Ignore]
        [TestTraits(Trait.CreateTextFile)]
        public void CreateCities()
        {
            string[] oregonCities = FileOperations.OregonCities();

            var cities = oregonCities.CityListIndices();

            var sb = new StringBuilder();

            sb.AppendLine(" Id                      Name     Start   End");

            /*
             * Seed for City Id property as there are no id's in the text file
             */
            var id = 1;

            foreach (var city in cities)
            {
                city.Id = id;
                sb.AppendLine($"{city.Id,4}{city.Name,25},{city.StartIndex,5}{city.EndIndex,11}");

                id++;

            }

            File.WriteAllText("cities.txt", sb.ToString());
        }

        #endregion

        /// <summary>
        /// Hard-wired for Aloha to Ashland were in this case if the city names
        /// change, removed or added this will fail.
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Cities)]
        public void BetweenCities_1()
        {

            string[] oregonCities = FileOperations.OregonCities();
            List<string> citiesBetweenTwoCities = new List<string>();

            for (int index = 4; index < 13; index++)
            {
                citiesBetweenTwoCities.Add(oregonCities[index]);
            }

            Assert.IsTrue(
                citiesBetweenTwoCities
                    .SequenceEqual(ExpectedCities));

        }
        /// <summary>
        /// A variation of <see cref="BetweenCities_1"/>
        ///
        /// - Assert 1, perfect match
        /// - Assert 2, case of first city
        /// - Assert 3, [ExpectedException(typeof(NullReferenceException)
        ///             Here we tell the test engine an exception is expected
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Cities)]
        [ExpectedException(typeof(NullReferenceException), "yep, unsafe")]
        public void BetweenCities_2_UnSafe()
        {
            string startCity = "Aloha";
            string endCity = "Ashland";

            string[] oregonCities = FileOperations.OregonCities();

            var cities = oregonCities.CityListIndices();

            var (startIndex, endIndex) = cities.BetweenCities(startCity, endCity);

            var citiesBetweenTwoCities = oregonCities[startIndex..endIndex];

            Assert.IsTrue(
                citiesBetweenTwoCities
                    .SequenceEqual(ExpectedCities));

            
            
            startCity = "aloha";
            (startIndex, endIndex) = cities.BetweenCities(startCity, endCity);
            
            Assert.IsTrue(startIndex.Value >0 && endIndex.Value >0);

            /*
             * Given a non-existing city name expect an exception
             * as per [ExpectedException]
             */
            startCity = "aloh";
            (_, _) = cities.BetweenCities(startCity, endCity);

        }
        /// <summary>
        /// A variation of <see cref="BetweenCities_2_UnSafe"/>
        /// In this case assertion is used in the language extension along with an extra element
        /// of the tuple to indicate if both start and end city exists.
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Cities)]
        public void BetweenCities_2_Safe()
        {
            string startCity = "Aloha";
            string endCity = "Ashland";

            string[] oregonCities = FileOperations.OregonCities();

            var cities = oregonCities.CityListIndices();
            
            var (startIndex, endIndex, failed) = cities.BetweenCitiesSafe(startCity, endCity);

            Assert.IsFalse(failed);

            var citiesBetweenTwoCities = oregonCities[startIndex..endIndex];

            Assert.IsTrue(
                citiesBetweenTwoCities
                    .SequenceEqual(ExpectedCities));

        }
        [TestMethod]
        [TestTraits(Trait.Cities)]
        public void BetweenCities_3()
        {
            string[] oregonCities = FileOperations.OregonCities();

            var cities = oregonCities.CityListIndices();
            var citiesBetweenTwoCitiesHardWired = cities.ToArray()[4..^360];
            
            Assert.IsTrue(
                citiesBetweenTwoCitiesHardWired.Select(city => city.Name)
                    .SequenceEqual(ExpectedCities));
        }

        [TestMethod]
        [TestTraits(Trait.Strings)]
        public void StringArray_StartFrom_Index()
        {
            string[] oregonCities = FileOperations.OregonCitiesFirstTen();
            Range range = Range.StartAt(5);
            var fiveCities = oregonCities[range];

            Assert.IsTrue(fiveCities.SequenceEqual(FiveCitiesFromIndex));   
        }

        [TestMethod]
        [TestTraits(Trait.Strings)]
        public void StringArray_StartFrom_Beginning()
        {
            Range range = Range.EndAt(5);
            string[] oregonCities = FileOperations.OregonCitiesFirstTen();
            var fiveCities = oregonCities[range];

            Assert.IsTrue(
                fiveCities
                    .SequenceEqual(FiveCitiesFromBeginning));

        }


        [TestMethod]
        [TestTraits(Trait.RangesIndices)]
        public void BetweenPeriod()
        {

            var result = Periods.BetweenElements("2010 FYA", "3Q 2014A");
            
            Assert.IsTrue(
                result
                    .SequenceEqual(ExpectedPeriod));
        }

    }
}
