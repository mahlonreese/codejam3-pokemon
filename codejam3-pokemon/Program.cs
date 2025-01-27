﻿using PokeAPI;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Windows.UI.Xaml.Media.Imaging;
using System.Drawing;
using System.ComponentModel.Design;

class Program
{
    public static async Task Main()
    {


        Console.ForegroundColor = ConsoleColor.Yellow;
        string title = @"
                                  ,'\
    _.----.        ____         ,'  _\   ___    ___     ____
_,-'       `.     |    |  /`.   \,-'    |   \  /   |   |    \  |`.
\      __    \    '-.  | /   `.  ___    |    \/    |   '-.   \ |  |
 \.    \ \   |  __  |  |/    ,','_  `.  |          | __  |    \|  |
   \    \/   /,' _`.|      ,' / / / /   |          ,' _`.|     |  |
    \     ,-'/  /   \    ,'   | \/ / ,`.|         /  /   \  |     |
     \    \ |   \_/  |   `-.  \    `'  /|  |    ||   \_/  | |\    |
      \    \ \      /       `-.`.___,-' |  |\  /| \      /  | |   |
       \    \ `.__,'|  |`-._    `|      |__| \/ |  `.__,'|  | |   |
        \_.-'       |__|    `-._ |              '-.|     '-.| |   |
                                `'                            '-._|                                                                                       
                                                                           ";

        Console.WriteLine(title);
        Console.WriteLine("Welcome to the Pokedex!");
        await startGame();
    }

    public static async Task startGame()
    {
        Console.WriteLine("Enter the name of a Pokemon to see its stats! Enter quit to quit.");
        var p = Console.ReadLine()?.ToLower();
        bool hasArt = false;
        if (p == "quit")
        {
            quitGame();
        }
        else if (!await isRealPoke(p))
        {
            await startGame();
        } else
        {
            hasArt = PrintPoke(p.ToLower());
        }
        bool quit2 = false;
        while (!quit2)
        {
            if (!hasArt) { Console.ForegroundColor = ConsoleColor.Blue; }
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Pick a number to the see the stats for " + p + ":");
            Console.WriteLine("1. Basic Info (Capture Rate & Pokedex Number)");
            Console.WriteLine("2. Female to Male Ratio");
            Console.WriteLine("3. Base Happiness");
            Console.WriteLine("4. All of the above");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Q. Quit Game");
            Console.WriteLine("R. Restart");
            if (!hasArt) { Console.ForegroundColor = ConsoleColor.White; }
            var x = Console.ReadLine()?.ToLower();
            switch (x)
            { 
                case "1":
                    await GetPokemon(p.ToLower());
                    break;
                case "2":
                    await GetFemaletoMaleRate(p.ToLower());
                    break;
                case "3":
                    await GetBaseHappiness(p.ToLower());
                    break;
                case "4":
                    await GetPokemon(p.ToLower());
                    await GetFemaletoMaleRate(p);
                    await GetBaseHappiness(p);
                    break;
                case "q":
                    quit2 = true;
                    quitGame();
                    break;
                case "r":
                    // Console.WriteLine("Case 6 entered.");
                    quit2 = true;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    await startGame();
                    break;
                default:
                    Console.WriteLine("Invalid input. Try again!");
                    break;
                }
                // System.Threading.Thread.Sleep(2000); Console.Clear();
            }
    }

    public static async Task GetFemaletoMaleRate(string pokemonspecies)
    {
        try
        {
            PokemonSpecies p = await DataFetcher.GetNamedApiObject<PokemonSpecies>(pokemonspecies);

            float? r = p.FemaleToMaleRate;

            Console.WriteLine(p.Name + " has a female to male rate of " + r + "!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while fetching the female to male rate: " + ex.Message);
        }
    }

    public static async Task GetBaseHappiness(string pokemonspecies)
    {
        try
        {
            PokemonSpecies p = await DataFetcher.GetNamedApiObject<PokemonSpecies>(pokemonspecies);

            int r = p.BaseHappiness;

            Console.WriteLine(p.Name + " has a base happiness of " + r + "!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while fetching the base happiness: " + ex.Message);
        }
    }


    public static async Task GetPokemon(string pokemonspecies)
    {

        //Console.WriteLine("hi");

        //PokemonSpecies p = await DataFetcher.GetApiObject<PokemonSpecies>(395);

        try
        {
            PokemonSpecies p = await DataFetcher.GetNamedApiObject<PokemonSpecies>(pokemonspecies);


            float cRate = p.CaptureRate;
            float entrynumber = p.PokedexNumbers[0].EntryNumber;
            Console.WriteLine(p.Name + " has a capture rate of " + cRate + "!");
            Console.WriteLine(p.Name + " has a pokedex number of " + entrynumber + "!");
            string imageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{entrynumber}.png";
            // Create a BitmapImage object to load the image from the URL
            //BitmapImage image = new BitmapImage(new Uri(imageUrl));

            Console.WriteLine("The image url of this pokemon is: " + imageUrl);

        }

        catch (HttpRequestException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("Pokemon species not found.");
        }

    }

    public static async Task<bool> isRealPoke(string pokemonspecies)
    {
        try
        {
            PokemonSpecies p = await DataFetcher.GetNamedApiObject<PokemonSpecies>(pokemonspecies);


            return true;

        }

        catch (HttpRequestException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("Pokemon species not found.");
            return false;

        }

    }
    public static void quitGame() {
        Console.WriteLine("Thanks for playing!");
        System.Threading.Thread.Sleep(1500); System.Environment.Exit(0);

    }

            public static bool PrintPoke(string pokemonspecies) {
                string[] knownPokes = { "jigglypuff", "charizard", "pikachu", "squirtle", "bulbasaur" };
                string noArt = " _____  _______      _____      _____          __     _______          __    ___________                      .___\r\n  /  |  | \\   _  \\    /  |  |    /  _  \\________/  |_   \\      \\   _____/  |_  \\_   _____/___  __ __  ____    __| _/\r\n /   |  |_/  /_\\  \\  /   |  |_  /  /_\\  \\_  __ \\   __\\  /   |   \\ /  _ \\   __\\  |    __)/  _ \\|  |  \\/    \\  / __ | \r\n/    ^   /\\  \\_/   \\/    ^   / /    |    \\  | \\/|  |   /    |    (  <_> )  |    |     \\(  <_> )  |  /   |  \\/ /_/ | \r\n\\____   |  \\_____  /\\____   |  \\____|__  /__|   |__|   \\____|__  /\\____/|__|    \\___  / \\____/|____/|___|  /\\____ | \r\n     |__|        \\/      |__|          \\/                      \\/                   \\/                   \\/      \\/ ";
                if (!knownPokes.Contains(pokemonspecies)) {
                    Console.WriteLine(noArt);
                    return false;
                    //    System.Threading.Thread.Sleep(1500); Console.Clear();    
                    //    startGame();
                }
                if (pokemonspecies == "jigglypuff")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    string jigglypuff = @"
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⠶⠛⠉⠹⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⣤⣤⣤⣀⣤⠞⠋⠀⢀⣴⣿⣇⠹⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⠾⠛⠉⠁⠀⠈⠉⠙⠳⣦⣤⣿⣿⣿⣿⡄⢿⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢿⣿⣿⣿⡇⢸⡇⠀⠀⠀⠀⠀⠀⠀⠀
⢀⣶⠶⠶⠶⠶⠶⢶⣦⣤⣤⣤⣶⠾⡿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢻⣿⣿⣧⠈⣇⠀⠀⠀⠀⠀⠀⠀⠀
⣼⡇⠀⣰⣶⣤⣤⣤⣤⣄⣈⣉⠙⠒⡷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⠛⢿⣀⣿⠀⠀⠀⠀⠀⠀⠀⠀
⢿⡇⠀⢹⣿⣿⣿⣿⣿⣿⣿⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⠀⠀⠙⢿⣄⠀⠀⠀⠀⠀⠀⠀
⢸⡇⠀⠸⣿⣿⣿⣿⣿⣿⠃⠀⠀⠀⠀⠀⠀⠲⢦⣤⣄⣀⠀⠀⠀⠀⠀⠀⠀⠀⣿⠀⠀⠀⠀⠙⢧⡀⠀⠀⠀⠀⠀
⢸⣇⠀⠀⢻⣿⣿⣿⣿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡟⠙⢷⡄⠀⠀⠀⠀⠀⢀⣿⣶⣿⡿⢷⣄⠈⢿⡄⠀⠀⠀⠀
⠈⣿⠀⠀⠈⢿⣿⣿⠃⠀⠀⠀⠀⣀⣀⣤⣤⣀⡀⠀⣧⠀⠀⠀⠀⠀⠀⠀⢠⡾⢿⣿⣿⡀⠀⢹⣧⠀⢻⡀⠀⠀⠀
⠀⠹⣧⠀⠀⠈⢿⡏⠀⠀⢀⡴⠋⣩⣿⣿⡿⠛⠻⣷⣼⡷⣄⣀⣀⣠⣤⠾⠋⠀⣾⣿⣿⣿⣶⣾⣿⣇⠈⣧⠀⠀⠀
⠀⠀⣿⡆⠀⠀⠀⠀⠀⢠⡟⢠⣾⣿⣿⣟⠀⠀⠀⢸⣿⣷⡀⠉⠉⠁⠀⠀⠀⠀⢻⣿⣿⣿⣿⣿⣿⣿⠀⢿⠟⠛⣷
⠀⠀⣿⠀⠀⠀⠀⠀⠀⣿⠀⣾⣿⣿⣿⣿⣦⣀⣠⣾⣿⡏⢷⠀⠀⠀⠀⠀⠀⠀⠘⣎⠻⣿⣿⣿⣿⣿⠀⢸⠀⣸⠇
⠀⠀⢿⡄⠀⠀⠀⠀⠀⣿⠀⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⣸⠀⠀⠀⠀⠀⢀⣀⣀⠈⢧⣌⣉⣋⣴⠃⠀⢸⣴⠏⠀
⠀⠀⢸⡇⠀⠀⠀⠀⠀⢹⣆⠘⢿⣿⣿⣿⣿⣿⣿⣿⠟⢠⠏⠀⠀⣰⣶⣿⣿⣿⣿⠀⠀⠈⠉⠉⠀⠀⠀⣿⠃⠀⠀
⠀⠀⠸⣷⠀⠀⠀⠀⠀⠀⠻⣦⡀⠙⠻⠿⠿⠿⠛⢁⣴⠏⠀⠀⠀⢸⡄⠀⠙⢿⣿⠀⠀⠀⠀⠀⠀⠀⢰⡏⠀⠀⠀
⠀⠀⠀⢹⣇⠀⠀⠀⠀⠀⠀⠈⠛⠶⣦⣤⣤⡴⠾⠋⠀⠀⠀⠀⠀⠈⣷⠀⠀⠀⢻⠀⠀⠀⠀⠀⠀⢀⡿⠀⠀⠀⠀
⠀⠀⠀⠀⢻⣦⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠦⣤⣀⡀⠀⠀⠀⠀⠸⣧⡀⢀⡿⠀⠀⠀⠀⠀⢠⡾⠁⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠹⣷⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠉⠛⠳⢦⡄⠀⠀⠘⠻⠞⠃⠀⠀⠀⠀⣰⠟⠁⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠈⠻⣦⡀⠀⠀⠀⠀⠀⠀⣀⠀⠀⠀⢀⣀⣴⠞⠁⠀⠀⠀⠀⠀⠀⠀⢀⣠⠞⠁⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⢷⣤⣀⠀⠀⠀⠙⠛⠛⠛⠋⠉⠀⠀⠀⠀⠀⠀⠀⢀⣠⣴⡟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠻⣶⣦⣤⣀⣀⣀⣀⣀⣀⣀⣠⣤⣴⣶⠟⠋⠉⠈⠙⠛⢶⣄⡀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠞⠋⠉⠀⠀⠈⠉⠉⠉⢉⣹⡏⠁⠀⠀⠈⠛⢶⣤⣀⡀⠀⠀⠈⢻⣄⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⠿⣦⣤⣤⣤⣤⡴⠶⠶⠛⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⠛⠷⠶⠶⠟⠀⠀⠀";


                    Console.WriteLine(jigglypuff);
                }

                if (pokemonspecies == "charizard")
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    string charizard = @"
.""-,.__
                 `.     `.  ,
              .--'  .._,'""-' `.
             .    .'         `'
             `.   /          ,'
               `  '--.   ,-""'
                `""`   |  \
                   -. \, |
                    `--Y.'      ___.
                         \     L._, \
               _.,        `.   <  <\                _
             ,' '           `, `.   | \            ( `
          ../, `.            `  |    .\`.           \ \_
         ,' ,..  .           _.,'    ||\l            )  '"".
        , ,'   \           ,'.-.`-._,'  |           .  _._`.
      ,' /      \ \        `' ' `--/   | \          / /   ..\
    .'  /        \ .         |\__ - _ ,'` `        / /     `.`.
    |  '          ..         `-...-""  |  `-'      / /        . `.
    | /           |L__           |    |          / /          `. `.
   , /            .   .          |    |         / /             ` `
  / /          ,. ,`._ `-_       |    |  _   ,-' /               ` \
 / .           \""`_/. `-_ \_,.  ,'    +-' `-'  _,        ..,-.    \`.
.  '         .-f    ,'   `    '.       \__.---'     _   .'   '     \ \
' /          `.'    l     .' /          \..      ,_|/   `.  ,'`     L`
|'      _.-""""` `.    \ _,'  `            \ `.___`.'""`-.  , |   |    | \
||    ,'      `. `.   '       _,...._        `  |    `/ '  |   '     .|
||  ,'          `. ;.,.---' ,'       `.   `.. `-'  .-' /_ .'    ;_   ||
|| '              V      / /           `   | `   ,'   ,' '.    !  `. ||
||/            _,-------7 '              . |  `-'    l         /    `||
. |          ,' .-   ,' ||               | .-.        `.      .'     ||
 `'        ,'    `"".'    |               |    `.        '. -.'       `'
          /      ,'      |               |,'    \-.._,.'/'
          .     /        .               .       \    .''
        .`.    |         `.             /         :_,'.'
          \ `...\   _     ,'-.        .'         /_.-'
           `-.__ `,  `'   .  _.>----''.  _  __  /
                .'        /""'          |  ""'   '_
               /_|.-'\ ,"".             '.'`__'-( \
                 / ,""'""\,'               `/  `-.|"" ";

                    Console.WriteLine(charizard);
                }



                if (pokemonspecies == "pikachu")
                {

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    string pikachu = @"
quu..__
 $$$b  `---.__
  ""$$b        `--.                          ___.---uuudP
   `$$b           `.__.------.__     __.---'      $$$$""              .
     ""$b          -'            `-.-'            $$$""              .'|
       "".                                       d$""             _.'  |
         `.   /                              ...""             .'     |
           `./                           ..::-'            _.'       |
            /                         .:::-'            .-'         .'
           :                          ::''\          _.'            |
          .' .-.             .-.           `.      .'               |
          : /'$$|           .@""$\           `.   .'              _.-'
         .'|$u$$|          |$$,$$|           |  <            _.-'
         | `:$$:'          :$$$$$:           `.  `.       .-'
         :                  `""--'             |    `-.     \
        :##.       ==             .###.       `.      `.    `\
        |##:                      :###:        |        >     >
        |#'     `..'`..'          `###'        x:      /     /
         \                                   xXX|     /    ./
          \                                xXXX'|    /   ./
          /`-.                                  `.  /   /
         :    `-  ...........,                   | /  .'
         |         ``:::::::'       .            |<    `.
         |             ```          |           x| \ `.:``.
         |                         .'    /'   xXX|  `:`M`M':.
         |    |                    ;    /:' xXXX'|  -'MMMMM:'
         `.  .'                   :    /:'       |-'MMMM.-'
          |  |                   .'   /'        .'MMM.-'
          `'`'                   :  ,'          |MMM<
            |                     `'            |tbap\
             \                                  :MM.-'
              \                 |              .''
               \.               `.            /
                /     .:::::::.. :           /
               |     .:::::::::::`.         /
               |   .:::------------\       /
              /   .''               >::'  /
              `',:                 :    .'
                                   `:.:'";

                    Console.WriteLine(pikachu);
                }

                if (pokemonspecies == "squirtle")
                {

                    Console.ForegroundColor = ConsoleColor.Blue;
                    string squirtle = @"
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀     _,........__
            ,-'            ""`-.
          ,'                   `-.
        ,'                        \
      ,'                           .
      .'\               ,"""".       `
     ._.'|             / |  `       \
     |   |            `-.'  ||       `.
     |   |            '-._,'||       | \
     .`.,'             `..,'.'       , |`-.
     l                       .'`.  _/  |   `.
     `-.._'-   ,          _ _'   -"" \  .     `
`.""""""""""'-.`-...,---------','         `. `....__.
.'        `""-..___      __,'\          \  \     \
\_ .          |   `""""""""'    `.           . \     \
  `.          |              `.          |  .     L
    `.        |`--...________.'.        j   |     |
      `._    .'      |          `.     .|   ,     |
         `--,\       .            `7""""' |  ,      |
            ` `      `            /     |  |      |    _,-'""""""`-.
             \ `.     .          /      |  '      |  ,'          `.
              \  v.__  .        '       .   \    /| /              \
               \/    `""""\""""""""""""""`.       \   \  /.''                |
                `        .        `._ ___,j.  `/ .-       ,---.     |
                ,`-.      \         .""     `.  |/        j     `    |
               /    `.     \       /         \ /         |     /    j
              |       `-.   7-.._ .          |""          '         /
              |          `./_    `|          |            .     _,'
              `.           / `----|          |-............`---'
                \          \      |          |
               ,'           )     `.         |
                7____,,..--'      /          |
                                  `---.__,--.'mh";

                    Console.WriteLine(squirtle);
                }

                if (pokemonspecies == "bulbasaur")
                {

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    string bulbasaur = @"
                                           /
                        _,.------....___,.' ',.-.
                     ,-'          _,.--""        |
                   ,'         _.-'              .
                  /   ,     ,'                   `
                 .   /     /                     ``.
                 |  |     .                       \.\
       ____      |___._.  |       __               \ `.
     .'    `---""""       ``""-.--""'`  \               .  \
    .  ,            __               `              |   .
    `,'         ,-""'  .               \             |    L
   ,'          '    _.'                -._          /    |
  ,`-.    ,"".   `--'                      >.      ,'     |
 . .'\'   `-'       __    ,  ,-.         /  `.__.-      ,'
 ||:, .           ,'  ;  /  / \ `        `.    .      .'/
 j|:D  \          `--'  ' ,'_  . .         `.__, \   , /
/ L:_  |                 .  ""' :_;                `.'.'
.    """"'                  """"""""""'                    V
 `.                                 .    `.   _,..  `
   `,_   .    .                _,-'/    .. `,'   __  `
    ) \`._        ___....----""'  ,'   .'  \ |   '  \  .
   /   `. ""`-.--""'         _,' ,'     `---' |    `./  |
  .   _  `""""'--.._____..--""   ,             '         |
  | ."" `. `-.                /-.           /          ,
  | `._.'    `,_            ;  /         ,'          .
 .'          /| `-.        . ,'         ,           ,
 '-.__ __ _,','    '`-..___;-...__   ,.'\ ____.___.'
 `""^--'..'   '-`-^-'""--    `-^-'`.''""""""""""`.,^.`.--'";

                    Console.WriteLine(bulbasaur);
                }


                return true;
            }

}