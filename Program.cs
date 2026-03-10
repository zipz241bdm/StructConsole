using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructConsole
{
    internal class Program
    {
        static Entrant[] ReadEntrantsArray(int n)
        {
            Entrant[] entrants = new Entrant[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write($"\nВведіть прізвище та ініціали абітурієнта №{i+1}: ");
                string name = Console.ReadLine();
                Input("Введіть ідентифікаційний код абітурієнта: ", out int idNum);
                Input("Введіть бали за підготовчі курси: ", out int coursePoints);
                Input("Введіть бал атестату: ", out double avgPoints);
                Input("Введіть кількість предметів на ЗНО: ", out int subjectCount);
                ZNO[] znoResults = new ZNO[subjectCount];
                for (int j = 0; j < subjectCount; j++)
                {
                    Console.Write($"Введіть назву предмета №{j+1}: ");
                    znoResults[j].Subject = Console.ReadLine();
                    Input($"Введіть оцінку предмета №{j+1}: ", out znoResults[j].Points);
                }
                entrants[i] = new Entrant(name, idNum, coursePoints, avgPoints, znoResults);
            }
            return entrants;
        }
        static void Input(string prompt, out int x)
        {
            bool f;
            do
            {
                Console.Write(prompt);
                f = int.TryParse(Console.ReadLine(), out x);
                if (!f)
                {
                    WriteLineError("Помилка введення значення. " +
                        "Будь ласка, повторіть ще раз!");
                }
                else if (x < 0.0)
                {
                    WriteLineError("Значення має бути більше нуля. " +
                        "Будь ласка, повторіть ще раз!");
                }
            } while (!f || x < 0.0);
        }
        static void Input(string prompt, out double x)
        {
            bool f;
            do
            {
                Console.Write(prompt);
                f = double.TryParse(Console.ReadLine(), out x);
                if (!f)
                {
                    WriteLineError("Помилка введення значення. " +
                        "Будь ласка, повторіть ще раз!");
                }
                else if (x < 0.0)
                {
                    WriteLineError("Значення має бути більше нуля. " +
                        "Будь ласка, повторіть ще раз!");
                }
            } while (!f || x < 0.0);
        }
        static void WriteLineError(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ResetColor();
        }
        static void PrintEntrant(Entrant entr, bool isExpanded = false)
        {
            Console.WriteLine($"Прізвище та ініціали абітурієнта: {entr.Name}");
            Console.WriteLine($"Ідентифікаційний код абітурієнта: {entr.IdNum}");
            Console.WriteLine($"Бали за підготовчі курси: {entr.CoursePoints}");
            Console.WriteLine($"Бал атестату: {entr.AvgPoints}");
            Console.WriteLine("Предмети ЗНО: ");
            for (int i = 0; i < entr.ZNOResults.Length; i++)
            {
                Console.WriteLine($"Назва предмета №{i+1}: {entr.ZNOResults[i].Subject}");
                Console.WriteLine($"Оцінка предмета №{i+1}: {entr.ZNOResults[i].Points}");
            }
            if (isExpanded)
            {
                Console.WriteLine($"Конкурсний бал: {entr.GetCompMark()}");
                Console.WriteLine($"Найкращий бал з предмету: {entr.GetBestSubject()}");
                Console.WriteLine($"Найгірший бал з предмету: {entr.GetWorstSubject()}");
            }
        }
        static void PrintEntrants(Entrant[] entrants)
        {
            foreach (Entrant entr in entrants)
            {
                PrintEntrant(entr);
                Console.WriteLine();
            }
        }
        static void GetEntrantsInfo(Entrant[] entrants, out double max, out double min)
        {
            max = entrants[0].GetCompMark();
            min = entrants[0].GetCompMark();
            for (int i = 1; i < entrants.Length; i++)
            {
                if (entrants[i].GetCompMark() > max) max = entrants[i].GetCompMark();
                if (entrants[i].GetCompMark() < min) min = entrants[i].GetCompMark();
            }
        }
        static void SortEntrantsByPoints(ref Entrant[] entrants)
        {
            Array.Sort(entrants, (a, b) => {
                double markA = a.GetCompMark();
                double markB = b.GetCompMark();
                return markA > markB ? -1 : markA < markB ? 1 : 0;
            });
        }
        static void SortEntrantsByName(ref Entrant[] entrants)
        {
            Array.Sort(entrants, (a, b) => {
                int comp = String.Compare(a.Name, b.Name);
                double markA = a.GetCompMark();
                double markB = b.GetCompMark();
                return comp != 0 ? comp : markA > markB ? -1 : markA < markB ? 1 : 0;
            });
        }
        static void Main(string[] args)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)
                System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Console.Title = "Лабораторна робота №4";
            Console.SetWindowSize(100, 30);

            Entrant[] entrs = new Entrant[0];
            int n, i;
            string[] incorrectFirstOption = new string[] { "в", "вб", "п", "ск", "сп" };
            Entrant[] entrsToAdd;

            while (true)
            {
                Console.Write("-----------------\n" +
                    "Введіть дію\n" +
                    "[Д]одати абітурієнтів до кінця списку\n" +
                    "[Д]одати [с]тандартні дані списку\n" +
                    "[В]ивести список абітурієнтів\n" +
                    "[В]ивести [б]ільше інформації абітурієнта\n" +
                    "[П]оказати найвищий та найменший конкурсні бали\n" +
                    "[С]ортувати абітурієнтів за [к]онкурсними балами\n" +
                    "[С]ортувати абітурієнтів за [п]різвищем\n" +
                    ": ");
                string input = Console.ReadLine().ToLower();
                if (entrs.Length == 0 && Array.Exists(incorrectFirstOption, e => e == input))
                {
                    WriteLineError("Абітурієнтів не додано, будь ласка, додайте абітурієнтів.");
                    continue;
                }
                    
                switch (input)
                {
                    case "д":
                        Input("Введіть число абітурієнтів: ", out n);
                        Array.Resize(ref entrs, entrs.Length + n);
                        entrsToAdd = ReadEntrantsArray(n);
                        for (i = 0; i < n; i++)
                            entrs[entrs.Length - n + i] = entrsToAdd[i];
                        break;
                    case "дс":
                        Array.Resize(ref entrs, entrs.Length + 2);
                        entrsToAdd = new Entrant[2] {
                            new Entrant("Франко І.Я.", 12345, 10, 11.0, new ZNO[3]
                            {
                                new ZNO("Українська мова", 200),
                                new ZNO("Математика", 195),
                                new ZNO("Історія", 195),
                            }),
                            new Entrant("Шевченко Т.Г.", 12346, 10, 11.5, new ZNO[3]
                            {
                                new ZNO("Українська мова", 200),
                                new ZNO("Математика", 200),
                                new ZNO("Історія", 195),
                            }),
                        };
                        for (i = 0; i < 2; i++)
                            entrs[entrs.Length - 2 + i] = entrsToAdd[i];
                        break;
                    case "в":
                        PrintEntrants(entrs);
                        break;
                    case "вб":
                        Input("Введіть номер абітурієнта: ", out i);
                        PrintEntrant(entrs[i-1], true);
                        break;
                    case "п":
                        GetEntrantsInfo(entrs, out double max, out double min);
                        Console.WriteLine($"Найкращий конкурсний бал: {max}\nНайнижчий конкурсний бал: {min}");
                        break;
                    case "ск":
                        SortEntrantsByPoints(ref entrs);
                        break;
                    case "сп":
                        SortEntrantsByName(ref entrs);
                        break;
                    default:
                        WriteLineError("Помилка введення значення. " +
                            "Будь ласка, повторіть ще раз!");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
