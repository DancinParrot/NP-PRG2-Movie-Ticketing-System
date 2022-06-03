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
    internal class Movie
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Classification { get; set; }
        public DateTime OpeningDate { get; set; }
        public List<string> GenreList { get; set; } = new List<string>();
        public List<Screening> ScreeningList { get; set; } = new List<Screening>();
        public Movie() { }
        public Movie(string title, int duration, string classification, DateTime openingDate, List<string> genreList)
        {
            Title = title;
            Duration = duration;
            Classification = classification;
            OpeningDate = openingDate;
            GenreList = genreList;
        }
        public void AddGenre(string genre)
        {
            GenreList.Add(genre);
        }
        public void AddScreening(Screening screening)
        {
            ScreeningList.Add(screening);
        }
        public override string ToString()
        {
            return "Title: " + Title + " Duration: " + Duration + " Classification: " + Classification + " Opening Date: " + OpeningDate.ToString("dd/MM/yyyy")
                + " Genre(s): " + String.Join("/", GenreList);
        }
    }
}
