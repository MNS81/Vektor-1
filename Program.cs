using System;
using System.Globalization;

Console.WriteLine("Департамент     Сотрудников     Тугрики     Кофе     Страницы     Тугр./стр.");
Console.WriteLine("----------------------------------------------------------------------------");
var dept1 = new Departament(Console.ReadLine());
var dept2 = new Departament(Console.ReadLine());
var dept3 = new Departament(Console.ReadLine());
var dept4 = new Departament(Console.ReadLine());
var allCoffee = (dept1.coffee + dept2.coffee + dept3.coffee + dept4.coffee);
var allReport = (dept1.report + dept2.report + dept3.report + dept4.report);
var allWorkers = (dept1.workersCount + dept2.workersCount + dept3.workersCount + dept4.workersCount);
var allSalary = (dept1.salary + dept2.salary + dept3.salary + dept4.salary);
var allPerformance = Math.Round(allSalary / allReport, 2, MidpointRounding.AwayFromZero);
Console.WriteLine("----------------------------------------------------------------------------");
Console.WriteLine("Всего".PadRight(16) + allWorkers.ToString().PadRight(16) + allSalary.ToString().PadRight(12) + 
    allCoffee.ToString().PadRight(9) + allReport.ToString().PadRight(13) + allPerformance);

public class Departament
{
    public string name { get; set; }
    public string boss { get; set; }
    public string workers { get; set; }
    public double coffee = 0;
    public double report = 0;
    public double salary = 0;
    public double workersCount = 0;
    public double performance = 0;
    public Departament(string line)
    {
        var allWorkers = line.Split(": ")[1].Split(" + ");
        name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(line.Split(": ")[0].Split()[1]);
        workers = allWorkers[0];
        boss = allWorkers[1].Split()[2];
        GetResults();
        GetBossResults();
        GetPerformance();
        PrintInfo();
    }
    public void GetResults()
    {
        foreach (var worker in workers.Split(", "))
        {
            var temp = worker.Split("*");
            var count = int.Parse(temp[0]);
            workersCount += count;
            if (temp[1].StartsWith("manager"))
            {
                var manager = new Manager(count, temp[1].EndsWith("2") ? 2 : temp[1].EndsWith("3") ? 3 : 1);
                salary += manager.GetSalary();
                coffee += manager.GetCoffee();
                report += manager.GetReport();
            }
            else if (temp[1].StartsWith("marketer"))
            {
                var marketer = new Marketer(count, temp[1].EndsWith("2") ? 2 : temp[1].EndsWith("3") ? 3 : 1);
                salary += marketer.GetSalary();
                coffee += marketer.GetCoffee();
                report += marketer.GetReport();
            }
            else if (temp[1].StartsWith("engineer"))
            {
                var engineer = new Engineer(count, temp[1].EndsWith("2") ? 2 : temp[1].EndsWith("3") ? 3 : 1);
                salary += engineer.GetSalary();
                coffee += engineer.GetCoffee();
                report += engineer.GetReport();
            }
            else
            {
                var analyst = new Analyst(count, temp[1].EndsWith("2") ? 2 : temp[1].EndsWith("3") ? 3 : 1);
                salary += analyst.GetSalary();
                coffee += analyst.GetCoffee();
                report += analyst.GetReport();
            }
        }
    }
    public void GetBossResults()
    {
        workersCount++;
        if (boss.StartsWith("manager"))
        {
            var manager = new Manager(1, boss.EndsWith("2") ? 2 : boss.EndsWith("3") ? 3 : 1);
            salary += manager.GetSalary() * 1.5;
            coffee += manager.GetCoffee() * 2;
        }
        else if (boss.StartsWith("marketer"))
        {
            var marketer = new Marketer(1, boss.EndsWith("2") ? 2 : boss.EndsWith("3") ? 3 : 1);
            salary += marketer.GetSalary() * 1.5;
            coffee += marketer.GetCoffee() * 2;
        }
        else if (boss.StartsWith("engineer"))
        {
            var engineer = new Engineer(1, boss.EndsWith("2") ? 2 : boss.EndsWith("3") ? 3 : 1);
            salary += engineer.GetSalary() * 1.5;
            coffee += engineer.GetCoffee() * 2;
        }
        else
        {
            var analyst = new Analyst(1, boss.EndsWith("2") ? 2 : boss.EndsWith("3") ? 3 : 1);
            salary += analyst.GetSalary() * 1.5;
            coffee += analyst.GetCoffee() * 2;
        }
    }
    public void GetPerformance() => performance = Math.Round(salary / report, 2, MidpointRounding.AwayFromZero);
    public void PrintInfo() => Console.WriteLine(name.PadRight(16, ' ') + workersCount.ToString().PadRight(16, ' ') + 
        salary.ToString().PadRight(12, ' ') + coffee.ToString().PadRight(9, ' ') + report.ToString().PadRight(13, ' ') + 
        performance);
}
public class Manager
{
    public double count;
    public double rank;
    public Manager(int count, int rank)
    {
        this.count = count;
        this.rank = rank;
    }
    public double GetCoffee() => 20 * count;
    public double GetReport() => 200 * count;
    public double GetSalary() => count * (rank == 1 ? 50000 : rank == 2 ? 50000 * 1.25 : 50000 * 1.5);
}
public class Marketer : Manager
{
    public Marketer(int count, int rank) : base(count, rank)
    {
        this.count = count;
        this.rank = rank;
    }
    public double GetCoffee() => 15 * count;
    public double GetReport() => 150 * count;
    public double GetSalary() => count * (rank == 1 ? 40000 : rank == 2 ? 40000 * 1.25 : 40000 * 1.5);
}
public class Analyst : Manager
{
    public Analyst(int count, int rank) : base(count, rank)
    {
        this.count = count;
        this.rank = rank;
    }
    public double GetCoffee() => 50 * count;
    public double GetReport() => 5 * count;
    public double GetSalary() => count * (rank == 1 ? 80000 : rank == 2 ? 80000 * 1.25 : 80000 * 1.5);
}
public class Engineer : Manager
{
    public Engineer(int count, int rank) : base(count, rank)
    {
        this.count = count;
        this.rank = rank;
    }
    public double GetCoffee() => 5 * count;
    public double GetReport() => 50 * count;
    public double GetSalary() => count * (rank == 1 ? 20000 : rank == 2 ? 20000 * 1.25 : 20000 * 1.5);
}