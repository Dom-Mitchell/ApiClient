using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Pastel;
using System.Drawing;

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

            static async Task GetGeneralJoke()
            {
                var client = new HttpClient();
                var url = "https://official-joke-api.appspot.com/jokes/general/random";
                var responseAsStream = await client.GetStreamAsync(url);
                var joke = await JsonSerializer.DeserializeAsync<List<Joke>>(responseAsStream);
                Console.WriteLine($"\n{joke[0].SetUp} ~ ~ ~ {joke[0].PunchLine}");
            }

            static async Task GetProgrammingJoke()
            {
                var client = new HttpClient();
                var url = "https://official-joke-api.appspot.com/jokes/programming/random";
                var responseAsStream = await client.GetStreamAsync(url);
                var joke = await JsonSerializer.DeserializeAsync<List<Joke>>(responseAsStream);
                Console.WriteLine();
                Console.WriteLine($"\n{joke[0].SetUp} ~ ~ ~ {joke[0].PunchLine}");
            }

            static async Task GetKnock_KnockJoke()
            {
                var client = new HttpClient();
                var url = "https://official-joke-api.appspot.com/jokes/knock-knock/random";
                var responseAsStream = await client.GetStreamAsync(url);
                var joke = await JsonSerializer.DeserializeAsync<List<Joke>>(responseAsStream);
                Console.WriteLine($"\n{joke[0].SetUp} ~ ~ ~ {joke[0].PunchLine}");
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
                Console.WriteLine("(J)oke Type");
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
                        case "J":
                            {

                                var jokeType = true;
                                while (jokeType)
                                {
                                    Console.Clear();
                                    Console.Write("What type of joke would you like to hear?\n(G)eneral\n(P)rogramming\n(K)nock-knock\n: ");
                                    var choice = Console.ReadLine().ToUpper();
                                    switch (choice)
                                    {
                                        case "G":
                                            Console.Clear();
                                            await GetGeneralJoke();
                                            PressAnyKey("\nPress Any Key to Continue! ");
                                            Console.Clear();
                                            jokeType = false;
                                            break;
                                        case "P":
                                            Console.Clear();
                                            await GetProgrammingJoke();
                                            PressAnyKey("\nPress Any Key to Continue! ");
                                            Console.Clear();
                                            jokeType = false;
                                            break;
                                        case "K":
                                            Console.Clear();
                                            await GetKnock_KnockJoke();
                                            PressAnyKey("\nPress Any Key to Continue! ");
                                            Console.Clear();
                                            jokeType = false;
                                            break;
                                        default:
                                            Console.WriteLine($"\n{"Your answer was invalid. Please try again!".Pastel(Color.Red)}");
                                            PressAnyKey("\nPress Any Key to Continue! ");
                                            Console.Clear();
                                            break;
                                    }
                                }
                                break;
                            }
                        case "Q":
                            Console.Clear();
                            keepTheJokesComing = false;
                            break;
                        default:
                            Console.WriteLine($"{"Your answer was invalid. Please try again!".Pastel(Color.Red)}");
                            PressAnyKey("\nPress Any Key to Continue! ");
                            Console.Clear();
                            break;
                    }
                }
            }
        }
    }
}