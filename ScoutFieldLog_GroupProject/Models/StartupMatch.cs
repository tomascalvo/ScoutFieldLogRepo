using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScoutFieldLog_GroupProject.Models
{
    public class StartupMatch
    {
        public static string replacePunctuation(string inputString)
        {
            var sb = new StringBuilder();

            foreach (char c in inputString)
            {
                if (!char.IsPunctuation(c))
                {
                    sb.Append(c);
                }
                else
                {
                    sb.Append(" ");// special chars are replaced w space
                }
            }
            return sb.ToString();
        }

        public static string getKeywordString(string twolineSummary)
        {
            if (twolineSummary == null)
            {
                return null;
            }
            IEnumerable<string> kws = getKeywords(twolineSummary);
            return convertListToString(kws, ", ");
        }

        public static string convertListToString(IEnumerable<string> kws, string separator)
        {
            StringBuilder sb = new StringBuilder();
            String[] kwsArray = kws.ToArray();

            if (kwsArray.Length >= 1)
            {
                sb.Append( kwsArray[0] );
            }
            for (int i=1; i < kwsArray.Length; i++)
            {
                sb.Append(separator);
                sb.Append(kwsArray[i]);
            }
            return sb.ToString();
        }

        //1. before you save the lead to db, call this method to get the keywords from the 2 line summary and save them, to a keywords column(still has to be created).
        public static IEnumerable<string> getKeywords(string twolineSummary)
        {
            if (null == twolineSummary || "".Equals(twolineSummary))
            {
                return null;
            }
            twolineSummary = replacePunctuation(twolineSummary);
            string[] words = twolineSummary.Split(" ");
            List<string> wordList = words.ToList<string>();

            wordList = wordList.ConvertAll(d => d.Trim());

            return wordList.Except(excludedKeywords); ;
        }

        public static List<string> getCompanyKeywords(string companyName)
        {
            string companyKeywordString = null;

            for (int i = 0; i < existingCompanies.Count(); i++)
            {
                if (existingCompanies[i].CompanyName == companyName)
                {
                    companyKeywordString = existingCompanies[i].Keywords;
                }
            }
            if (companyKeywordString == null) return null;

            string[] keywords = companyKeywordString.Split(" ");
            return keywords.ToList();
        }

        public static List<string> convertKeywordsToUpperList(string keywordString)
        {
            if (keywordString == null) return null;

            string[] keywords = keywordString.Split(",");
            List<string> wordList = keywords.ToList<string>();
            wordList = wordList.ConvertAll(d => d.ToUpper().Trim());
            return wordList;
        }

        public void refreshCompanyCache()
        {
            List<StartUpCompanies> sc = _dbContext.StartUpCompanies.ToList();

            if (sc != null && sc.Count > 0)
            {
                existingCompanies = sc;
            }
        }

        public void refreshKeywords()
        {
            List<StartUpCompanies> sc = _dbContext.StartUpCompanies.ToList();
            foreach (StartUpCompanies s in sc)
            {
                if (s.TwoLineSummary == null)
                {
                    continue;
                }
                s.Keywords = getKeywordString(s.TwoLineSummary);
                _dbContext.Update(s);
                _dbContext.SaveChanges();
            }
        }

        public List<RecommendedAlignment> getSimilarCompanies(int companyId, string keywordString)
        {
            List<string> companyKeywords = convertKeywordsToUpperList(keywordString);

            if (companyKeywords == null || companyKeywords.Count == 0)
            {
                return null;
            }

            List<RecommendedAlignment> lra = new List<RecommendedAlignment>();

            for (int i = 0; i < existingCompanies.Count(); i++)
            {
                if (existingCompanies[i].Id != companyId)
                {
                    List<string> otherCompanyKeywords =
                        convertKeywordsToUpperList(existingCompanies[i].Keywords);
                    if (otherCompanyKeywords == null)
                    {
                        continue;
                    }
                    IEnumerable<string> matches =
    companyKeywords.AsQueryable().Intersect(otherCompanyKeywords);

                    int matchCount = matches.Count();
                    if (matchCount > 0)
                    {
                        string matchedWords = convertListToString(matches, ",");

                        RecommendedAlignment ra = new RecommendedAlignment();
                        ra.Id = existingCompanies[i].Id.ToString();
                        ra.CompanyName = existingCompanies[i].CompanyName;
                        ra.KeywordsMatched = matchedWords;
                        ra.Alignments = existingCompanies[i].Alignments;
                        ra.TechnologyAreas = existingCompanies[i].TechnologyAreas;

                        foreach (string s in matches)
                        {
                            existingCompanies[i].TwoLineSummary =
                                Regex.Replace(existingCompanies[i].TwoLineSummary, s, "*" + s+ "*", RegexOptions.IgnoreCase);
                        }
                        
                        ra.TwoLineSummary = existingCompanies[i].TwoLineSummary;
                        lra.Add(ra);
                    }
                }
            }

            return lra;
        }

        private static readonly List<string> excludedKeywords =
            new List<string> { null, "",
                "a", "an", "and", "any", "are", "at",
                "for", "from",
                "help",
                "i", "in", "into", "is",
                "of", "on",
                "the", "that", "to",
                "using",
                "with",
                "your",
            };

        private static List<StartUpCompanies> existingCompanies = new List<StartUpCompanies>();

        private static StartupMatchDbContext _dbContext;

        public StartupMatch(StartupMatchDbContext dbContext)
        {
            _dbContext = dbContext;
            refreshCompanyCache();
        }

    }// end StartupMatch class

    public class RecommendedAlignment
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string TechnologyAreas { get; set; }
        public string TwoLineSummary { get; set; }
        public string KeywordsMatched { get; set; }
        public string Alignments { get; set; }
    }
}