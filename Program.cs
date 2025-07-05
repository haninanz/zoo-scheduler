using System;

// Arrays to keep data 
int maxData = 18;
string[] pettingZoo =
[
    "alpacas", "capybaras", "chickens", "ducks", "emus", "geese",
    "goats", "iguanas", "kangaroos", "lemurs", "llamas", "macaws",
    "ostriches", "pigs", "ponies", "rabbits", "sheep", "tortoises",
];
string[,] visitingGroups = new string[maxData, 4];
int[][] combinations = [];

// Code number to distinguish which row keeps which data
int codeSchool = 0;
int codeGroup = 1;
int codeAnimalsOrder = 2;
// int code = 3;

// Generate data
int groupsACount = 0;
int groupsBCount = 0;
int groupsCCount = 0;
for (int row = 0; row < maxData; row++)
{
    switch (row / 6)
    {
        case 0:
            {
                visitingGroups[row, codeSchool] = "A";
                visitingGroups[row, codeGroup] = (++groupsACount).ToString();
                visitingGroups[row, codeAnimalsOrder] = string.Join(" ", RandomizeAnimals());
                break;
            }
        case 1:
            {
                if (groupsBCount < 3)
                {
                    visitingGroups[row, codeSchool] = "B";
                    visitingGroups[row, codeGroup] = (++groupsBCount).ToString();
                    visitingGroups[row, codeAnimalsOrder] = string.Join(" ", RandomizeAnimals());
                } else
                {
                    visitingGroups[row, codeSchool] = "";
                    visitingGroups[row, codeGroup] = "";
                    visitingGroups[row, codeAnimalsOrder] = "";
                }
                break;
            }
        case 2:
            {
                if (groupsCCount < 4)
                {
                    visitingGroups[row, codeSchool] = "C";
                    visitingGroups[row, codeGroup] = (++groupsCCount).ToString();
                    visitingGroups[row, codeAnimalsOrder] = string.Join(" ", RandomizeAnimals());
                }
                else
                {
                    visitingGroups[row, codeSchool] = "";
                    visitingGroups[row, codeGroup] = "";
                    visitingGroups[row, codeAnimalsOrder] = "";
                }
                break;
            }
        default:
            break;
    }
}

// Print all data
for (int row = 0; row < maxData; row++) 
{
    if (visitingGroups[row, codeSchool] == "")
        continue;

    Console.Write($"Group ID: {visitingGroups[row, codeSchool] + visitingGroups[row, codeGroup]}\n");
    Console.Write($"Animal visit order: {visitingGroups[row, codeAnimalsOrder]}\n");
}

// for (int index = 0; index < combinations.Length; index++)
// {
//     Console.WriteLine($"Index: {string.Join(" ", combinations[index])}");
// }

string[] RandomizeAnimals()
{
    string[] randomized = [];
    int[] randomizedIndices = [];
    Random random = new();

    if (combinations.Length == 0)
    {
        for (int index = 0; index < pettingZoo.Length; index++)
        {
            int number = random.Next(pettingZoo.Length);

            while (randomizedIndices.Contains(number))
                number = random.Next(pettingZoo.Length);

            randomizedIndices = [.. randomizedIndices, number];
        }
        combinations = [.. combinations, randomizedIndices];
    }
    else
    {
        do
        {
            for (int index = 0; index < pettingZoo.Length; index++)
            {
                int number;
                int[] allNumbers = [.. Enumerable.Range(0, pettingZoo.Length)];
                int[] chosenNumbers = [];
                foreach (int[] combination in combinations)
                    chosenNumbers = [.. chosenNumbers, combination[index]];

                int[] possibleNumbers = [.. allNumbers.Where(n => !chosenNumbers.Contains(n) && !randomizedIndices.Contains(n))];
                try
                {
                    number = possibleNumbers[random.Next(possibleNumbers.Length)];
                    // if (chosenNumbers.Contains(number))
                    // {
                    //     Console.WriteLine($"Chosen numbers: {string.Join(" ", chosenNumbers)}");
                    //     break;
                    // }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }

                randomizedIndices = [.. randomizedIndices, number];
            }
            if (randomizedIndices.Length < pettingZoo.Length)
                randomizedIndices = [];
        } while (randomizedIndices.Length < pettingZoo.Length);
        // for (int index = 0; index < pettingZoo.Length; index++)
        // {
        //     int number;
        //     int[] allNumbers = [.. Enumerable.Range(0, pettingZoo.Length)];
        //     int[] chosenNumbers = [];
        //     foreach (int[] combination in combinations)
        //         chosenNumbers = [.. chosenNumbers, combination[index]];

        //     int[] possibleNumbers = [.. allNumbers.Where(n => !chosenNumbers.Contains(n) && !randomizedIndices.Contains(n))];
        //     Console.WriteLine($"Possible numbers: {string.Join(" ", possibleNumbers)}");
        //     if (possibleNumbers.Length == 0)
        //         continue;
        //     else if (possibleNumbers.Length == 1)
        //         number = possibleNumbers[0];
        //     else
        //         number = possibleNumbers[random.Next(possibleNumbers.Length)];

        //     randomizedIndices = [.. randomizedIndices, number];
        // }
        Console.WriteLine($"Combinations count: {combinations.Length}");
        Console.WriteLine($"Randomized indices: {string.Join(" ", randomizedIndices)}");
        combinations = [.. combinations, randomizedIndices];
    }

    foreach (int index in randomizedIndices)
        randomized = [.. randomized, pettingZoo[index]];

    return randomized;
}

// void AssignAnimalToGroup()
// {

// }

// void PrintSchoolName(int index)
// {
//     Console.WriteLine(visitingGroups[index, codeSchool]);
// }

// void PrintAnimalGroup()
// {

// }
