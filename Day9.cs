namespace adventofcode2022
{
    using Day9Items;

    namespace Day9Items
    {
        public readonly struct Vector2
        {
            public readonly int X { get; init; }
            public readonly int Y { get; init; }

            public Vector2(int x, int y)
            {
                X = x;
                Y = y;
            }

            public static Vector2 operator +(Vector2 a, Vector2 b)
                => new Vector2(a.X + b.X, a.Y + b.Y);

            public Vector2 Normalize() => new Vector2(Math.Sign(X), Math.Sign(Y));

            public override string ToString() => $"{X}, {Y}";
        }

        public class Rope
        {
            Vector2 head = new(0, 0);
            Vector2 tail = new(0, 0);
            Rope? next = null;
            HashSet<Vector2> visited = new(new Vector2[] { new(0, 0) });

            public int VisitedCount => visited.Count;

            public Rope()
            {

            }

            public Rope(int numberOfKnots)
            {
                if (numberOfKnots < 2) throw new ArgumentOutOfRangeException("Knots must be at least 2");
                var lastRope = this;
                for (var i = 2; i < numberOfKnots; i++)
                {
                    lastRope.next = new Rope();
                    lastRope = lastRope.next;
                }
            }

            public Rope GetLastRope()
            {
                var lastRope = this;
                while (lastRope.next != null)
                {
                    lastRope = lastRope.next;
                }
                return lastRope;
            }

            public void Move(IEnumerable<Vector2> moves)
            {
                foreach (var move in moves)
                {
                    HeadMove(move);
                }
            }

            void HeadMove(Vector2 move)
            {
                var normalizedMove = move.Normalize();
                for (var i = 0; i < Math.Abs(move.X) || i < Math.Abs(move.Y); i++)
                {
                    head += normalizedMove;
                    TailMove();
                }
            }

            void TailMove()
            {
                var diffX = Math.Abs(head.X - tail.X);
                var diffY = Math.Abs(head.Y - tail.Y);
                if ((diffX > 1 && diffY != 0) || (diffY > 1 && diffX != 0))
                {
                    // diagonal move
                    TailMoveX();
                    TailMoveY();
                }
                else if (diffY > 1)
                {
                    TailMoveY();
                }
                else if (diffX > 1)
                {
                    TailMoveX();
                }
                else
                {
                    // no need to move
                    return;
                }
                visited.Add(tail);
                if (next != null)
                {
                    next.head = tail;
                    next.TailMove();
                }
            }

            void TailMoveY()
            {
                if (tail.Y > head.Y)
                {
                    tail += new Vector2(0, -1);
                }
                else
                {
                    tail += new Vector2(0, 1);
                }
            }

            void TailMoveX()
            {
                if (tail.X > head.X)
                {
                    tail += new Vector2(-1, 0);
                }
                else
                {
                    tail += new Vector2(1, 0);
                }
            }

        }
    }

    public class Day9 : IDay
    {
        IEnumerable<Vector2> moves;

        public Day9() : this(Utils.GetInputLines(9)) { }
        public Day9(IEnumerable<string> input)
        {
            var movesList = new List<Vector2>();
            foreach (var line in input)
            {
                var tokens = line.Split();
                var (x, y) = tokens[0] switch
                {
                    "U" => (0, -1),
                    "R" => (1, 0),
                    "D" => (0, 1),
                    "L" => (-1, 0),
                    _ => throw new ArgumentOutOfRangeException()
                };
                var moveLength = int.Parse(tokens[1]);
                x *= moveLength;
                y *= moveLength;
                movesList.Add(new Vector2(x, y));
            }
            moves = movesList;
        }

        public object Part1()
        {
            var rope = new Rope();
            rope.Move(moves);
            return rope.VisitedCount;
        }

        public object Part2()
        {
            var rope = new Rope(10);
            var lastRope = rope.GetLastRope();
            rope.Move(moves);
            return lastRope.VisitedCount;
        }
    }
}