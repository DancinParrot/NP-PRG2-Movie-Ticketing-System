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
    internal class Screening : IComparable<Screening>
    {
        public int ScreeningNo { get; set; }
        public DateTime ScreeningDateTime { get; set; }
        public string ScreeningType { get; set; }
        public int SeatsRemaining { get; set; }
        public Cinema Cinema { get; set; }
        public Movie Movie { get; set; }
        public Screening() { }
        public Screening(int screeningNo, DateTime screeningDateTime, string screeningType, Cinema cinema, Movie movie)
        {
            ScreeningNo = screeningNo;
            ScreeningDateTime = screeningDateTime;
            ScreeningType = screeningType;
            Cinema = cinema;
            Movie = movie;
        }
        public int CompareTo(Screening screening)
        {
            // Sort by Seats Remaining (Available Seats) in Descending Order
            return screening.SeatsRemaining.CompareTo(SeatsRemaining);
        }
        public override string ToString()
        {
            return "Screening Number: " + ScreeningNo + " Datetime: " + ScreeningDateTime.ToString("dd/MM/yyyy h:mmtt")
            + " Screening Type: " + ScreeningType + " Seats Remaining: " + SeatsRemaining;
        }
    }
}
