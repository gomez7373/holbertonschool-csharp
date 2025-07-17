public class Queue<T>
{
    public class Node
    {
        public T value;
        public Node next;

        public Node(T value)
        {
            this.value = value;
            this.next = null;
        }
    }

    private Node head;
    private Node tail;
    private int count = 0;

    public void Enqueue(T value) { /* ... */ }

    public int Count() { return count; }

    public string CheckType() // ¡aún debes mantenerlo!
    {
        return typeof(T).ToString();
    }
}

