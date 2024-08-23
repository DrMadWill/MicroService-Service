namespace Application.Helpers;

public static class Parser
{
    /// <summary>
    /// Covert To String 
    /// </summary>
    /// <param name="rowValue"></param>
    /// <returns></returns>
    public static string GetRowValueAsString(this object rowValue)
    {
        if (DBNull.Value == rowValue) return string.Empty;
#pragma warning disable CS8603 // Possible null reference return.
        return rowValue switch
        {
            double d => $"{d}",
            string _ => $"{rowValue}",
            DateTime _ => $"{rowValue}",
            _ => null
        };
#pragma warning restore CS8603 // Possible null reference return.
    }
}