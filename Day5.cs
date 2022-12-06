namespace adventofcode2022;

public class Day5 : IDay
{
    readonly IEnumerable<string> stackLines, moveLines;
    readonly int stacksCount;

    public Day5() {
        var input=Utils.GetInputLines(5);
        stackLines = input.TakeWhile(line=>!String.IsNullOrWhiteSpace(line));
        stacksCount = int.Parse(stackLines.Last().Trim().Split().Last());
        stackLines = stackLines.SkipLast(1);
        moveLines = input.SkipWhile(line=>!String.IsNullOrWhiteSpace(line)).Skip(1);
    }


    Stack<char>[] GetStacks()
    {
        var stacks = new Stack<char>[stacksCount];
        for (var i = 0; i < stacks.Length; i++)
            stacks[i] = new Stack<char>();
        foreach (var line in stackLines.Reverse())
        {
            for (var i = 0; i < stacks.Length; i++)
            {
                var c = line[1 + 4 * i];
                if (Char.IsLetter(c))
                    stacks[i].Push(c);
            }
        }
        return stacks;
    }

    abstract class Move
    {
        protected int count, from, to;

        public void Parse(string instruction)
        {
            var parts = instruction.Split();
            count = int.Parse(parts[1]);
            from = int.Parse(parts[3]);
            to = int.Parse(parts[5]);
        }

        public abstract void Apply(Stack<char>[] stacks);
    }

    class Move9000 : Move
    {
        public override void Apply(Stack<char>[] stacks)
        {
            for (var i = 0; i < count; i++)
            {
                stacks[to - 1].Push(stacks[from - 1].Pop());
            }
        }
    }

    class Move9001 : Move
    {
        public override void Apply(Stack<char>[] stacks)
        {
            var s = new Stack<char>();
            for (var i = 0; i < count; i++)
            {
                s.Push(stacks[from - 1].Pop());
            }
            while (s.Count > 0)
            {
                stacks[to - 1].Push(s.Pop());
            }
        }
    }

    IEnumerable<T> GetMoves<T>() where T : Move, new()
    {
        foreach (var line in moveLines)
        {
            var move = new T();
            move.Parse(line);
            yield return move;
        }
    }

    public object Part1()
    {
        var stacks = GetStacks();

        foreach (var move in GetMoves<Move9000>())
        {
            move.Apply(stacks);
        }

        return String.Concat(
                stacks.Select(
                    stack => stack.Peek()
                )
            );
    }

    public object Part2()
    {
        var stacks = GetStacks();

        foreach (var move in GetMoves<Move9001>())
        {
            move.Apply(stacks);
        }

        return String.Concat(
                stacks.Select(
                    stack => stack.Peek()
                )
            );
    }
}