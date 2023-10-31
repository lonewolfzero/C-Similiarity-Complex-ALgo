using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Renci.SshNet;
using System.Text.RegularExpressions;
using F23.StringSimilarity.Interfaces;

namespace SimiliarityStringTestingConsole
{


    public class Levenshtein : IMetricStringDistance
    {

        public double Distance(string s1, string s2)
        {
            return Distance(s1, s2, int.MaxValue);
        }

        public double Distance(string s1, string s2, int limit)
        {
            if (s1 == null)
            {
                throw new ArgumentNullException(nameof(s1));
            }

            if (s2 == null)
            {
                throw new ArgumentNullException(nameof(s2));
            }

            if (s1.Equals(s2))
            {
                return 0;
            }

            if (s1.Length == 0)
            {
                return s2.Length;
            }

            if (s2.Length == 0)
            {
                return s1.Length;
            }

            // create two work vectors of integer distances
            int[] v0 = new int[s2.Length + 1];
            int[] v1 = new int[s2.Length + 1];
            int[] vtemp;

            // initialize v0 (the previous row of distances)
            // this row is A[0][i]: edit distance for an empty s
            // the distance is just the number of characters to delete from t
            for (int i = 0; i < v0.Length; i++)
            {
                v0[i] = i;
            }

            for (int i = 0; i < s1.Length; i++)
            {
                // calculate v1 (current row distances) from the previous row v0
                // first element of v1 is A[i+1][0]
                //   edit distance is delete (i+1) chars from s to match empty t
                v1[0] = i + 1;

                int minv1 = v1[0];

                // use formula to fill in the rest of the row
                for (int j = 0; j < s2.Length; j++)
                {
                    int cost = 1;
                    if (s1[i] == s2[j])
                    {
                        cost = 0;
                    }
                    v1[j + 1] = Math.Min(
                            v1[j] + 1,              // Cost of insertion
                            Math.Min(
                                    v0[j + 1] + 1,  // Cost of remove
                                    v0[j] + cost)); // Cost of substitution

                    minv1 = Math.Min(minv1, v1[j + 1]);
                }

                if (minv1 >= limit)
                {
                    return limit;
                }

                // copy v1 (current row) to v0 (previous row) for next iteration
                // System.arraycopy(v1, 0, v0, 0, v0.length);

                // Flip references to current and previous row
                vtemp = v0;
                v0 = v1;
                v1 = vtemp;
            }

            return v0[s2.Length];
        }

    }
}
