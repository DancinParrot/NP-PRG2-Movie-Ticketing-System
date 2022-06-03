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
    internal class Adult : Ticket
    {
        public bool PopcornOffer { get; set; } 
        public Adult() : base() { }
        public Adult(Screening screening, bool popcornOffer) : base(screening)
        {
            PopcornOffer = popcornOffer;
        }
        public override double CalculatePrice(Screening screening, Movie movie)
        {
            if (screening.ScreeningType == "2D")
            {
                if(screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Monday ||
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
            else if (screening.ScreeningType == "3D")
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
            return 0;
        }
        public override string ToString()
        {
            return "Popcorn Offer: " + PopcornOffer;
        }
    }
}
