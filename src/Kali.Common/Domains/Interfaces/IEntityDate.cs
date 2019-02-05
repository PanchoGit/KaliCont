using System;

namespace Kali.Common.Domains.Interfaces
{
    public interface IEntityDate
    {
        DateTimeOffset? Created { get; set; }

        DateTimeOffset? Modified { get; set; }
    }
}