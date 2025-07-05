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
                }
                else
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

string[,] newArrivals = AssignAnimalToGroup("B", 1);

// Print all data
for (int row = 0; row < maxData; row++)
{
    if (visitingGroups[row, codeSchool] == "")
        continue;

    PrintSchoolName(row, true);
    PrintAnimalGroup(row);
}

string[] RandomizeAnimals()
{
    /* Create a unique randomized array of animal names from pettingZoo */
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

        // For debugging purposes, you can comment the next two lines
        Console.WriteLine($"Combinations count: {combinations.Length}");
        Console.WriteLine($"Randomized indices: {string.Join(" ", randomizedIndices)}");
        
        combinations = [.. combinations, randomizedIndices];
    }

    foreach (int index in randomizedIndices)
        randomized = [.. randomized, pettingZoo[index]];

    return randomized;
}

string[,] AssignAnimalToGroup(string school, int groups = 6)
{
    string[,] assignedGroups;
    int checkedSchools = 0;

    for (int row = 0; row < maxData; row++)
    {
        if (visitingGroups[row, codeSchool] != school)
            checkedSchools++;
    }
    if (checkedSchools == 6)
    {
        Console.WriteLine($"School {school} has reached the maximum number of groups.");
        return new string[0, 0];
    }
    else
    {
        if (checkedSchools + groups > 6)
        {
            Console.WriteLine($"Cannot add {groups} more group(s) to school {school}");
            return new string[0, 0];
        }
        else
        {
            assignedGroups = new string[groups, 3];
            for (int row = 0; row < groups; row++)
            {
                assignedGroups[row, codeSchool] = school;
                assignedGroups[row, codeGroup] = (checkedSchools + row + 1).ToString();
                assignedGroups[row, codeAnimalsOrder] = string.Join(" ", RandomizeAnimals());
            }
        }
    }

    return assignedGroups;
}

void PrintSchoolName(int index, bool inID = false)
{
    if (inID)
        Console.WriteLine($"Group ID: {visitingGroups[index, codeSchool] + visitingGroups[index, codeGroup]} ");
    else
        Console.WriteLine($"School: {visitingGroups[index, codeSchool]} ");
}

void PrintAnimalGroup(int index, bool inDetail = false)
{
    if (inDetail)
    {
        string[] animals = visitingGroups[index, codeAnimalsOrder].Split(' ');
        Console.WriteLine("Animal visit order: ");
        for (int animalIndex = 0; animalIndex < animals.Length; animalIndex++)
            Console.WriteLine($"  {animalIndex + 1}. {animals[animalIndex]}");
    }
    else
    {
        Console.WriteLine($"Animal visit order: {visitingGroups[index, codeAnimalsOrder]} ");
    }
}
