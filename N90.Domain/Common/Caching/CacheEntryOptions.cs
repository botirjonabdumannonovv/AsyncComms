namespace N90.Domain.Common.Caching;

/// <summary>
/// Represents options for caching an entry.
/// </summary>
public class CacheEntryOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CacheEntryOptions"/> class.
    /// </summary>
    public CacheEntryOptions()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CacheEntryOptions"/> class with specified expiration options.
    /// </summary>
    /// <param name="absoluteExpirationRelativeToNow">The absolute expiration relative to the current time.</param>
    /// <param name="slidingExpiration">The sliding expiration duration.</param>
    public CacheEntryOptions(TimeSpan? absoluteExpirationRelativeToNow, TimeSpan? slidingExpiration)
    {
        AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
        SlidingExpiration = slidingExpiration;
    }

    /// <summary>
    /// Gets or sets the absolute expiration relative to the current time.
    /// </summary>
    public TimeSpan? AbsoluteExpirationRelativeToNow { get; init; }

    /// <summary>
    /// Gets or sets the sliding expiration duration.
    /// </summary>
    public TimeSpan? SlidingExpiration { get; init; }
}
