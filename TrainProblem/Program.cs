// Вы находитесь в пустом поезде. Это даже не поезд, а просто вагоны, они сцеплены друг с другом.
// Все вагоны внутри одинаковы, двери на выход из вагона закрыты, через окна ничего не видно.
// Вы можете включать и выключать свет в вагоне, в котором находитесь, можете сходить в соседний вагон, там тоже можно включать или выключать свет.
// Вам известно, что вагоны стоят на кольце и сами сцеплены в кольцо, первый вагон сцеплен с последним, ходить по кругу можно сколько угодно.
// В момент начала решения задачи в каких-то вагонах свет уже горит, в каких-то — не горит.
// Ваша задача при помощи управления светом в вагонах и перемещения по ним узнать сколько в этом кольце вагонов.

// so the original approach was to use a doubly linked list to move back and forward.
// But it's actually not required, because we can just reference 2 elements so, no moving is required

Train train = new Train(157);

Console.WriteLine(Solve(train, 3));

// Solving func, has a startIndex, so we can basically start in any cabin
int Solve(Train train, int startIndex)
{
    // index of cabin, by design there are at least 2 of them
    int rightIndex = startIndex + 1;

    // get the initial cabin
    var startCabin = train.LoopGet(startIndex);

    while (true)
    {
        // remember the initial cabin flag
        bool startCabinIsLightOn = startCabin.IsLightOn;

        // go to the N'th cabin
        var rightCabin = train.LoopGet(rightIndex);

        // switch the lights
        rightCabin.IsLightOn = !rightCabin.IsLightOn;

        // return back to the initial and check for light difference
        // if the light switched - we made a loop
        if (startCabinIsLightOn != startCabin.IsLightOn)
        {
            return rightIndex - startIndex;
        }

        rightIndex++;
    }
}

public class Cabin
{
    public bool IsLightOn { get; set; }
}

public class Train
{
    private readonly Cabin[] _cabins;

    /// <summary>
    /// Basic constructor, count is needed to initialize a train
    /// </summary>
    public Train(int count)
    {
        if (count < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Can't create a train with less than 2 cabins");
        }
        
        // generate cabins
        _cabins = Enumerable.Range(0, count).Select(i => new Cabin()).ToArray();
    }

    /// <summary>
    /// Looping cabin accessor, allows to continuously get cabins, regardless of array indices
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Cabin LoopGet(int index)
    {
        return _cabins[index % _cabins.Length];
    }
}