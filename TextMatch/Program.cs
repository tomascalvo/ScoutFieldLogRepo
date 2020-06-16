using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScoutFieldLog_GroupProject.Models;

namespace TextMatch
{
    class Program
    {
        static string replacePunctuation(string inputString)
        {
            var sb = new StringBuilder();
            
            foreach (char c in inputString) {
                if (!char.IsPunctuation(c)) {
                    sb.Append(c);
                } else {
                    sb.Append(" ");// special chars are replaced w space
                }
            }
            return sb.ToString();
        }
        static IEnumerable<string> getKeywords(string twolineSummary)
        {
            twolineSummary = replacePunctuation(twolineSummary);         
            string[] words = twolineSummary.Split(" ");
            List<string> wordList = words.ToList<string>();

            wordList = wordList.ConvertAll(d => d.ToLower().Trim() );
            
            return wordList.Except( excludedKeywords ); ;
        }

        static IEnumerable<string> getSimilarCompanies(string companyName)
        {
            string newStartupString;
            if (!existingCompanies.TryGetValue(companyName, out newStartupString)) {
                return null;
            }

            IEnumerable<string> newStartupKeywords = getKeywords(newStartupString);
            List<string> companiesThatMatch = new List<string>();

            foreach (KeyValuePair<string, string> kvp in existingCompanies)
            {
                string existingCompanyName = kvp.Key;
                if (existingCompanyName.Equals(companyName))
                    continue;
                string twoLineString = kvp.Value;
                IEnumerable<string> companyKeywords = getKeywords(twoLineString);
                IEnumerable<string> matches =
                    companyKeywords.AsQueryable().Intersect(newStartupKeywords);
                int matchCount = matches.Count();
                if (matchCount > 0)
                    Console.Write("\nCompany " + existingCompanyName +
                    " has " + matches.Count() + " keyword matches: ");
                foreach (string m in matches)
                {
                    Console.Write(m + ", ");
                }
                companiesThatMatch.Add(existingCompanyName);
            }
            return companiesThatMatch;
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

        static void Main(string[] args)
        {
            IEnumerable<string> matches = new List<string>();

            foreach (KeyValuePair<string, string> kvp in existingCompanies)
            {
                string existingCompanyName = kvp.Key;
                Console.WriteLine("\n\n-----------------------------------------");
                Console.WriteLine("Checking company: " + existingCompanyName);
                matches = getSimilarCompanies(existingCompanyName);
                if (matches == null || matches.Count() == 0)
                {
                    Console.WriteLine("no matches found");
                }
            }
        }// end main

        private static Dictionary<string, string> existingCompanies = 
            new Dictionary<string, string>();

        static Program()
        {
            existingCompanies.Add("Startup 1", "Video monitoring AI that gives rooms a voice.");
            existingCompanies.Add("Startup 2", "Analytics to measure Organizational Health and analyses Employee-Engagement, Team-Productivity, and Organizational-Adaptability.");
            existingCompanies.Add("Startup 3", "Wearables that reduces injuries for the industrial workforce");
            existingCompanies.Add("Startup 4", "IoT smart label technology, long range connectivity, for autonomous end-to-end traceability in your supply chain.");
            existingCompanies.Add("Startup 5", "Exoskeleton to help workers with lifting.");
            existingCompanies.Add("Startup 6", "Small sensor to detect allergens in food");
            existingCompanies.Add("Startup 7", " booking, tracking and billing for Medicaid transportation programs using rideshare technology.");
            existingCompanies.Add("Startup 8", "Data management platform for microbiome scientists");
            existingCompanies.Add("Startup 9", "Clinical decision support tool to help maintain guidelines at the point of care. ");
            existingCompanies.Add("Startup 10", " mood-enhancing ice cream");
            existingCompanies.Add("Startup 11", "Digital health for pets");
            existingCompanies.Add("Startup 12", "Health focused, subscription,  education experience for children");
            existingCompanies.Add("Startup 13", "Electrolytic ozone an eco-friendly and sustainable sanitizing solution");
            existingCompanies.Add("Startup 14", "linen and laundry solution for short-term rental hosts");
            existingCompanies.Add("Startup 15", "industry 4.0 'factory of the future'. Visibility and actionable insights quickly using a range of  accurate sensors");
            existingCompanies.Add("Startup 16", "Affordable housing and innovation community for minority owned ventures. ");
            existingCompanies.Add("Startup 17", "View everything you are seeing on TV using this app (purchase clothes your favorite actor is wearing)");
            existingCompanies.Add("Startup 18", "intelligent process automation platform for the enterprise. improves knowledge workers productivity optimizes business outcomes");
            existingCompanies.Add("Startup 19", "AI for efficient and cheap electricity usage");
            existingCompanies.Add("Startup 20", "Coordinates every element of every delivery and fulfillment, ensuring orders are efficiently fulfilled at massive scale. ");
            existingCompanies.Add("Startup 21", "Community engagement platform that brings people together for unique discussions");
            existingCompanies.Add("Startup 22", "brainwaves and heart rate stress monitoring and facial UX controls");
            existingCompanies.Add("Startup 23", "Helping retailers meet demands for quick fulfillment at scale. ");
            existingCompanies.Add("Startup 24", "Inexpensive robot improving worker safety and efficiency. ");
            existingCompanies.Add("Startup 25", "freight comparison, booking, and management");
            existingCompanies.Add("Startup 26", "Measuring blood loss without lab work. ");
            existingCompanies.Add("Startup 27", "Coordinated care for kids with chronic health issues.");
            existingCompanies.Add("Startup 28", " sound-based solutions for locating and analyzing  faults and failures ");
            existingCompanies.Add("Startup 29", "improving healthcare defect-free care");
            existingCompanies.Add("Startup 30", " non-invasive self-care product for personalized care using predictive analytics");
            existingCompanies.Add("Startup 31", "helping pay unemployed people to perform public benefit work.");
            existingCompanies.Add("Startup 32", "facial recognition for a fast, safe, and a reliable way to identify patients. ");
            existingCompanies.Add("Startup 33", "sensing solution measuring a wide range of physiological and behavioral markers and analyzing critical health-related parameters during sleep ");
            existingCompanies.Add("Startup 34", "Electrolysis systems for sanitation and disinfection");
            existingCompanies.Add("Startup 35", "Single layer graphene oxide fiber");
            existingCompanies.Add("Startup 36", "Pillow that induces and facilities quality sleep");
            existingCompanies.Add("Startup 37", "Reusing water from previous wash cycles");
            existingCompanies.Add("Startup 38", "Connects organizations with fans and followers across the globe using a live video  platform.");
            existingCompanies.Add("Startup 39", "Predictive AI algorithm technology for logistics management, optimization, and automation");
            existingCompanies.Add("Startup 40", "user-centric mobile health solution.");
            existingCompanies.Add("Startup 41", "Contact-free and early detection solution for patients");
            existingCompanies.Add("Startup 42", "In-app balance training");
            existingCompanies.Add("Startup 43", "Emotional processing unit");
            existingCompanies.Add("Startup 44", "energy harvesting sensor chip that can communicate to anything with Bluetooth");
            existingCompanies.Add("Startup 45", "Antimicrobial coatings");
            existingCompanies.Add("Startup 46", "sensory human interface technology company creating cognitive computing applications to determine intoxication, fatigue, and cognitive load using cameras and computer vision.");
            existingCompanies.Add("Startup 47", "Autonomous microtransit service ");
            existingCompanies.Add("Startup 48", "high-strength silk fibers");
            existingCompanies.Add("Startup 49", "Detect cognitive decline over time");
            existingCompanies.Add("Startup 50", "intelligent medical scribe");
            existingCompanies.Add("Startup 51", "Software to turn any engineer into a AI expert");
            existingCompanies.Add("Startup 52", "Digital therapy platform");
            existingCompanies.Add("Startup 53", "Personalized meal recipes and shopping lists");
            existingCompanies.Add("Startup 54", "personal digestive tracker");
            existingCompanies.Add("Startup 55", "Remote, contactless Health and Wellness monitoring");
            existingCompanies.Add("Startup 56", "Smart interfaces out of surfaces of any shape size and material");
            existingCompanies.Add("Startup 57", "Preventive healthcare through genomics");
            existingCompanies.Add("Startup 58", "brain health wearable");
            existingCompanies.Add("Startup 59", "whole room disinfection using hydrogen peroxide disinfectant formula i");
            existingCompanies.Add("Startup 60", "Sensors for all sorts of scents");
            existingCompanies.Add("Startup 61", "smartphone sperm test, captures a sperm video and reports moving sperm count with >97% accuracy, in private");
            existingCompanies.Add("Startup 62", "Material that becomes cooler with increased sun exposure");
            existingCompanies.Add("Startup 63", "Smart healthcare");
            existingCompanies.Add("Startup 64", "Mini sensor packages");
            existingCompanies.Add("Startup 65", "last mile delivery robot");
            existingCompanies.Add("Startup 66", " cognitive state monitoring using eye tracking");
            existingCompanies.Add("Startup 67", "Predicts the emotional reaction to a new product");
            existingCompanies.Add("Startup 68", "Smart Remote for smart homes");
            existingCompanies.Add("Startup 69", "digital fitness and health platform");
            existingCompanies.Add("Startup 70", "VR for education, rehabilitation, healthy aging, and more");
            existingCompanies.Add("Startup 71", "Technology that generates electricity from temperature change");
            existingCompanies.Add("Startup 72", "Sensors to map and uncover hidden data in your breath");
            existingCompanies.Add("Startup 73", "Light-harvesting solar cells ");
            existingCompanies.Add("Startup 74", "A sustainable process for production of activated carbon");
            existingCompanies.Add("Startup 75", "AI platform that uses voice analysis to evaluate health status");
            existingCompanies.Add("Startup 76", "sounds/music that help people focus and relax.");
        }


    }// end class
}// end namespace
