using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructConsole
{
    struct Entrant
    {
        private const double CoursePointsWeight = 0.05;
        private const double AvgPointsWeight = 0.10;
        private const double PrimarySubjectWeight = 0.25;
        private const double SecondarySubjectWeight = 0.40;
        private const double TertiarySubjectWeight = 0.20;

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
            return
                CoursePoints * CoursePointsWeight +
                AvgPoints * AvgPointsWeight +
                ZNOResults[0].Points * PrimarySubjectWeight +
                ZNOResults[1].Points * SecondarySubjectWeight +
                ZNOResults[2].Points * TertiarySubjectWeight;
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
