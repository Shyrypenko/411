using System;
using System.Collections.Generic;

public class BookList
{
    private List<string> books = new List<string>();
    public string this[int index]
    {
        get => books[index];
        set => books[index] = value;
    }

    public void Add(string book) => books.Add(book);
    public bool Remove(string book) => books.Remove(book);
    public bool Contains(string book) => books.Contains(book);
    public static BookList operator +(BookList list, string book)
    {
        list.Add(book);
        return list;
    }
    public static BookList operator -(BookList list, string book)
    {
        list.Remove(book);
        return list;
    }

    public override string ToString() => string.Join("\n", books);
}