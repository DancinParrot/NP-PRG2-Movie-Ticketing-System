//============================================================
// Student Number : S10219390, S10219129
// Student Name : Tan Kai Zhe, Chuah Boon Chong
// Module Group : T07 
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace PRG2_T07_Team5
{
    internal class SeniorCitizen : Ticket
    {
        public int YearOfBirth { get; set; }
        public SeniorCitizen() : base() { }
        public SeniorCitizen(Screening screening, int yearOfBirth) : base(screening)
        {
            YearOfBirth = yearOfBirth;
        }
        public override double CalculatePrice(Screening screening, Movie movie)
        {
            var openingDate = movie.OpeningDate;
            var screeningDate = screening.ScreeningDateTime;
            var dateDiff = screeningDate - openingDate;

            if (screening.ScreeningType == "2D")
            {
                if (dateDiff.Days < 7)
                {
                    if (screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Monday ||
                    screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Tuesday ||
                    screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Wednesday ||
                    screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Thursday)
                    {
                        return 8.50;
                    }
                    else if (screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Friday ||
                        screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Saturday ||
                        screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        return 12.50;
                    }
                }
                else if (dateDiff.Days > 7)
                {
                    if (screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Monday ||
                        screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Tuesday ||
                        screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Wednesday ||
                        screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Thursday)
                    {
                        return 5.00;
                    }
                    else if (screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Friday ||
                             screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Saturday ||
                             screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        return 12.50;
                    }
                }
            }
            else if (screening.ScreeningType == "3D")
            {
                if (dateDiff.Days < 7)
                {
                    if (screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Monday ||
                    screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Tuesday ||
                    screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Wednesday ||
                    screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Thursday)
                    {
                        return 11;
                    }
                    else if (screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Friday ||
                        screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Saturday ||
                        screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        return 14;
                    }
                }
                else if (dateDiff.Days > 7)
                {
                    if (screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Monday ||
                        screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Tuesday ||
                        screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Wednesday ||
                        screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Thursday)
                    {
                        return 6.00;
                    }
                    else if (screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Friday ||
                             screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Saturday ||
                             screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        return 14;
                    }
                }
            }
            return 0;

        }
        public override string ToString()
        {
            return "Year of Birth: " + YearOfBirth;
        }
    }
}
