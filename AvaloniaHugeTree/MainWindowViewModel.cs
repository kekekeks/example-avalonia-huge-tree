namespace AvaloniaHugeTree
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private bool _recountQueued = false;
        public RootTreeNodeModel TreeRoot { get; }
        public int TotalItems { get; set; }

        public MainWindowViewModel()
        {
            var root = new RootTreeNodeModel()
            {
                Name = "Root",
                IsExpanded = true
            };
            Populate(root, 5);
            root.ForceResync();
            TreeRoot = root;
            TotalItems = Count(root);
        }

        int Count(TreeNodeModel node)
        {
            int cnt = 1;
            foreach (var ch in node.Children) 
                cnt += Count(ch);
            return cnt;
        }

        void Populate(TreeNodeModel node, int levels)
        {
            if (levels == 0)
                return;
            for (var c = 0; c < 10; c++)
            {
                var ch = new TreeNodeModel
                {
                    Name = "Child " + c,
                    IsExpanded = levels>1
                };
                if (levels == 1)
                    SetupAutoAdd(ch);
                node.AddChild(ch);
                Populate(ch, levels - 1);
            }
        }

        private void SetupAutoAdd(TreeNodeModel node)
        {
            node.PropertyChanged += (s, e) =>
            {
                if (node.IsExpanded && node.Children.Count == 0)
                {
                    for (var c = 0; c < 20; c++)
                    {
                        var ch = new TreeNodeModel
                        {
                            Name = "Auto-added child " + c
                        };
                        SetupAutoAdd(ch);
                        node.AddChild(ch);
                    }

                    TotalItems += 20;
                    RaisePropertyChanged(nameof(TotalItems));
                }
            };
        }
    }
}