namespace Core.Interfaces.Request
{
    public interface IOrderedRequest
    {
        /// <summary>
        /// Field name of ordering
        /// </summary>
        string OrderBy { get; set; }

        /// <summary>
        /// Direction of ordering
        /// </summary>
        bool IsDescending { get; set; }
    }
}
