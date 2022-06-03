//============================================================
// Student Number : S10219390, S10219129
// Student Name : Tan Kai Zhe, Chuah Boon Chong
// Module Group : T07 
//============================================================

using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

namespace PRG2_T07_Team5
{
    internal class Program
    {
        static int orderNo = 1;
        static void Main(string[] args)
        {
            List<Movie> movieList = new List<Movie>();
            List<Cinema> cinemaList = new List<Cinema>();
            List<Screening> screeningList = new List<Screening>();
            List<Order> orderList = new List<Order>();
            List<Order> popularMovieList = new List<Order>();

            int option = 0;

            while (true)
            {
                DisplayMenu();
                Console.Write("Enter an option: ");
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Format not accepted. Please enter an integer.");
                    continue;
                }

                if (option == 1)
                {
                    if (movieList.Count > 0 && cinemaList.Count > 0)
                    {
                        Console.WriteLine("Error: Movie and cinema data already loaded.");
                        continue;
                    }

                    LoadMovieCinemaData(movieList, cinemaList);
                }
                else if (option == 2)
                {
                    if (screeningList.Count > 0)
                    {
                        Console.WriteLine("Error: Screening data already loaded.");
                        continue;
                    }

                    LoadScreeningData(movieList, cinemaList, screeningList);
                }
                else if (option == 3)
                {
                    if (movieList.Count == 0)
                    {
                        Console.WriteLine("Error: Movie data not loaded. Please load the data before continuing.");
                        continue;
                    }

                    DisplayMovies(movieList);
                }
                else if (option == 4)
                {
                    if (movieList.Count == 0 && screeningList.Count == 0)
                    {
                        Console.WriteLine("Error: Movie and screening data not loaded. Please load the data before continuing.");
                        continue;
                    }

                    ListMovieScreenings(movieList, screeningList);
                }
                else if (option == 5)
                {
                    if (movieList.Count == 0 && screeningList.Count == 0 && cinemaList.Count == 0)
                    {
                        Console.WriteLine("Error: Movie, cinema, and screening data not loaded. Please load the data before continuing.");
                        continue;
                    }

                    AddMovieScreeningSession(movieList, screeningList, cinemaList);
                }
                else if (option == 6)
                {
                    if (screeningList.Count == 0)
                    {
                        Console.WriteLine("Error: Screening data not loaded. Please load the data before continuing.");
                        continue;
                    }

                    DeleteMovieScreeningSession(screeningList, movieList);
                }
                else if (option == 7)
                {
                    if (movieList.Count == 0 && screeningList.Count == 0)
                    {
                        Console.WriteLine("Error: Movie, cinema, and screening data not loaded. Please load the data before continuing.");
                        continue;
                    }

                    OrderMovieTickets(movieList, screeningList, orderList);
                }
                else if (option == 8)
                {
                    CancelOrder(orderList);
                }
                else if (option == 9)
                {
                    if (screeningList.Count == 0 && movieList.Count == 0)
                    {
                        Console.WriteLine("Error: Screening data not loaded. Please load the data before continuing.");
                        continue;
                    }
                    RecommendMovieWithSale(orderList, screeningList, movieList);
                }
                else if (option == 10)
                {
                    if (screeningList.Count == 0)
                    {
                        Console.WriteLine("Error: Screening data not loaded. Please load the data before continuing.");
                        continue;
                    }

                    DisplayScreeningDesc(screeningList);
                }
                else if (option == 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Option entered not within scope. Please enter an integer from 0 to 10.");
                }
            }
        }
        static void DisplayMenu()
        {
            Console.WriteLine("==========================================================================");
            Console.WriteLine("|                                   Menu                                 |");
            Console.WriteLine("==========================================================================");
            Console.WriteLine(" [1] Load Movie and Cinema Data");
            Console.WriteLine(" [2] Load Screening Data");
            Console.WriteLine(" [3] List all movies");
            Console.WriteLine(" [4] List movie screenings");
            Console.WriteLine(" [5] Add a movie screening session");
            Console.WriteLine(" [6] Delete a movie screening session");
            Console.WriteLine(" [7] Order movie ticket(s)");
            Console.WriteLine(" [8] Cancel order of ticket");
            Console.WriteLine(" [9] Recommend movie based on sale of tickets sold");
            Console.WriteLine(" [10] Display available seats of screening session in descending order");
            Console.WriteLine(" [0] Exit");
            Console.WriteLine("===========================================================================");
        }
        static void DisplayMovies(List<Movie> movieList)
        {
            Console.WriteLine("===================================================================================================");
            Console.WriteLine("|                                            Movies                                               |");
            Console.WriteLine("===================================================================================================");
            Console.WriteLine(" {0,-25} {1,-10} {2,-20} {3,-15} {4,-10}", "Title", "Duration", "Classification", "Opening Date", "Genre(s)");
            for (int i = 0; i < movieList.Count; i++)
            {
                Console.WriteLine(" {0,-25} {1,-10} {2,-20} {3,-15} {4,-10}", movieList[i].Title, movieList[i].Duration, movieList[i].Classification, movieList[i].OpeningDate.ToString("dd/MM/yyyy"), String.Join("/", movieList[i].GenreList));
            }
            Console.WriteLine("==================================================================================================");
        }
        static void DisplayMovieScreenings(Movie movie)
        {
            Console.WriteLine("===================================================================================================");
            Console.WriteLine("|                                       Screening Sessions                                        |");
            Console.WriteLine("===================================================================================================");
            Console.WriteLine(" {0,-25} {1,-30} {2,-20} {3,-15}", "Screening No", "DateTime", "Type", "Remaining Seats");

            foreach (Screening screening in movie.ScreeningList)
            {
                Console.WriteLine(" {0,-25} {1,-30} {2,-20} {3,-15}", screening.ScreeningNo, screening.ScreeningDateTime.ToString("dd/MM/yyyy h:mmtt"),
                    screening.ScreeningType, screening.SeatsRemaining);
            }
            Console.WriteLine("==================================================================================================");
        }
        static void DisplayCinema(List<Cinema> cinemaList)
        {
            // List Cinema Halls
            Console.WriteLine("====================================================================");
            Console.WriteLine("|                             Cinema Halls                         |");
            Console.WriteLine("====================================================================");
            Console.WriteLine(" {0,-25} {1,-30} {2,-20}", "Name", "Hall Number", "Capacity");
            foreach (Cinema cinema in cinemaList)
            {
                Console.WriteLine(" {0,-25} {1,-30} {2,-20}", cinema.Name, cinema.HallNo, cinema.Capacity);
            }
            Console.WriteLine("====================================================================");
        }
        static void LoadMovieCinemaData(List<Movie> movieList, List<Cinema> cinemaList)
        {
            // Lists are reference-type var, so the function will populate the lists with data
            try
            {
                using (StreamReader sr = new StreamReader("Movie.csv"))
                {
                    string s = sr.ReadLine(); // read the heading such that it is excluded from the list

                    // repeat until end of file
                    while ((s = sr.ReadLine()) != null)
                    {
                        // For each line create one movie object
                        string[] movieArr = s.Split(',');
                        string[] genreArr = movieArr[2].Split("/");
                        List<string> genreList = new List<string> { };
                        foreach (string genre in genreArr)
                        {
                            genreList.Add(genre);
                        }
                        Movie movie = new Movie(movieArr[0], Convert.ToInt32(movieArr[1]), movieArr[3], DateTime.ParseExact(movieArr[4], "dd/MM/yyyy", CultureInfo.InvariantCulture), genreList);
                        movieList.Add(movie);
                    }
                    Console.WriteLine("Loading of Movie Data is successful.");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: The file, Movie.csv, could not be found. Please try again.");
                Console.WriteLine("Loading of Movie Data is unsuccessful.");
            }
            try
            {
                using (StreamReader sr = new StreamReader("Cinema.csv"))
                {
                    string s = sr.ReadLine(); // read the heading such that it is excluded from the list

                    // repeat until end of file
                    while ((s = sr.ReadLine()) != null)
                    {
                        // For each line create one movie object
                        string[] cinemaArr = s.Split(',');

                        Cinema cinema = new Cinema(cinemaArr[0], Convert.ToInt32(cinemaArr[1]), Convert.ToInt32(cinemaArr[2]));
                        cinemaList.Add(cinema);
                    }
                    Console.WriteLine("Loading of Cinema Data is successful.");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: The file, Cinema.csv, could not be found. Please try again.");
                Console.WriteLine("Loading of Cinema Data is unsuccessful.");
            }
        }
        static void LoadScreeningData(List<Movie> movieList, List<Cinema> cinemaList, List<Screening> screeningList)
        {
            try
            {
                using (StreamReader sr = new StreamReader("Screening.csv"))
                {
                    string s = sr.ReadLine(); // read the heading such that it is excluded from the list
                    int i = 1001;
                    // repeat until end of file
                    while ((s = sr.ReadLine()) != null)
                    {
                        // For each line create one movie object
                        string[] screeningArr = s.Split(',');

                        // Search for correct cinema object
                        Cinema cinema = new Cinema();
                        Movie movie = new Movie();
                        foreach (Cinema c in cinemaList)
                        {
                            if (c.Name == screeningArr[2] && c.HallNo == Convert.ToInt32(screeningArr[3]))
                            {
                                cinema = c;
                            }
                        }
                        foreach (Movie m in movieList)
                        {
                            if (m.Title == screeningArr[4])
                            {
                                movie = m;
                            }
                        }
                        Screening screening = new Screening(i, DateTime.ParseExact(screeningArr[0], "dd/MM/yyyy h:mmtt", CultureInfo.InvariantCulture), screeningArr[1], cinema, movie);
                        foreach (Cinema c in cinemaList)
                        {
                            if (screening.Cinema.Equals(c))
                            {
                                screening.SeatsRemaining = c.Capacity;
                            }
                        }
                        screeningList.Add(screening);

                        // Add relevant screening sessions to its corresponding movie
                        foreach (Movie m in movieList)
                        {
                            if (m.Title == screening.Movie.Title)
                            {
                                m.AddScreening(screening);
                            }
                        }

                        i++;
                    }
                }
                Console.WriteLine("Loading of Screening Data is successful.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: The file, Screening.csv, could not be found. Please try again.");
                Console.WriteLine("Loading of Screening Data is unsuccessful.");
            }
        }
        static void ListMovieScreenings(List<Movie> movieList, List<Screening> screeningList)
        {
            DisplayMovies(movieList);

            // Prompt user to select a movie 
            // Input validation: add check for movie entered must be an existing movie
            bool isMovieExist = false;

            while (isMovieExist != true)
            {
                Console.Write("\nEnter your movie of choice: ");
                string movieInput = Console.ReadLine();

                // Retriev movie object, check by converting string to lowercase
                // In case, user did not input correctly
                Movie movieMatched = new Movie();
                foreach (Movie movie in movieList)
                {
                    if (movie.Title.ToLower() == movieInput.ToLower())
                    {
                        movieMatched = movie;
                        isMovieExist = true;
                    }
                }

                if (isMovieExist != true)
                {
                    Console.WriteLine("Error: The movie entered does not exist. Please try again.");
                    continue;
                }

                // Retrieve and display screening sessions
                // If no screening sessions for a movie (eg. Special Delivery), tell users
                if (movieMatched.ScreeningList.Count > 0)
                {
                    DisplayMovieScreenings(movieMatched);
                }
                else
                {
                    Console.WriteLine("No screening sessions are available for the selected movie.");
                }
            }
        }
        static void AddMovieScreeningSession(List<Movie> movieList, List<Screening> screeningList, List<Cinema> cinemaList)
        {
            // List all movies
            DisplayMovies(movieList);

            // User Input
            // Check if movie exists in movieList, otherwise prompt to enter again
            bool isMovieExist = false;
            string movieTitle = "";

            while (isMovieExist != true)
            {
                Console.Write("\nEnter movie title: ");
                movieTitle = Console.ReadLine();

                foreach (Movie m in movieList)
                {
                    if (m.Title.ToLower() == movieTitle.ToLower())
                    {
                        isMovieExist = true;
                    }
                }

                if (isMovieExist == false)
                {
                    Console.WriteLine("Error: The movie selected does not exist. Please try again.");
                }
            }

            // Input should only be 3D or 2D, if not, prompt user
            bool isScreeningTypeRight = false;
            string screeningType = "";

            while (isScreeningTypeRight != true)
            {
                Console.Write("Enter screening type (2D/3D): ");
                screeningType = Console.ReadLine();

                if (screeningType.ToUpper() != "3D" && screeningType.ToUpper() != "2D")
                {
                    Console.WriteLine("Error: Screening type not recognised. Please ensure to only enter 3D or 2D.");
                }
                else
                {
                    isScreeningTypeRight = true;
                }
            }

            // Input for Cinema and Cinema Hall Number
            string cinemaName = "";
            int cinemaHall = 0;
            DateTime screeningDate = new DateTime();
            DateTime screeningTime = new DateTime();
            DateTime screeningDateTime = new DateTime();



            while (true)
            {
                // Check if the entered date is after the selected movie's opening date
                // If not, prompt user to enter again
                bool isDateAfterOpening = false;

                while (isDateAfterOpening != true)
                {
                    Console.Write("Enter screening date (eg. 13/01/2022): ");
                    // Check correct format
                    try
                    {
                        screeningDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error: Date format not recognised. Please ensure to enter in the following format, \"dd/MM/yyyy\".");
                        continue;
                    }

                    // Check against movie opening date
                    foreach (Movie m in movieList)
                    {
                        if (movieTitle.ToLower() == m.Title.ToLower())
                        {
                            if (screeningDate >= m.OpeningDate)
                            {
                                isDateAfterOpening = true;
                            }
                            else
                            {
                                Console.WriteLine("Error: Date entered is before or equal the movie's opening date of " + m.OpeningDate.ToString("dd/MM/yyyy") +
                                    ". Please ensure the date entered is after or equal to the movie's opening date.");
                            }
                        }
                    }
                }

                // Check time format
                while (true)
                {
                    Console.Write("Enter screening time (eg. 8:00PM): ");

                    try
                    {
                        screeningTime = DateTime.ParseExact(Console.ReadLine(), "h:mmtt", CultureInfo.InvariantCulture);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error: Date format not recognised. Please ensure to enter in the following format, \"h:mmtt\".or \"12:00AM\"");
                        continue;
                    }

                    break;
                }
                // Join Date and Time into a single DateTime object
                screeningDateTime = screeningDate.Add(screeningTime.TimeOfDay);

                // List cinema halls
                DisplayCinema(cinemaList);

                bool isCinemaExist = false;
                bool isHallExist = false;
                // Check if Cinema Exists
                while (isCinemaExist != true)
                {
                    Console.Write("Enter the name of a cinema: ");
                    cinemaName = Console.ReadLine();

                    foreach (Cinema c in cinemaList)
                    {
                        if (c.Name.ToLower() == cinemaName.ToLower())
                        {
                            isCinemaExist = true;
                        }
                    }

                    if (isCinemaExist == false)
                    {
                        Console.WriteLine("Error: The cinema selected does not exist. Please try again.");
                    }
                }

                // Catch input format error and,
                // Check if the hall exists for the cinema entered
                while (isHallExist != true)
                {
                    Console.Write("Enter the hall number: ");
                    try
                    {
                        cinemaHall = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error: Format error. Please enter an integer.");
                        continue;
                    }

                    foreach (Cinema c in cinemaList)
                    {
                        if (c.Name.ToLower() == cinemaName.ToLower())
                        {
                            if (c.HallNo == cinemaHall)
                            {
                                isHallExist = true;
                            }
                        }
                    }

                    if (isHallExist == false)
                    {
                        Console.WriteLine("Error: The hall entered does not exist. Please try again.");
                    }
                }

                // Retrieve movie duration
                Movie movie = new Movie();
                foreach (Movie m in movieList)
                {
                    if (movieTitle == m.Title.ToLower())
                    {
                        movie = m;
                    }
                }

                int cleaningDuration = 30;
                Movie movieCompare = new Movie();

                // Make sure cinema hall, before and after duration of movie + cleaning time is clear,
                // otherwise, prompt user to enter again
                // Consider 30mins cleaning time, and movie duration (ensure no collisions) (30mins cleaning already included in one session)
                bool isHallAvailable = true;

                for (int i = 0; i < screeningList.Count; i++)
                {
                    // Filter sessions to same cinema and hall
                    if (screeningList[i].Cinema.Name.ToLower() == cinemaName.ToLower() && screeningList[i].Cinema.HallNo == cinemaHall)
                    {

                        // Retrieve screening movie duration
                        foreach (Movie m in movieList)
                        {
                            if (screeningList[i].Movie.Equals(m))
                            {
                                movieCompare = m;
                            }
                        }

                        if (screeningList[i].ScreeningDateTime <= screeningDateTime &&
                            screeningDateTime <= screeningList[i].ScreeningDateTime.AddMinutes(movieCompare.Duration + cleaningDuration))
                        {
                            // User input movie screening falls in between a pre-existing session, hence, false
                            isHallAvailable = false;
                        }
                    }
                }

                // Display status of moving screening session (eg. successful, unsuccessful)
                if (isHallAvailable == false)
                {
                    Console.WriteLine("Error: Collision between screening sessions, creation of Movie Screening Session is unsuccessful. Please try again.");

                }
                else
                {
                    Console.WriteLine("Creation of Movie Screening Session is successful.");
                    break;
                }
            }

            // Create a screening object and add to screeningList
            Cinema cinemaMatch = new Cinema();
            Movie movieMatch = new Movie();

            foreach (Cinema c in cinemaList)
            {
                if (c.Name.ToLower() == cinemaName.ToLower() && c.HallNo == cinemaHall)
                {
                    cinemaMatch = c;
                }
            }

            foreach (Movie m in movieList)
            {
                if (m.Title.ToLower() == movieTitle.ToLower())
                {
                    movieMatch = m;
                }
            }

            Screening newScreening = new Screening(screeningList[screeningList.Count - 1].ScreeningNo + 1, screeningDateTime, screeningType.ToUpper(), cinemaMatch, movieMatch);

            // Add initial seating capacity
            newScreening.SeatsRemaining = cinemaMatch.Capacity;

            // Add to screening list
            screeningList.Add(newScreening);

            // Add to the movie's screening list
            movieMatch.ScreeningList.Add(newScreening);


        }
        static void DeleteMovieScreeningSession(List<Screening> screeningList, List<Movie> movieList)
        {
            // Check if same as initial capacity (seatsRemaining), if yes, this indicates no tickets sold, as seats are the same as the initial capacity
            // If yes, meaning no tickets sold, add the screening session to a new list called screeningNoOrderList
            List<Screening> screeningNoOrderList = new List<Screening>();

            foreach (Screening screening in screeningList)
            {
                if (screening.SeatsRemaining == screening.Cinema.Capacity)
                {
                    screeningNoOrderList.Add(screening);
                }
            }

            // List all movie screening sessions that have not sold any tickets
            Console.WriteLine("===================================================================================================");
            Console.WriteLine("|                           Screening Sessions with no Tickets Sold                               |");
            Console.WriteLine("===================================================================================================");
            Console.WriteLine(" {0,-18} {1,-25} {2,-10} {3,-20} {4}", "Screening No", "DateTime", "Type", "Remaining Seats", "Movie");

            foreach (Screening screeningNoOrder in screeningNoOrderList)
            {
                Console.WriteLine(" {0,-18} {1,-25} {2,-10} {3,-20} {4}", screeningNoOrder.ScreeningNo, screeningNoOrder.ScreeningDateTime.ToString("dd/MM/yyyy h:mmtt"),
                                    screeningNoOrder.ScreeningType, screeningNoOrder.SeatsRemaining, screeningNoOrder.Movie.Title);
            }
            Console.WriteLine("==================================================================================================");

            // User input
            int sNoInput = 0;

            bool isSessionNoOrder = false;
            while (isSessionNoOrder != true)
            {
                try
                {
                    Console.Write("Enter a session: ");
                    sNoInput = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Format not accepted. Please enter an integer.");
                    continue;
                }

                // Check if session entered is in screeningNoOrderList
                foreach (Screening screeningNOrd in screeningNoOrderList)
                {
                    if (sNoInput == screeningNOrd.ScreeningNo)
                    {
                        isSessionNoOrder = true;
                    }
                }

                if (isSessionNoOrder == false)
                {
                    Console.WriteLine("Error: Either the screening session entered has tickets sold or that it does not exist. Please try again.");
                    Console.WriteLine("Status: Removal of screening session is unsuccessful.");
                }
            }

            // Remove movie screening from all screening lists
            Screening screeningDel = new Screening();

            foreach (Screening sNoOrder in screeningNoOrderList)
            {
                if (sNoInput == sNoOrder.ScreeningNo)
                {
                    screeningDel = sNoOrder;
                }
            }

            screeningList.Remove(screeningDel);
            
            foreach (Movie movie in movieList)
            {
                if (screeningDel.Movie.Equals(movie))
                {
                    movie.ScreeningList.Remove(screeningDel);
                }
            }

            Console.WriteLine("Status: Removal of screening session is successful.");
        }
        static void OrderMovieTickets(List<Movie> movieList, List<Screening> screeningList, List<Order> orderList)
        {
            double total = 0;
            //List all movies, prompt user to select a movie and list all movie screenings of the selected movie
            DisplayMovies(movieList);

            // Prompt user to select a movie 
            // Input validation: add check for movie entered must be an existing movie
            bool isMovieExist = false;

            while (isMovieExist != true)
            {
                Console.Write("\nEnter your movie of choice: ");
                string movieInput = Console.ReadLine();

                // Retriev movie object, check by converting string to lowercase
                // In case, user did not input correctly
                Movie movieMatched = new Movie();
                foreach (Movie movie1 in movieList)
                {
                    if (movie1.Title.ToLower() == movieInput.ToLower())
                    {
                        movieMatched = movie1;
                        isMovieExist = true;
                    }
                }

                if (isMovieExist != true)
                {
                    Console.WriteLine("Error: The movie entered does not exist. Please try again.");
                    continue;
                }

                // Retrieve and display screening sessions
                // If no screening sessions for a movie (eg. Special Delivery), tell users
                if (movieMatched.ScreeningList.Count > 0)
                {
                    DisplayMovieScreenings(movieMatched);
                    Screening screening = new Screening();
                    Movie movie = new Movie();

                    while (true)
                    {
                        try
                        {
                            //Retrieve the selected movie screening
                            bool isSessionNoOrder = false;
                            while (true)
                            {
                                try
                                {
                                    //Prompt user to select movie screening
                                    Console.Write("Enter the screening number of your choice: ");
                                    int selectedScreening = Convert.ToInt32(Console.ReadLine());
                                    //Check if session number is inside list
                                    foreach (Screening screeningNoList in screeningList)
                                    {
                                        if (selectedScreening == screeningNoList.ScreeningNo)
                                        {
                                            for (int i = 0; i < movieList.Count; i++)
                                            {
                                                Movie m = movieList[i];
                                                for (int x = 0; x < m.ScreeningList.Count; x++)
                                                {
                                                    Screening s = m.ScreeningList[x];
                                                    if (s.ScreeningNo == selectedScreening)
                                                    {
                                                        screening = s;
                                                        movie = m;
                                                    }
                                                }
                                            }
                                            while (true)
                                            {
                                                try
                                                {
                                                    //Prompt user to enter total number of tickets
                                                    Console.Write("Enter the total number of tickets you would like to purchase: ");
                                                    int tickets = Convert.ToInt32(Console.ReadLine());
                                                    //Check if figure entered is more than available seats
                                                    //Prompt user if all ticket holders meet the movie classification requirements
                                                    bool check = false;
                                                    if (tickets > screening.SeatsRemaining)
                                                    {
                                                        Console.WriteLine("There is insufficient seats available.");
                                                    }
                                                    else
                                                    {
                                                        while (true)
                                                        {
                                                            if (movie.Classification == "PG13")
                                                            {
                                                                Console.Write("This movie is restricted to persons aged 13 and above. Does all ticket holders meet the age " +
                                                                    "requirement? (Yes/No) ");
                                                                string confirmation = Console.ReadLine();
                                                                if (confirmation.ToLower() == "yes")
                                                                {
                                                                    check = true;
                                                                    break;
                                                                }
                                                                else if (confirmation.ToLower() == "no")
                                                                {
                                                                    check = false;
                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("Please only enter Yes or No. "); //checks if confirmation is either a yes or no
                                                                    continue;
                                                                }
                                                            }
                                                            else if (movie.Classification == "NC16")
                                                            {
                                                                Console.Write("This movie is restricted to persons aged 16 and above. Does all ticket holders meet the age " +
                                                                    "requirement? (Yes/No) ");
                                                                string confirmation = Console.ReadLine();
                                                                if (confirmation.ToLower() == "yes")
                                                                {
                                                                    check = true;
                                                                    break;
                                                                }
                                                                else if (confirmation.ToLower() == "no")
                                                                {
                                                                    check = false;
                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("Please only enter Yes or No. "); //checks if confirmation is either a yes or no
                                                                    continue;
                                                                }
                                                            }
                                                            else if (movie.Classification == "M18")
                                                            {
                                                                Console.Write("This movie is restricted to persons aged 18 and above. Does all ticket holders meet the age " +
                                                                    "requirement? (Yes/No) ");
                                                                string confirmation = Console.ReadLine();
                                                                if (confirmation.ToLower() == "yes")
                                                                {
                                                                    check = true;
                                                                    break;
                                                                }
                                                                else if (confirmation.ToLower() == "no")
                                                                {
                                                                    check = false;
                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("Please only enter Yes or No. "); //checks if confirmation is either a yes or no
                                                                    continue;
                                                                }
                                                            }
                                                            else if (movie.Classification == "R21")
                                                            {
                                                                Console.Write("This movie is restricted to persons aged 21 and above. Does all ticket holders meet the age " +
                                                                    "requirement? (Yes/No) ");
                                                                string confirmation = Console.ReadLine();
                                                                if (confirmation.ToLower() == "yes")
                                                                {
                                                                    check = true;
                                                                    break;
                                                                }
                                                                else if (confirmation.ToLower() == "no")
                                                                {
                                                                    check = false;
                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("Please only enter Yes or No. "); //checks if confirmation is either a yes or no
                                                                    continue;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                check = true;
                                                                break;
                                                            }
                                                        }


                                                        if (check == true)
                                                        {
                                                            Console.WriteLine("Please be informed that cinema staff would check your age and ticket again before the movie. ");
                                                            //Order object with status "Unpaid"
                                                            Order or = new Order();
                                                            or.Status = "Unpaid";
                                                            //Prompt user for responose depending on the type of ticket ordered
                                                            for (int i = 0; i < tickets; i++)
                                                            {
                                                                while (true)
                                                                {
                                                                    try
                                                                    {
                                                                        Console.WriteLine("[1] Student Ticket");
                                                                        Console.WriteLine("{2] Adult Ticket");
                                                                        Console.WriteLine("[3] Senior Citizen Ticket");
                                                                        Console.Write("Which ticket would you like to purchase?: ");
                                                                        int ticket = Convert.ToInt32(Console.ReadLine());
                                                                        if (ticket == 1)
                                                                        {
                                                                            while (true)
                                                                            {
                                                                                Console.Write("Please enter the level of study (Primary, Secondary, Tertiary): ");
                                                                                string level = Console.ReadLine();
                                                                                if (level.ToLower() == "primary" ||
                                                                                    level.ToLower() == "secondary" ||
                                                                                    level.ToLower() == "tertiary")
                                                                                {
                                                                                    //Ticket object
                                                                                    Student student = new Student(screening, level);
                                                                                    //add ticket object to ticket list of object
                                                                                    or.AddTicket(student);
                                                                                    //Update seats remaining for movie screening
                                                                                    screening.SeatsRemaining -= 1;
                                                                                    Console.WriteLine("Ticket Registered. ");
                                                                                    break;
                                                                                }
                                                                                else
                                                                                {
                                                                                    //ensures that level of study is entered correct;y.
                                                                                    Console.WriteLine("Invalid input. Please enter level of study according to options given.");
                                                                                    continue;
                                                                                }

                                                                            }
                                                                            break;

                                                                        }
                                                                        else if (ticket == 2)
                                                                        {
                                                                            while (true)
                                                                            {
                                                                                Console.Write("Would you like to purchase popocorn for $3? (Y/N): ");
                                                                                string ans = Console.ReadLine().ToLower();
                                                                                bool result = false;
                                                                                if (ans == "y")
                                                                                {
                                                                                    result = true;
                                                                                    //Ticket object
                                                                                    Adult adult = new Adult(screening, result);
                                                                                    //add ticket object to ticket list of object
                                                                                    or.AddTicket(adult);
                                                                                    //Update seats remaining for movie screening
                                                                                    screening.SeatsRemaining -= 1;
                                                                                    Console.WriteLine("Ticket registered. ");
                                                                                    break;
                                                                                }
                                                                                else if (ans == "n")
                                                                                {
                                                                                    result = false;
                                                                                    //Ticket object
                                                                                    Adult adult = new Adult(screening, result);
                                                                                    //add ticket object to ticket list of object
                                                                                    or.AddTicket(adult);
                                                                                    //Update seats remaining for movie screening
                                                                                    screening.SeatsRemaining -= 1;
                                                                                    Console.WriteLine("Ticket registered. ");
                                                                                    break;
                                                                                }
                                                                                else
                                                                                {
                                                                                    //ensures that choice is entered correctly.
                                                                                    Console.WriteLine("Invalid input. Please enter either Y or N");
                                                                                    continue;
                                                                                }
                                                                            }
                                                                            break;

                                                                        }
                                                                        else if (ticket == 3)
                                                                        {
                                                                            while (true)
                                                                            {
                                                                                try
                                                                                {
                                                                                    Console.Write("Please enter the year of birth (Must be 55 years and above): ");
                                                                                    int yob = Convert.ToInt32(Console.ReadLine());
                                                                                    int age = 2022 - yob;
                                                                                    if (age > 55)
                                                                                    {
                                                                                        //Ticket object
                                                                                        SeniorCitizen seniorcitizen = new SeniorCitizen(screening, yob);
                                                                                        //add ticket object to ticket list of object
                                                                                        or.AddTicket(seniorcitizen);
                                                                                        //Update seats remaining for movie screening
                                                                                        screening.SeatsRemaining -= 1;
                                                                                        Console.WriteLine("Ticket registered. ");
                                                                                        break;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        Console.WriteLine("You are not eligible to purchase Senior Citizen Ticket. ");
                                                                                        continue;
                                                                                    }
                                                                                }
                                                                                catch (FormatException)
                                                                                {
                                                                                    //ensures input is in integer.
                                                                                    Console.WriteLine("Invalid input. Please enter your year of birth.");
                                                                                }
                                                                            }
                                                                            break;

                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine("Invalid option. Please enter a valid ticket type."); //Ensures that user only chooses ticket type listed.
                                                                            continue;
                                                                        }
                                                                    }
                                                                    catch (FormatException)
                                                                    {
                                                                        Console.WriteLine("You have entered an invalid ticket type. Please try again!"); //Ensures that ticket type can only be integer
                                                                    }

                                                                }


                                                            }
                                                            foreach (Ticket ticket in or.TicketList)
                                                            {
                                                                total += ticket.CalculatePrice(screening, movie);
                                                                if (ticket is Adult)
                                                                {
                                                                    Adult a = (Adult)ticket;
                                                                    if (a.PopcornOffer is true)
                                                                    {
                                                                        total += 3;
                                                                    }
                                                                }
                                                            }

                                                            Console.WriteLine("Your total amount payable is: $" + total);
                                                            //Prompt user to press any key to make payment
                                                            Console.Write("Press any key to make payment. ");
                                                            Console.ReadKey();
                                                            Console.WriteLine("\n============= Receipt ============ ");
                                                            Console.WriteLine("Cinema Name: " + screening.Cinema.Name);
                                                            Console.WriteLine("Hall Number: " + screening.Cinema.HallNo);
                                                            Console.WriteLine("Total amount paid: $" + total);
                                                            Console.WriteLine("Your order number is {0}. Thank you for visiting {1}, hope to see you again!",
                                                                orderNo, screening.Cinema.Name);
                                                            or.Status = "Paid";
                                                            or.OrderNo = orderNo;
                                                            orderNo += 1;
                                                            orderList.Add(or);
                                                            break;

                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Sorry you are not allowed to watch this movie. Please choose another movie! ");
                                                            //restricts customers who do not meet the age classification of movie
                                                        }

                                                    }
                                                    break;
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Error: Please enter the number of tickets in digits."); //Ensures that number of tickets can only be integer
                                                }
                                            }
                                            isSessionNoOrder = true;
                                            break;
                                        }
                                    }

                                    if (isSessionNoOrder is false)
                                    {
                                        Console.WriteLine("This screening session does not exist or the tickets have been sold out. "); //Informs user that screening session is not found
                                    }
                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Error: Please enter an integer"); //Ensures that an integer must be entered
                                    continue;
                                }

                            }
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("You have entered an invalid screening number, please try again!"); //Ensures that screening number can only be integer
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No screening sessions are available for the selected movie.");
                }
            }
        }
        static void CancelOrder(List<Order> orderList)
        {
            bool refundStat = false;
            while (true)
            {
                try
                {
                    //Checks if any order had been made yet
                    if (orderList.Count > 0)
                    {
                        //Prompt user for order number
                        Console.Write("Please enter your order number: ");
                        int orderNumber = Convert.ToInt32(Console.ReadLine());
                        //check if order number is valid or not
                        if (orderNumber <= 0 || orderNumber >= 100)
                        {
                            Console.Write("The order number you have entered does not exist. ");
                        }
                        else
                        {
                            for (int i = 0; i < orderList.Count; i++)
                            {

                                Order o = orderList[i];
                                //Retrieve selected order
                                if (o.OrderNo == orderNumber)
                                {
                                    if (o.Status.ToLower() == "cancelled")
                                    {
                                        Console.WriteLine("This order has already been cancelled. ");
                                        refundStat = true; //so order not found would not be printed again
                                    }
                                    else
                                    {
                                        Screening screening = o.TicketList[0].Screening;
                                        //Check if screening has been screened
                                        if (DateTime.Now > screening.ScreeningDateTime)
                                        {
                                            Console.WriteLine("We are unable to refund your ticket after the movie has been screened. ");
                                            break;
                                        }
                                        else
                                        {
                                            for (int x = 0; x < o.TicketList.Count; x++)
                                            {
                                                //Update seats remaining for screening
                                                screening.SeatsRemaining += 1;
                                            }
                                            //Change order status to cancelled
                                            o.Status = "Cancelled";
                                            //Message that implies status of cancellation and payment refund
                                            Console.WriteLine("Your order number {0} has been cancelled successfully, your payment has been refunded to you.", o.OrderNo);
                                            refundStat = true;
                                            break;

                                        }
                                    }
                                }
                            }

                            if (refundStat is false)
                            {
                                //After checking if order exists inside order list
                                Console.WriteLine("Your order number could not be found, please try again!");
                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("No orders have been made yet. ");
                    }

                    break;
                }
                catch (FormatException)
                {
                    //Checks if order number inputted is an integer, if its not, program prompts user
                    Console.WriteLine("Invalid input. Please enter an integer of your order number. ");
                }
            }

        }
        static void DisplayScreeningDesc(List<Screening> screeningList)
        {
            // Descending order -> Big to small
            Console.WriteLine("===================================================================================================");
            Console.WriteLine("|                   Available Seats of Screening Session in Descending Order                      |");
            Console.WriteLine("===================================================================================================");
            Console.WriteLine(" {0,-15} {1,-25} {2,-10} {3,-20} {4}", "Screening No", "DateTime", "Type", "Remaining Seats", "Movie Title");

            // Sort with IComparable interface in Screening.cs
            screeningList.Sort();

            foreach (Screening screening in screeningList)
            {
                Console.WriteLine(" {0,-15} {1,-25} {2,-10} {3,-20} {4}", screening.ScreeningNo, screening.ScreeningDateTime.ToString("dd/MM/yyyy h:mmtt"),
                    screening.ScreeningType, screening.SeatsRemaining, screening.Movie.Title);
            }
            Console.WriteLine("==================================================================================================");
        }
        static void RecommendMovieWithSale(List<Order> orderList, List<Screening> screeningList, List<Movie> movieList)
        {
            // link how many tickets sold to their respective movies 
            // use dictionary (https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-6.0) 
            Dictionary<Movie, int> salesRanking = new Dictionary<Movie, int>();

            if (orderList.Count == 0)
            {
                Console.WriteLine("No ticket has been sold yet. Please check again at a later time.");
            }
            else
            {
                // Populate dictionary with movie data 
                foreach (Movie m in movieList)
                {
                    salesRanking.Add(m, 0);
                }

                foreach (Order order in orderList)
                {
                    foreach (Ticket ticket in order.TicketList)
                    {
                        foreach (Movie movie in movieList)
                        {
                            if (ticket.Screening.Movie.Equals(movie))
                            {
                                // If the same movie, add one count to dictionary 
                                salesRanking[movie] += 1;
                            }
                        }
                    }
                }

                // Get the movie with the most number of tickets sold (recommended) 
                KeyValuePair<Movie, int> recommendedMovie = new KeyValuePair<Movie, int>();

                foreach (var kvp in salesRanking)
                {
                    if (kvp.Value > recommendedMovie.Value)
                    {
                        recommendedMovie = kvp;
                    }
                }
                Console.WriteLine("The recommended movie based on sale of tickets sold is {0}.", recommendedMovie.Key.Title);
            }
        }
    }
}
