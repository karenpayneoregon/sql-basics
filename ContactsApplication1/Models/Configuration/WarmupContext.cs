namespace ContactsApplication1.Models.Configuration;

/// <summary>
/// Represents the configuration settings for enabling or disabling the use of a cancellation token with a specified timeout.
/// </summary>
/// <remarks>
/// This class is designed to map configuration settings from the <c>appsettings.json</c> file under the 
/// <see cref="SectionName"/> section. It provides properties to control whether a cancellation token is used
/// and to specify the timeout duration.
/// </remarks>
public sealed class WarmupContext
{
    public const string SectionName = "WarmupContext";
    
    /// <summary>
    /// Gets a value indicating whether the feature or option is enabled.
    /// </summary>
    public bool Use { get; init; }
    /// <summary>
    /// Gets or sets a value indicating whether a timeout should be applied when using a cancellation token.
    /// </summary>
    /// <value>
    /// <c>true</c> if a timeout should be applied; otherwise, <c>false</c>.
    /// </value>
    /// <remarks>
    /// This property is used to determine whether the application should enforce a timeout when performing
    /// operations that support cancellation. It is typically configured through the <c>appsettings.json</c>
    /// file under the <see cref="WarmupContext.SectionName"/> section.
    /// </remarks>
    public bool UseTimeout { get; set; }
    /// <summary>
    /// Gets the timeout duration, in milliseconds, for the warmup process.
    /// </summary>
    /// <remarks>
    /// This property specifies the duration for which the warmup process should wait before timing out. 
    /// It is initialized from the <c>Timeout</c> setting in the <see cref="WarmupContext"/> section of the 
    /// configuration file.
    /// </remarks>
    /// <value>
    /// The timeout duration in milliseconds.
    /// </value>
    public int Timeout { get; init; }
}

