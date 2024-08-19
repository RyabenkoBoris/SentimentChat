using Azure;
using Azure.AI.TextAnalytics;
using Chat.Interfaces;

namespace Chat
{
    public class SentimentAnalysis
    {
        static string languageKey = Environment.GetEnvironmentVariable("LANGUAGE_KEY");
        static string languageEndpoint = Environment.GetEnvironmentVariable("LANGUAGE_ENDPOINT");

        private static readonly AzureKeyCredential credentials = new AzureKeyCredential(languageKey);
        private static readonly Uri endpoint = new Uri(languageEndpoint);
        private static readonly TextAnalyticsClient client = new TextAnalyticsClient(endpoint, credentials);

        public static async Task<string> SentimentAnalysMessage(string message)
        {
            var review = await client.AnalyzeSentimentAsync(message);

            var neutral = review.Value.ConfidenceScores.Neutral;
            var positive = review.Value.ConfidenceScores.Positive;
            var negative = review.Value.ConfidenceScores.Negative;

            if (neutral > positive && neutral > negative) return "Neutral";
            else if (positive > negative) return "Positive";
            else return "Negative";
        }
    }
}
