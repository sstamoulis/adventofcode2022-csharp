namespace adventofcode2022
{
    using Day8Items;

    namespace Day8Items
    {
        using System.Collections.ObjectModel;

        public abstract class Grid
        {
            protected readonly ReadOnlyCollection<int> array;
            protected readonly int width;
            protected readonly int height;

            protected Grid(ReadOnlyCollection<int> array, int width)
            {
                this.array = array;
                this.width = width;
                this.height = array.Count / width;
            }

            protected int Get(int x, int y) => array[x + y * width];
        }

        public class TreesGrid : Grid
        {
            TreesGrid(ReadOnlyCollection<int> array, int width) : base(array, width) { }

            public static TreesGrid Parse(IEnumerable<string> input)
            {
                List<int> array = new();
                int width = input.First().Length;

                foreach (var line in input)
                {
                    foreach (var c in line)
                    {
                        array.Add(int.Parse(c.ToString()));
                    }
                }

                return new TreesGrid(array.AsReadOnly(), width);
            }

            public int VisibleCount()
            {
                var count = 0;
                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        if (IsVisible(x, y)) count++;
                    }
                }
                return count;
            }

            bool IsVisible(int x, int y)
            {
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    return true;

                var currentValue = Get(x, y);

                // check left
                var visible = true;
                for (var xo = x - 1; xo >= 0; xo--)
                {
                    if (Get(xo, y) >= currentValue)
                    {
                        visible = false;
                        break;
                    }
                }

                // check right
                if (!visible)
                {
                    visible = true;
                    for (var xo = x + 1; xo < width; xo++)
                        if (Get(xo, y) >= currentValue)
                        {
                            visible = false;
                            break;
                        }
                }

                // check up
                if (!visible)
                {
                    visible = true;
                    for (var yo = y - 1; yo >= 0; yo--)
                        if (Get(x, yo) >= currentValue)
                        {
                            visible = false;
                            break;
                        }
                }

                // check down
                if (!visible)
                {
                    visible = true;
                    for (var yo = y + 1; yo < height; yo++)
                        if (Get(x, yo) >= currentValue)
                        {
                            visible = false;
                            break;
                        }
                }

                return visible;
            }

            public ScoresGrid GetScores()
            {
                List<int> scores = new();



                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                    {
                        var tree = Get(x, y);
                        var score = 1;
                        var treesCount = 0;

                        // check up
                        treesCount = 0;
                        for (int yo = y - 1; yo >= 0; yo--)
                        {
                            treesCount++;
                            if (Get(x, yo) >= tree)
                                break;
                        }
                        score *= treesCount;

                        // check right
                        treesCount = 0;
                        for (int xo = x + 1; xo < width; xo++)
                        {
                            treesCount++;
                            if (Get(xo, y) >= tree)
                                break;
                        }
                        score *= treesCount;

                        // check down
                        treesCount = 0;
                        for (int yo = y + 1; yo < height; yo++)
                        {
                            treesCount++;
                            if (Get(x, yo) >= tree)
                                break;
                        }
                        score *= treesCount;

                        // check left
                        treesCount = 0;
                        for (int xo = x - 1; xo >= 0; xo--)
                        {
                            treesCount++;
                            if (Get(xo, y) >= tree)
                                break;
                        }
                        score *= treesCount;

                        scores.Add(score);
                    }

                return new ScoresGrid(scores.AsReadOnly(), width);
            }
        }

        public class ScoresGrid : Grid
        {
            public ScoresGrid(ReadOnlyCollection<int> array, int width) : base(array, width) { }

            public int Max() => array.Max();
        }
    }

    public class Day8 : IDay
    {
        TreesGrid treesGrid;

        public Day8() : this(Utils.GetInputLines(8)) { }
        public Day8(IEnumerable<string> input)
        {
            treesGrid = TreesGrid.Parse(input);
        }

        public object Part1()
        {
            return treesGrid.VisibleCount();
        }

        public object Part2()
        {
            var scoresGrid = treesGrid.GetScores();
            return scoresGrid.Max();
        }
    }
}