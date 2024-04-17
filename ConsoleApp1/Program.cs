using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int[] array = GenerateRandomArray(10000000); 
        string inputFileName = "input.txt";
        string outputDirectory = "output";
        string outputFilePattern = "output_{0}.txt";       
        WriteArrayToFile(inputFileName, array);     
        SortFile(inputFileName);      
        SplitFile(inputFileName, outputDirectory, outputFilePattern);
    }
   
    static int[] GenerateRandomArray(int size)
    {
        Random random = new Random();
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next(1, 10000000);
        }
        return array;
    }    
    static void WriteArrayToFile(string fileName, int[] array)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (int num in array)
            {
                writer.WriteLine(num);
            }
        }
    }  
    static void SortFile(string fileName)
    {
        List<int> numbers = new List<int>();
        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                numbers.Add(int.Parse(line));
            }
        }

        numbers.Sort();
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (int num in numbers)
            {
                writer.WriteLine(num);
            }
        }
    }
    static void SplitFile(string inputFileName, string outputDirectory, string outputFilePattern)
    {
        Directory.CreateDirectory(outputDirectory);

        int maxSize = 5 * 1024; 
        using (StreamReader reader = new StreamReader(inputFileName))
        {
            int partNum = 1;
            int currentSize = 0;
            StreamWriter currentWriter = new StreamWriter(Path.Combine(outputDirectory, string.Format(outputFilePattern, partNum)));

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                currentWriter.WriteLine(line);
                currentSize += System.Text.Encoding.UTF8.GetByteCount(line);

                if (currentSize >= maxSize)
                {
                    currentWriter.Close();
                    partNum++;
                    currentWriter = new StreamWriter(Path.Combine(outputDirectory, string.Format(outputFilePattern, partNum)));
                    currentSize = 0;
                }
            }
            currentWriter.Close();
        }
    }
}