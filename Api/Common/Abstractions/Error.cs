﻿namespace Api.Common.Abstractions;

public record Error(string Code, string Name)
{
    public static Error None = new(string.Empty, string.Empty);

    public static Error NullValue = new("error.NullValue", "Null value was provided");


}
