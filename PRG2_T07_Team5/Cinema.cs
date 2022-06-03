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
    internal class Cinema
    {
        public string Name { get; set; }
        public int HallNo { get; set; }
        public int Capacity { get; set; }
        public Cinema() { }
        public Cinema(string name, int hallNo, int cap)
        {
            Name = name;
            HallNo = hallNo;
            Capacity = cap;
        }
        public override string ToString()
        {
            return "Cinema Name: " + Name + " Hall Number: " + HallNo + " Capacity: " + Capacity;
        }
    }
}
