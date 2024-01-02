namespace N90.Domain.Common.Caching;

/// <summary>
/// Represents an abstract base class for caching models.
/// </summary>
public abstract class CacheModel
{
    /// <summary>
    /// Gets the unique cache key associated with the cache model.
    /// </summary>
    public abstract string CacheKey { get; }
}
