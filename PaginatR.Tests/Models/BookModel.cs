namespace PaginatR.Tests.Models;

internal record BookModel(
    string Title,
    AuthorModel Author,
    IEnumerable<string> Chapters,
    int Reviews
);
