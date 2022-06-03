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
    abstract class Ticket
    {
        public Screening Screening { get; set; }
        public Ticket() { }
        public Ticket(Screening screening)
        {
            Screening = screening;
        }
        public abstract double CalculatePrice(Screening screening, Movie movie);
        public override string ToString()
        {
            return "screening: " + Screening;
        }
    }
}
