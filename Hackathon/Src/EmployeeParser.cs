using System.Resources;
using Microsoft.CSharp.RuntimeBinder;
using Nsu.HackathonProblem.Contracts;

namespace Hackathon;

public class EmployeeParser
{
    private readonly string _directoryPath;

    public EmployeeParser(string directoryPath)
    {
        _directoryPath = directoryPath;
    }
    
    public List<Employee> Parse(string fileName)
    {
        string filePath = Path.Combine(_directoryPath, fileName);
        if (File.Exists(filePath))
        {
            List<Employee> employees = [];
            using (StreamReader reader = new StreamReader(filePath))
            {
                string header = reader.ReadLine();
                string[] columns = header.Split(';');

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(';');
                    employees.Add(new Employee(Convert.ToInt32(values[0]) - 1, values[1]));
                }
            }
            return employees;
        }
        throw new RuntimeBinderException($"File {filePath} not found");
    }
}