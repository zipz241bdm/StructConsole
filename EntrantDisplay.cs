using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructConsole
{
    static class EntrantDisplay
    {
        public static void PrintEntrant(Entrant entr, bool isExpanded = false)
        {
            Console.WriteLine($"Прізвище та ініціали абітурієнта: {entr.Name}");
            Console.WriteLine($"Ідентифікаційний код абітурієнта: {entr.IdNum}");
            Console.WriteLine($"Бали за підготовчі курси: {entr.CoursePoints}");
            Console.WriteLine($"Бал атестату: {entr.AvgPoints}");
            Console.WriteLine("Предмети ЗНО: ");
            for (int i = 0; i < entr.ZNOResults.Length; i++)
            {
                Console.WriteLine($"Назва предмета №{i + 1}: {entr.ZNOResults[i].Subject}");
                Console.WriteLine($"Оцінка предмета №{i + 1}: {entr.ZNOResults[i].Points}");
            }
            if (isExpanded)
            {
                Console.WriteLine($"Конкурсний бал: {entr.GetCompMark()}");
                Console.WriteLine($"Найкращий бал з предмету: {entr.GetBestSubject()}");
                Console.WriteLine($"Найгірший бал з предмету: {entr.GetWorstSubject()}");
            }
        }
        public static void PrintEntrants(Entrant[] entrants)
        {
            foreach (Entrant entr in entrants)
            {
                PrintEntrant(entr);
                Console.WriteLine();
            }
        }
    }
}
