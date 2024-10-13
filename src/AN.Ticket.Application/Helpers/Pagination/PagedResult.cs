﻿namespace AN.Ticket.Application.Helpers.Pagination;
public class PagedResult<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}