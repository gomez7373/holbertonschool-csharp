using System;

class Number
{
    public static int PrintLastDigit(int number)
    {
        int isLD = number % 10;
        if (isLD < 0)
            isLD *= -1;
        Console.Write(isLD);

        return isLD;
    }
}