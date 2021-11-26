using System;
using System.Collections.Generic;

namespace movie_theatre
{

	class Movie {
		public string Movie_Name { get; set; }
		public string Movie_Rating { get; set; }

	}

    class Program
    {
		public static List<Movie> Movie_List = new List<Movie>();

		static bool ValidateUserNumberInput(string userInput, int max = 150)
		{
			bool isValid = false;

			int x;

			if (int.TryParse(userInput, out x) && x > 0 && x <= max)
			{
				isValid = true;
			}

			return isValid;
		}

		public static Dictionary<string, int> Movie_Ratings = new Dictionary<string, int>(){
			{"G", 0},
			{"PG", 10},
			{"PG-13", 13},
			{"R", 15},
			{"NC-17", 17}
		};

        static void Main(string[] args)
        {

            home();

        }

        static void home()
        {
            Console.Clear();
            Console.WriteLine("\t\t*************************************");
            Console.WriteLine("\t\t*** Welcome to MoviePlex Theatre ***");
            Console.WriteLine("\t\t*************************************\n");
            UserCheck();
        }

        static void UserCheck()
        {

			while(true){
				try{
				Console.WriteLine("Please Select From Following Options:");

            	Console.WriteLine("1: Administrator");
            	Console.WriteLine("2: Guests\n");

            	Console.WriteLine("Selection:");
            	string UserInput = Console.ReadLine();

				var isValid = ValidateUserNumberInput(UserInput, 2);

				if(!isValid){
					throw new ArgumentException("Please enter a valid number from 1 - 2");
				}

				if (UserInput == "1")
				{
					string adminPassword = "Movieplex@123";
					int i = 5;
					int status = 1;


					while (i >= 1)
					{
						Console.WriteLine("Please Enter the Admin Password: ");
						string adminPwd = Console.ReadLine();
						i--;


						if (adminPassword == adminPwd)
						{
							Console.WriteLine("Password entered is correct");
							status = 0;
							break;

						}
						else if (adminPwd == "B")
						{
							Console.WriteLine("Redirecting to previous screen");
							home();
						}

						else
						{

							Console.WriteLine("You entered an Invalid password.");
							Console.WriteLine("You have {0} more attempts to enter correct password OR Press B to go back to previous screen", i);

						}

						if (status == 0 || i == 0)
							break;
					}

					if (status == 0)
						admin();

					if (i == 0)
						home();


				}
				else if (UserInput == "2"){
					guest();
				}

				}catch(Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}

        }

        static void admin()
        {
            Console.Clear();
			Movie_List.Clear();
            int maximum_allowed_movies = 10;
			int showing_movie_count = 0;
            var amountWords = new[] { "zero", "First", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh", "Eighth", "Ninth", "Tenth" };
			var allowed_movie_ratings = new List<string>(Movie_Ratings.Keys);

            Console.WriteLine("Welcome Movieplex Administrator");

			while(true){

				try{

				Movie_List.Clear();

				Console.WriteLine("How many movies are playing today? :");

				var userInput = Console.ReadLine();

				var isValid = ValidateUserNumberInput(userInput, maximum_allowed_movies);

				if(!isValid){
					throw new ArgumentException("Please enter a valid number from 1 - 10");
				}

				showing_movie_count = Int32.Parse(userInput);				

				Console.WriteLine("You have entered a number");

				for (int i = 0; i < showing_movie_count; i++)
				{
					Console.WriteLine("Enter {0} Movie Name: ", amountWords[i + 1]);
					string movieName = Console.ReadLine();

					bool is_duplicate_movie = Movie_List.Exists(x => x.Movie_Name == movieName);

					if(is_duplicate_movie){
						throw new ArgumentException("Movie name already exists");
					}

					Console.WriteLine("Enter Movie Rating: ");
					string movieRating = Console.ReadLine();

					var isValidNumber = ValidateUserNumberInput(movieRating, 120);

					if(!allowed_movie_ratings.Contains(movieRating) && !isValidNumber){
						throw new ArgumentOutOfRangeException("Please add an appropriate movie rating\n");
					}

					Movie movie = new Movie();
					movie.Movie_Name = movieName;
					movie.Movie_Rating = movieRating;

					Movie_List.Add(movie);

				}

				for (int j = 0; j < showing_movie_count; j++)
            	{
               		Console.WriteLine("{0}. {1} {{ {2} }}", j + 1, Movie_List[j].Movie_Name, Movie_List[j].Movie_Rating);
				}

				accept_movie_list();

				}catch (Exception ex) {
					Console.WriteLine(ex.Message);

				}
			}

        }

		static void accept_movie_list()
		{
			string response;
			
			while (true)
			{
				try{
					Console.WriteLine("Your Movies Playing Today Are Listed Above. Are you satisfied? (Y/N)? ");
							
					response = Console.ReadLine();

					if (response.ToUpper() == "Y")
						home();
					else if (response.ToUpper() == "N"){
						Movie_List.Clear();
						admin();
					}
					else
						throw new ArgumentException("Please enter y or n to continue");
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}


		static void guest()
		{
		    Console.Clear();
            Console.WriteLine
			("\t\t*************************************");
            Console.WriteLine("\t\t*** Welcome to Movieplex Theatre ***");
            Console.WriteLine("\t\t*************************************\n");
			int movie_choice_index = 0;
			int age = 0;
			Movie selected_movie;
			string end_response;

			if(Movie_List.Count <= 0){
				Console.WriteLine("THERE ARE NO MOVIES PLAYING TODAY!, PLEASE CHECK BACK LATER!\n");
				Console.WriteLine("Please press any button to go back home");
				Console.ReadKey();
				home();
			} else {
			while (true) {

				Console.WriteLine("There are {0} movies playing today. Please choose from the following movies:", Movie_List.Count);

				for(int i = 0; i < Movie_List.Count; i++){
					Console.WriteLine("{0}. {1} {{ {2} }}", i + 1, Movie_List[i].Movie_Name, Movie_List[i].Movie_Rating);
				}

				try{
					Console.WriteLine("Which Movie Would You Like To Watch: ");

					var userInput = Console.ReadLine();

					var isValid = ValidateUserNumberInput(userInput, Movie_List.Count);

					if(!isValid){
						throw new ArgumentException(String.Format("Please enter a valid number from 1 - {0}", Movie_List.Count));
					}

					movie_choice_index = Int32.Parse(userInput);

					if((movie_choice_index - 1) > Movie_List.Count){
						throw new ArgumentOutOfRangeException(String.Format("Please enter a valid number\n", Movie_List.Count));
					}

					selected_movie = Movie_List[movie_choice_index - 1];

					Console.WriteLine("Please Enter Your Age For Verification: ");

					var age_input = Console.ReadLine();

					var age_is_valid = ValidateUserNumberInput(age_input, 120);

					if(!age_is_valid){
						throw new ArgumentException("Please enter a valid age from 0 - 120");
					}

					age = Int32.Parse(age_input);

					var is_movie_rating_number = Int32.TryParse(selected_movie.Movie_Rating, out int n);

					if(is_movie_rating_number){
						if(age < n){
						Console.WriteLine("Welcome");
						throw new ArgumentException("You are under the age limit for this movie, please pick another\n");
						}
					} else {
						if(age < Movie_Ratings[selected_movie.Movie_Rating]){
						Console.WriteLine("This is the one");
						throw new ArgumentOutOfRangeException("You are under the age limit for this movie, please pick another\n");
						}
					}

					Console.WriteLine("Enjoy The Movie!");

					Console.WriteLine("Press M to go back to the Guest Main Menu.\nPress S to go back to the start Page");

					end_response = Console.ReadLine().ToUpper();

					if(end_response == "M"){
						guest();
					}
					else if(end_response == "S"){
						home();
					}
					else{
						Console.WriteLine("Invalid Entry");
						guest();
					}

					break;
				}
				catch (Exception ex) {
					Console.WriteLine(ex.Message);

				}

				
			}
			}
		}

    }
}
