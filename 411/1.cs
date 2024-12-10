using System;

public class Matrix
{
    private int[,] elements;

    public int Rows { get; private set; }
    public int Columns { get; private set; }
    public int this[int row, int col]
    {
        get
        {
            if (row < 0 || row >= Rows || col < 0 || col >= Columns)
                throw new IndexOutOfRangeException("Invalid index.");
            return elements[row, col];
        }
        set
        {
            if (row < 0 || row >= Rows || col < 0 || col >= Columns)
                throw new IndexOutOfRangeException("Invalid index.");
            elements[row, col] = value;
        }
    }

    public Matrix(int rows, int columns)
    {
        if (rows <= 0 || columns <= 0)
            throw new ArgumentException("Matrix dimensions must be greater than zero.");
        Rows = rows;
        Columns = columns;
        elements = new int[rows, columns];
    }

    public Matrix(int[,] elements)
    {
        Rows = elements.GetLength(0);
        Columns = elements.GetLength(1);
        this.elements = (int[,])elements.Clone();
    }

    public int Max()
    {
        int max = elements[0, 0];
        foreach (int element in elements)
            max = Math.Max(max, element);
        return max;
    }

    public int Min()
    {
        int min = elements[0, 0];
        foreach (int element in elements)
            min = Math.Min(min, element);
        return min;
    }

    public static Matrix operator +(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns)
            throw new InvalidOperationException("Matrix dimensions must match for addition.");
        Matrix result = new Matrix(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < a.Columns; j++)
                result[i, j] = a[i, j] + b[i, j];
        return result;
    }

    public static Matrix operator -(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns)
            throw new InvalidOperationException("Matrix dimensions must match for subtraction.");
        Matrix result = new Matrix(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < a.Columns; j++)
                result[i, j] = a[i, j] - b[i, j];
        return result;
    }

    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.Columns != b.Rows)
            throw new InvalidOperationException("Matrix dimensions are incompatible for multiplication.");
        Matrix result = new Matrix(a.Rows, b.Columns);
        for (int i = 0; i < result.Rows; i++)
            for (int j = 0; j < result.Columns; j++)
                for (int k = 0; k < a.Columns; k++)
                    result[i, j] += a[i, k] * b[k, j];
        return result;
    }

    public static Matrix operator *(Matrix a, int scalar)
    {
        Matrix result = new Matrix(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < a.Columns; j++)
                result[i, j] = a[i, j] * scalar;
        return result;
    }

    public static bool operator ==(Matrix a, Matrix b) => a.Equals(b);
    public static bool operator !=(Matrix a, Matrix b) => !a.Equals(b);

    public override bool Equals(object obj)
    {
        if (obj is Matrix other && Rows == other.Rows && Columns == other.Columns)
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    if (this[i, j] != other[i, j])
                        return false;
            return true;
        }
        return false;
    }

    public override int GetHashCode() => elements.GetHashCode();

    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
                result += $"{elements[i, j]} ";
            result += "\n";
        }
        return result.TrimEnd();
    }
}