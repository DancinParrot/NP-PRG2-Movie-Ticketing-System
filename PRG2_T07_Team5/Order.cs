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
    internal class Order
    {
        public int OrderNo { get; set; }
        public DateTime OrderDateTime { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public List<Ticket> TicketList { get; set; } = new List<Ticket>();
        public Order() { }
        public Order(int orderNo, DateTime orderDateTime)
        {
            OrderNo = orderNo;
            OrderDateTime = orderDateTime;
        }
        public void AddTicket(Ticket ticket)
        {
            TicketList.Add(ticket);
        }
        public override string ToString()
        {
            return "Order Number: " + OrderNo + "Order Date and Time: " + OrderDateTime;
        }
    }
}
