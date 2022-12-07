namespace adventofcode2022 {
    using Day7Items;

    namespace Day7Items {
        public abstract class Node {
            readonly string name;
            public string Name => name;

            public abstract int Size { get; }

            public DirNode? Parent { get; set; }

            public Node(string name) {
                this.name = name;
            }
        }

        public class FileNode : Node {
            readonly int size;
            public override int Size => size;

            public FileNode(string name, int size) : base(name) {
                this.size = size;
            }
        }

        public class DirNode : Node {
            Dictionary<string, Node> children = new();
            public Dictionary<string, Node> Children => children;

            public override int Size => children.Values.Sum(node => node.Size);

            public DirNode(string name) : base(name) {
            }

            public void Add(Node node) {
                node.Parent = this;
                children.Add(node.Name, node);
            }

            public DirNode? GetDir(string nodeName) {
                var node = children.GetValueOrDefault(nodeName);
                if (node is DirNode dirNode)
                    return dirNode;
                else
                    return null;
            }
        }
    }

    public class Day7 : IDay {
        DirNode root = new("/");
        HashSet<DirNode> directories = new();

        public Day7() : this(Utils.GetInputLines(7)) { }
        public Day7(IEnumerable<string> input) {
            Parse(input);
        }

        void Parse(IEnumerable<string> input) {
            DirNode cwd = root;
            directories.Add(root);
            foreach (var line in input) {
                var tokens = line.Split();
                if (tokens[0] == "$") {
                    // command
                    if (tokens[1] == "cd") {
                        cwd = tokens[2] switch {
                            "/" => root,
                            ".." => cwd.Parent ?? cwd, // cannot step up from root
                            var dirName => cwd.GetDir(dirName) ?? throw new NullReferenceException(),
                        };
                    } // ls command is ignored
                } else if (tokens[0] == "dir") {
                    // directory
                    var dir = new DirNode(tokens[1]);
                    cwd.Add(dir);
                    directories.Add(dir);
                } else {
                    // file
                    cwd.Add(new FileNode(tokens[1], int.Parse(tokens[0])));
                }
            }
        }

        public object Part1() {
            int sum = 0;
            foreach (var dir in directories) {
                if (dir.Size <= 100000) sum += dir.Size;
            }
            return sum;
        }

        const int TOTAL_SPACE = 70000000;
        const int FREE_SPACE_REQUIRED = 30000000;

        public object Part2() {
            int minSizeRequired = FREE_SPACE_REQUIRED - (TOTAL_SPACE - root.Size);
            int targetDirSize = directories
                .Select(dir => dir.Size)
                .Where(s => s >= minSizeRequired)
                .Min();
            return targetDirSize;
        }
    }
}