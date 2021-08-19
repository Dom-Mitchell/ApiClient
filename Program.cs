using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace ApiClient
{
    class Joke
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("setup")]
        public string SetUp { get; set; }

        [JsonPropertyName("punchline")]
        public string PunchLine { get; set; }

        class Program
        {
            static async Task GetARandomJokeAsync()
            {
                var client = new HttpClient();
                var url = "https://official-joke-api.appspot.com/jokes/random";
                var responseAsStream = await client.GetStreamAsync(url);
                var joke = await JsonSerializer.DeserializeAsync<Joke>(responseAsStream);
                Console.WriteLine($"\n\n{joke.SetUp} ~ ~ ~ {joke.PunchLine}");
            }

            static async Task GetTenRandomJokes()
            {
                var client = new HttpClient();
                var url = "https://official-joke-api.appspot.com/jokes/ten";
                var responseAsStream = await client.GetStreamAsync(url);
                var jokes = await JsonSerializer.DeserializeAsync<List<Joke>>(responseAsStream);
                Console.WriteLine();
                foreach (var joke in jokes)
                {
                    Console.WriteLine($"{joke.SetUp} ~ ~ ~ {joke.PunchLine}\n");
                }
            }

            static char PressAnyKey(string prompt)
            {
                Console.WriteLine(prompt);
                var keyPressed = Console.ReadKey().KeyChar;
                return keyPressed;
            }

            static string Menu()
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("(R)andom Joke");
                Console.WriteLine("(T)en Random Jokes");
                Console.Write("(Q)uit\n: ");
                var choice = Console.ReadLine().ToUpper();
                Console.Clear();
                return choice;
            }

            static async Task Main(string[] args)
            {
                var keepTheJokesComing = true;
                while (keepTheJokesComing)
                {
                    Console.Clear();
                    var menuSelection = Menu();
                    switch (menuSelection)
                    {
                        case "R":
                            await GetARandomJokeAsync();
                            PressAnyKey("\nPress Any Key to Continue! ");
                            Console.Clear();
                            break;
                        case "T":
                            await GetTenRandomJokes();
                            PressAnyKey("\nPress Any Key to Continue! ");
                            Console.Clear();
                            break;
                        case "Q":
                            Console.Clear();
                            keepTheJokesComing = false;
                            break;
                    }
                }
            }
        }
    }
}