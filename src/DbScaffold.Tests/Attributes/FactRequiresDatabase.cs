namespace DbScaffold.Tests.Attributes;

internal sealed class FactRequiresDatabaseAttribute : FactAttribute
{
    /// <summary>
    /// When applied to a test method, this attribute will cause the test to be skipped if the database is not available.
    /// </summary>
    public FactRequiresDatabaseAttribute()
    {
        if (!DatabaseAvailabilityChecker.IsDatabaseAvailable)
        {
            Skip = DatabaseAvailabilityChecker.DatabaseUnavailableMessage;
        }
    }

    /// <summary>
    /// When applied to a test method, this attribute will cause the test to be skipped if the database is not available.
    /// </summary>
    /// <remarks>
    /// The provided skip reason will be used in the test run output if the test is skipped.
    /// </remarks>
    /// <param name="skipReason"></param>
    public FactRequiresDatabaseAttribute(string skipReason)
    {
        if (!DatabaseAvailabilityChecker.IsDatabaseAvailable)
        {
            Skip = skipReason;
        }
    }
}