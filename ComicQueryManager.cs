using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace Jimmys_Comics_ch_14_
{
    class ComicQueryManager
    {
        public ObservableCollection<ComicQuery> AvailableQueries { get; private set; }
        public ObservableCollection<object> CurrentQueryResults { get; private set; }
        public string Title { get; set; }

        public ComicQueryManager()
        {
            UpdateAvailableQueries();
            CurrentQueryResults = new ObservableCollection<object>();
        }

        private void UpdateAvailableQueries()
        {
            AvailableQueries = new ObservableCollection<ComicQuery>()
            {
                new ComicQuery("LINQ makes queries easy", "A sample query", "Let's show Jimmy how flexible LINQ is",
                                    CreateImageFromAssets("purple_250x250.jpg")), //File Name
                new ComicQuery("Expensive comics", "Comics over $500", "Comics whose value is over 500 bucks."+" Jimmy can use this to figure out which comics are most coveted",
                                    CreateImageFromAssets("captain_amazing_250x250.jpg")), //File Name
                new ComicQuery("LINQ is versatile 1", "Modify every item returned from query", "This code will add a string onto the end of each string in an array",
                                    CreateImageFromAssets("bluegray_amazing_250x250.jpg")), //File Name\
                new ComicQuery("LINQ is versatile 2", "Perform calculations", "LINQ provides extension methods",
                                    CreateImageFromAssets("purple_250x250.jpg")), //File Name
                new ComicQuery("LINQ is versatile 3", "Store all part of your results in a new sequence", "Sometimes you'll want to keep your results from a LINQ query around",
                                    CreateImageFromAssets("bluegray_250x250.jpg")), //File Name
                new ComicQuery("Join purchases with prices", "Let's see if Jimmy drives a hard bargain", "This query creates a list of Purchase classes that contain"+
                                    " Jimmy's purchases and compares them with with the prices he found on Greg's List", CreateImageFromAssets("capitan_amazing_250x250.jpg")), //File Name
                new ComicQuery("Group comics by price range", "Combine Jimmy's values into groups", "Groups of cheap, midrange and expensive comics",
                                    CreateImageFromAssets("capitan_amazing_250x250.jpg")), //File Name
            };
        }

        private static BitmapImage CreateImageFromAssets(string imageFileName)
        {
            try
            {
                Uri uri = new Uri(imageFileName, UriKind.RelativeOrAbsolute);
                return new BitmapImage(uri);
            }
            catch
            {
                return new BitmapImage();
            }
        }

        public void UpdateQueryResults(ComicQuery query)
        {
            Title = query.Title;

            switch (query.Title)
            {
                case "LINQ makes queries easy": LinqMakesQueriesEasy(); break;
                case "Expensive comics": ExpensiveComics(); break;
                case "LINQ is versatile 1": LinqIsVersatile1(); break;
                case "LINQ is versatile 2": LinqIsVersatile2(); break;
                case "LINQ is versatile 3": LinqIsVersatile3(); break;
                case "Join purchases with prices": JoinPurchaseWithPrices(); break;
                case "Group comics by price range": CombineJimmysValuesIntoGroups(); break;
            }
        }

        public static IEnumerable<Comic> BuildCataloq()
        {
            return new List<Comic>
            {
                new Comic {Name="Johnny America vs. Pinco", Issue=6 },
                new Comic {Name="Rock", Issue=19},
                new Comic {Name="Woman's work", Issue=36 },
                new Comic {Name="Hippie", Issue=57 },
                new Comic {Name="Revenge", Issue=68 },
                new Comic {Name="Black monday", Issue=74 },
                new Comic {Name="Tribal tattoo madness", Issue=83 },
                new Comic {Name="The death of an object", Issue=97 },
            };
        }

        private static Dictionary<int, decimal> GetPrices()
        {
            return new Dictionary<int, decimal>
            {
                {6, 3600M }, {19, 500M }, {36, 650M }, {57, 13525M },
                {68, 250M }, {74, 75M }, {83, 25.75M }, {97, 35.25M }
            };
        }

        private void LinqMakesQueriesEasy()
        {
            int[] values = new int[] { 0, 12, 44, 36, 92, 54, 13, 8 };
            var results = from v in values where v < 37 orderby v select v;
            CurrentQueryResults.Clear();
            foreach (int i in results)
                CurrentQueryResults.Add(new { Title = i.ToString(),
                    Image =CreateImageFromAssets("purple_250x250.jpg") }); //File Name
        }

        private void ExpensiveComics()
        {
            IEnumerable<Comic> comics = BuildCataloq();
            Dictionary<int, decimal> values = GetPrices();

            var mostExpensive = from comic in comics where values[comic.Issue] > 500
                                orderby values[comic.Issue] select comic;
            CurrentQueryResults.Clear();
            foreach (Comic comic in mostExpensive)
                CurrentQueryResults.Add(new { Title=string.Format("{0} is worth {1:c}", comic.Name, values[comic.Issue]),
                    Image =CreateImageFromAssets("captain_amazing_250x250.jpg") });
        }

        private void LinqIsVersatile1()
        {
            string[] sandwiches = { "ham and cheese", "salami with mayo" };

            var sandwichesOnRye = from sandwich in sandwiches select sandwich + " on rye";

            CurrentQueryResults.Clear();
            foreach (var sandwich in sandwichesOnRye)
                CurrentQueryResults.Add(CreateAnonymousListViewItem(sandwich, "bluegray_250x250.jpg"));
        }

        private void LinqIsVersatile2()
        {
            Random random = new Random();
            List<int> listOfNumbers = new List<int>();
            int length = random.Next(50, 150);
            for (int i = 0; i < length; i++)
                listOfNumbers.Add(random.Next(100));

            CurrentQueryResults.Clear();
            CurrentQueryResults.Add(CreateAnonymousListViewItem(String.Format("There are {0} numbers ", listOfNumbers.Count())));
            CurrentQueryResults.Add(CreateAnonymousListViewItem(String.Format("The smallest is {0} ", listOfNumbers.Min())));
            CurrentQueryResults.Add(CreateAnonymousListViewItem(String.Format("The biggest is {0} ", listOfNumbers.Max())));
            CurrentQueryResults.Add(CreateAnonymousListViewItem(String.Format("The sum is {0} ", listOfNumbers.Sum())));
            CurrentQueryResults.Add(CreateAnonymousListViewItem(String.Format("The avarage is {0:F2} ", listOfNumbers.Average())));
        }

        private void LinqIsVersatile3()
        {
            List<int> listOfNumbers = new List<int>();
            for (int i = 1; i <= 1000; i++)
                listOfNumbers.Add(i);

            var under50sorted = from number in listOfNumbers where number < 50 orderby number descending select number;
            var firstFive = under50sorted.Take(6);

            List<int> shortList = firstFive.ToList();
            foreach (int n in shortList)
                CurrentQueryResults.Add(CreateAnonymousListViewItem(n.ToString(), "bluegray_250x250.jpg")); ;
        }

        private object CreateAnonymousListViewItem(string title, string imageFileName= "purple_250x250.jpg")
        {
            return new { Title = title, Image = CreateImageFromAssets(imageFileName), };
        }

        private void CombineJimmysValuesIntoGroups()
        {
            Dictionary<int, decimal> values = GetPrices();
            var priceGroups = from pair in values
                              group pair.Key by Purchase.EvaluatePrice(pair.Value)
                              into priceGroup
                              orderby priceGroup.Key descending
                              select priceGroup;

            foreach (var group in priceGroups)
            {
                string message = String.Format("I found {0} {1} comics: issues ", group.Count() , group.Key);
                foreach (var price in group)
                    message += price.ToString() + "; ";
                CurrentQueryResults.Add(CreateAnonymousListViewItem(message, "capitan_amazing_250x250.jpg"));
            }
        }

        private void JoinPurchaseWithPrices()
        {
            IEnumerable<Comic> comics = BuildCataloq();
            Dictionary<int, decimal> values = GetPrices();
            IEnumerable<Purchase> purchases = Purchase.FindPurchases();
            var results = from comic in comics
                          join purchase in purchases on comic.Issue equals purchase.Issue
                          orderby comic.Issue ascending
                          select 
                          new { Comic = comic, Price = purchase.Price, Title = comic.Name, Subtitle = "Issue #" + comic.Issue,
                              Description = "Bought for " + purchase.Price, Image = CreateImageFromAssets("capitan_amazing_250x250.jpg") };
            decimal gregsListValue = 0;
            decimal totalSpent = 0;
            foreach (var result in results)
            {
                gregsListValue += values[result.Comic.Issue];
                totalSpent += result.Price;
                CurrentQueryResults.Add(result);
            }

            Title = String.Format("I spent {0:c} on comics worth {1:c}", totalSpent, gregsListValue);
        }
    }
}
