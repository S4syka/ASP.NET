using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures;

public abstract class BaseRequestParameters
{
    public const int MAX_PAGE_SIZE= 50;
    public int PageNumber { get; set; }
    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
    }
}