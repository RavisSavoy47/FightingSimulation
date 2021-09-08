using System;
using System.Collections.Generic;
using System.Text;

namespace FightingSimulation
{

    struct Monster
    {
        public string name;
        public float health;
        public float attack;
        public float defense;

    }

    class Game
    {
        bool gameOver = false;
        Monster currentMonster1;
        Monster currentMonster2;

        //Monsters
        Monster wompus;
        Monster thwompus;
        Monster backupWompus;
        Monster unclePhil;
        int currentScene = 0;
        int currentMonsterIndex = 0;
        public void Run()
        {
            Start();

            while (!gameOver)
            {
                Update();
            }

        }

        void Start()
        {

            //Initilize Monster
            wompus.name = "Wompus";
            wompus.attack = 15.0f;
            wompus.defense = 5.0f;
            wompus.health = 20.0f;


            thwompus.name = "Thwompus";
            thwompus.attack = 15.0f;
            thwompus.defense = 10.0f;
            thwompus.health = 15.0f;


            backupWompus.name = "Backup Wompus";
            backupWompus.attack = 25.6f;
            backupWompus.defense = 5.0f;
            backupWompus.health = 3.0f;


            unclePhil.name = "Uncle Phil";
            unclePhil.attack = 100000000000.0f;
            unclePhil.defense = 0f;
            unclePhil.health = 1.0f;

            //Set Starting fighters
            currentMonster1 = GetMonster(currentMonsterIndex);
            currentMonsterIndex++;
            currentMonster2 = GetMonster(currentMonsterIndex);



        }

        void displayCurrentScene()
        {
            if (currentScene == 0)
            {
                DisplayStartMenu();
            }

            else if (currentScene == 1)
            {
                Battle();
                UpdateCurrentMonsters();
                Console.ReadKey(true);
                Console.Clear();
            }
            else if (currentScene == 2)
            {
                DisplayRestartMenu();
            }

        }

        int GetInput(string description, string option1, string option2, bool pauseInvaild = false)
        {
            Console.WriteLine(description);
            Console.WriteLine("1. " + option1);
            Console.WriteLine("2. " + option2);

            string input = Console.ReadLine();
            int choice = 0;

            if (input == "1")
            {
                choice = 1;
            }
            else if (input == "2")
            {
                choice = 2;
            }
            else
            {
                Console.WriteLine("Invaild Input");
                if (pauseInvaild)
                {
                    Console.ReadKey(true);
                }
            }

            return choice;
        }

        void DisplayStartMenu()
        {
            GetInput("description", "option1", "option2");
        }

        void DisplayRestartMenu()
        {

        }

        void Update()
        {
            Battle();
            UpdateCurrentMonsters();

            DisplayRestartMenu();
            Console.ReadKey(true);
            Console.Clear();
        }




        Monster GetMonster(int monsterIndex)
        {
            Monster monster;
            monster.name = "None";
            monster.attack = 1;
            monster.defense = 1;
            monster.health = 1;

            if (monsterIndex == 0)
            {
                monster = unclePhil;
            }
            else if (monsterIndex == 1)
            {
                monster = backupWompus;
            }
            else if (monsterIndex == 2)
            {
                monster = wompus;
            }
            else if (monsterIndex == 3)
            {
                monster = thwompus;
            }

            return monster;
        }
        /// <summary>
        /// Simulates one turn in the current monster fight
        /// </summary>
        void Battle()
        {
            //Print monster 1 stats
            PrintStats(currentMonster1);
            //Print monster 2 stats
            PrintStats(currentMonster2);

            //monster 1 attacks monster 2
            float damageTaken = Fight(currentMonster1, ref currentMonster2);
            Console.WriteLine(currentMonster2.name + " has taken " + damageTaken);

            //monster 2 attacks monster 1

            damageTaken = Fight(currentMonster2, ref currentMonster1);
            Console.WriteLine(currentMonster1.name + " has taken " + damageTaken);
        }
        /// <summary>
        /// Updates the current monster to the next monster when one dies.
        /// Ends the game if all monster in th elist have been used.
        /// </summary>
        void UpdateCurrentMonsters()
        {
            //If monster 1 has died..
            if (currentMonster1.health <= 0)
            {
                //...increment the current monster index and swap out the monster
                currentMonsterIndex++;
                currentMonster1 = GetMonster(currentMonsterIndex);
            }
            //If monster 2 has died..
            if (currentMonster2.health <= 0)
            {
                //...increment the current monster index and swap out the monster
                currentMonsterIndex++;
                currentMonster2 = GetMonster(currentMonsterIndex);
            }
            //If either monster is set to "None" and last monster has been set..
            if (currentMonster2.name == "None" || currentMonster1.name == "None" && currentMonsterIndex >= 4)
            {
                //...end the game
                Console.WriteLine("Simulation Over");
                gameOver = true;
            }
        }

        String StartBattle(ref Monster monster1, ref Monster monster2)
        {
            string matchResult = "No Contest";

            while (monster1.health > 0 && monster2.health > 0)
            {
                //Print monster 1 stats
                PrintStats(monster1);
                //Print monster 2 stats
                PrintStats(monster2);

                //monster 1 attacks monster 2
                float damageTaken = Fight(monster1, ref monster2);
                Console.WriteLine(monster2.name + " has taken " + damageTaken);

                //monster 2 attacks monster 1

                damageTaken = Fight(monster2, ref monster1);
                Console.WriteLine(monster1.name + " has taken " + damageTaken);

                Console.ReadKey(true);
                Console.Clear();
            }



            if (monster1.health < 0 && monster2.health <= 0)
            {
                matchResult = "Draw";
            }

            else if (monster1.health > 0)
            {
                matchResult = monster1.name;
            }

            else if (monster2.health > 0)
            {
                matchResult = monster2.name;
            }


            return matchResult;
        }


        float Fight(Monster attacker, ref Monster defender)
        {
            float damageTaken = CalculateDamage(attacker, defender);
            defender.health -= damageTaken;
            return damageTaken;
        }

        void PrintStats(Monster monster)
        {
            Console.WriteLine("Name: " + monster.name);
            Console.WriteLine("Health " + monster.health);
            Console.WriteLine("Attack " + monster.attack);
            Console.WriteLine("Defense " + monster.defense);
        }

        float CalculateDamage(float attack, float defense)
        {
            float damage = attack - defense;
            if (attack - defense <= 0)
            {
                damage = 0;
            }

            return damage;
        }

        float CalculateDamage(Monster Attacker, Monster Defender)
        {
            return Attacker.attack - Defender.defense;
        }

    }
}
