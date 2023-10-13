using NLog;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");

string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
logger.Info(scrubbedFile);
MovieFile movieFile = new MovieFile(scrubbedFile);

logger.Info("Program ended");
bool optionISNOT1or2 = true;
while (optionISNOT1or2)
{
    Console.WriteLine("Enter 1 add a movie to the file");
    Console.WriteLine("Enter 2 to display all movies");
    Console.WriteLine("Enter anything else to quit.");

    string resp = Console.ReadLine();
    int maxid = 0;

    if (resp == "1") //add a new movie
    {
        Console.WriteLine("Enter movie title (year):");
        string newMovieTitle = Console.ReadLine();
        List<string> newMovieGenres = new List<string>();
        while(true)
        { 
            Console.WriteLine("Enter Enter genre (or done to quit):");
            string MovieGenre = Console.ReadLine();
            if (MovieGenre == "done")
                break;
                newMovieGenres.Add(MovieGenre);
        }
        Console.WriteLine("Enter movie director: ");
        string newMovieDirector = Console.ReadLine();
        Console.WriteLine("Enter movie runtime (h:m:s):");
        string newMovieRuntime = Console.ReadLine();

        //add entry to movies.csv
        string filePath = Directory.GetCurrentDirectory() + "\\movies.csv";
        StreamWriter sw = new StreamWriter(filePath, true);
        {
            sw.WriteLine($"{newMovieTitle}, {newMovieGenres}, {newMovieDirector}, {newMovieRuntime}");

        }
        sw.Close();
    }

else if (resp == "2")//read the movies.csv file
{
    string filePath = Directory.GetCurrentDirectory() + "\\movies.csv";
    if (File.Exists(filePath))
{
    StreamReader sr = new StreamReader(filePath);
    while (!sr.EndOfStream)
    {
        string line = sr.ReadLine();
        string[] segments = line.Split(',');
        Console.WriteLine($"{segments[0]}, {segments[1]}, {segments[2]}, {segments[3]}, {segments[4]}"); // display each movie attribute on one line

        if (int.TryParse(segments[0], out int id))
        {
            maxid = Math.Max(maxid, id);
        }
        else
        {
            logger.Warn("invalid ID in movies.csv file, file correct id number");
        }
    }
}
 else
        {
            Console.WriteLine("File doesn't exist");
        }
    }
else
    {
        optionISNOT1or2 = false;
    }
}