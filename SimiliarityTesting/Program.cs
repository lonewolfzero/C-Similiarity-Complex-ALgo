using F23.StringSimilarity;
using SimiliarityStringTestingConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPTestingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Damerau damerauTest = new Damerau();
                //double SimiliarityAccuracy = damerauTest.Distance("Color", "Colour");
                //Console.WriteLine("Result Accuracy gan: " + SimiliarityAccuracy+"%");

                //F23.StringSimilarity.Levenshtein levenshteinTest = new F23.StringSimilarity.Levenshtein();
                //double SimiliarityAccuracy2= levenshteinTest.Distance("Color", "Colour");
                //Console.WriteLine("Result Accuracy gan: " + SimiliarityAccuracy2 + "%");

                //var weightLevenshtein = new WeightedLevenshtein(new ExampleCharSub());
                //double SimiliarityAccuracy3 = weightLevenshtein.Distance("Color", "Colour");
                //Console.WriteLine("Result Accuracy gan: " + SimiliarityAccuracy3 + "%");

                var ngram = new NGram(100);
                double SimiliarityAccuracy5 = ngram.Distance("Color", "Colour") * 100;
                Console.WriteLine("Result Accuracy gan: " + SimiliarityAccuracy5 + "%");

                var ngram2 = new NGram(100);
                double SimiliarityAccuracy6 = ngram2.Distance("firman", "talon pt") * 100;
                Console.WriteLine("Result Accuracy gan: " + SimiliarityAccuracy6 + "%");

                var jw = new JaroWinkler();

                double SimiliarityAccuracy7 = jw.Similarity("firman", "talon pt") * 100;
                Console.WriteLine("Result Accuracy gan: " + SimiliarityAccuracy7 + "%");

                
                double SimiliarityAccuracy8 = jw.Similarity("hendi", "firman hendi") * 100;
                Console.WriteLine("Result Accuracy gan: " + SimiliarityAccuracy8 + "%");

                double SimiliarityAccuracy10 = jw.Similarity("firman hendi setiawan", "setiawan hendi firman udin") * 100;
                Console.WriteLine("Result Accuracy gan: " + SimiliarityAccuracy10 + "%");

                double SimiliarityAccuracy9 = jw.Similarity("budi setiawan", "setiawan budi") * 100;
                Console.WriteLine("Result Accuracy gan: " + SimiliarityAccuracy9 + "%");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }


        private class ExampleCharSub : ICharacterSubstitution
        {
            public double Cost(char c1, char c2)
            {
                // The cost for substituting 't' and 'r' is considered smaller as these 2 are located next to each other on a keyboard
                if (c1 == 't' && c2 == 'r') return 0.5;

                // For most cases, the cost of substituting 2 characters is 1.0
                return 1.0;
            }
        }
    }
}
