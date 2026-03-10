using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructConsole
{
    struct Entrant
    {
        public string Name;
        public int IdNum;
        public int CoursePoints;
        public double AvgPoints;
        public ZNO[] ZNOResults;

        public Entrant(string name, int idNum, int coursePoints, double avgPoints, ZNO[] znoResults)
        {
            Name = name;
            IdNum = idNum;
            CoursePoints = coursePoints;
            AvgPoints = avgPoints;
            ZNOResults = znoResults;
        }

        public double GetCompMark()
        {
            if (ZNOResults.Length < 3) return 0;
            else return
                CoursePoints * 0.05 +
                AvgPoints * 0.10 +
                ZNOResults[0].Points * 0.25 +
                ZNOResults[1].Points * 0.40 +
                ZNOResults[2].Points * 0.20;
        }

        public string GetBestSubject()
        {
            if (ZNOResults.Length == 0) return "";
            string subject = ZNOResults[0].Subject;
            int max = ZNOResults[0].Points;
            for (int i = 1; i < ZNOResults.Length; i++)
            {
                if (ZNOResults[i].Points > max)
                {
                    subject = ZNOResults[i].Subject;
                    max = ZNOResults[i].Points;
                }
            }
            return subject;
        }

        public string GetWorstSubject()
        {
            if (ZNOResults.Length == 0) return "";
            string subject = ZNOResults[0].Subject;
            int min = ZNOResults[0].Points;
            for (int i = 1; i < ZNOResults.Length; i++)
            {
                if (ZNOResults[i].Points < min)
                {
                    subject = ZNOResults[i].Subject;
                    min = ZNOResults[i].Points;
                }
            }
            return subject;
        }
    }
}
