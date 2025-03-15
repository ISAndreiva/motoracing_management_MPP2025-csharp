using ConcursMotociclism.domain;
using ConcursMotociclism.Repository.Db;

namespace ConcursMotociclism;

public static class Program
{
    public static void Main(string[] args)
    {
        var userDbRepository = new UserDbRepository();
        var teamDbRepository = new TeamDbRepository();
        var raceDbRepository = new RaceDbRepository();
        var racerDbRepository = new RacerDbRepository();
        var raceRegistrationDbRepository = new RaceRegistrationDbRepository();

        foreach (var VARIABLE in raceDbRepository.GetRacesByClass(250))
        {
            Console.WriteLine(VARIABLE.RaceName + " " + VARIABLE.RaceClass);
        }
    }
}