namespace Randomizer.Interfaces
{
    interface IAlgorithm
    {
        double Next();
        int Next(int to);
        int Next(int from, int to);
    }
}
