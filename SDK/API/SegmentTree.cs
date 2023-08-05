using SDK;
/// <summary>
/// 线段树:线段树是二叉树的一种,常常被用于求区间和与区间最大值等操作
/// </summary>
public class SegmentTree
{
    List<int> _orignalData = new List<int>();
    List<int?> _tree = new List<int?>();
    public SegmentTree ()
    {
        for (int i = 0; i < 1000; i++)
        {
            _tree.Add(null);
        }
    }


    public void Print ()
    {
        for (int i = 0; i < _tree.Count; i++)
        {
            if (_tree[i] == null)
            {
                continue;
            }
            API.Print(_tree[i]);
        }
    }


    public void Fill (List<int> data)
    {
        _orignalData = data;
        Fill(0, 0, _orignalData.Count - 1);
    }

    private void Fill (int node, int start, int end)
    {
        if (start == end)
        {
            _tree[node] = _orignalData[start];
        }
        else
        {
            int mid = (start + end) / 2;
            int leftNode = 2 * node + 1;
            int rightNode = 2 * node + 2;
            Fill(leftNode, start, mid);
            Fill(rightNode, mid + 1, end);
            _tree[node] = _tree[leftNode] + _tree[rightNode];
        }
    }

    public void Set (int index, int val)
    {
        SetValue(0, 0, _orignalData.Count - 1, index, val);
    }

    private void SetValue (int node, int start, int end, int index, int val)
    {
        if (start == end)
        {
            _orignalData[index] = val;
            _tree[node] = val;
        }
        else
        {
            int mid = (start + end) / 2;
            int leftNode = 2 * node + 1;
            int rightNode = 2 * node + 2;
            if (index >= start && index <= mid)
            {
                SetValue(leftNode, start, mid, index, val);
            }
            else
            {
                SetValue(rightNode, mid + 1, end, index, val);
            }
            _tree[node] = _tree[leftNode] + _tree[rightNode];
        }
    }


    public int? GetSum (int left, int right)
    {
        return Query(0, 0, _orignalData.Count - 1, left, right);
    }


    private int? Query (int node, int start, int end, int left, int right)
    {
        if (right < start || left > end)
        {
            return 0;
        }
        else if (left <= start && end <= right)
        {
            return _tree[node];
        }
        else if (start == end)
        {
            return _tree[node];
        }
        else
        {
            int mid = (start + end) / 2;
            int leftNode = 2 * node + 1;
            int rightNode = 2 * node + 2;
            int? sumLeft = Query(leftNode, start, mid, left, right);
            int? sumRight = Query(rightNode, mid + 1, end, left, right);
            return sumLeft + sumRight;
        }
    }
}