namespace Api.Common.Abstractions;

internal record ExceptionDetails(
    int Status,
    string Type,
    string Title,
    string Detail,
    IEnumerable<object>? Errors);
