
public static void Reverse(int[] array) {
    if (array == null || array.Length == 0) {
        Console.WriteLine();
        return;
    }
    Array.Reverse(array);
    Console.WriteLine(string.Join(" ", array));
}

